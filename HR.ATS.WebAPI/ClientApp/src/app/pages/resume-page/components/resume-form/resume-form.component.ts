import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output
} from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators
} from '@angular/forms';
import { PoDatepickerIsoFormat } from '@po-ui/ng-components';
import { DateTime } from 'luxon';
import { Resume } from '../../model/resume.model';

function experienceDateValidator(
  control: AbstractControl
): ValidationErrors | null {
  const parent = control.parent;
  if (!parent) {
    return null;
  }
  const startDateString: string = parent.get('periodStartDate')?.value;
  const startEndDateString: string = parent.get('periodEndDate')?.value;
  if (startDateString && startEndDateString) {
    const startDate = DateTime.fromISO(startDateString);
    const endDate = DateTime.fromISO(startEndDateString);
    return startDate >= endDate ? { periodError: 'Error' } : null;
  }
  return null;
}

@Component({
  selector: 'app-resume-form',
  templateUrl: './resume-form.component.html',
  styleUrls: ['./resume-form.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ResumeFormComponent {
  @Input() set resume(value: Resume | undefined | null) {
    this.originalValue = value;
    this.setFormValue(value);
  }
  @Output() saved = new EventEmitter<Resume>();

  isoFormat = PoDatepickerIsoFormat.Basic;
  private originalValue: Resume | undefined | null;

  formGroup = this.createFormGroup();

  private setFormValue(value: Resume | undefined | null): void {
    this.formGroup.reset();
    if (!value) {
      this.formGroup = this.createFormGroup();
    } else {
      this.formGroup.setValue(value);
    }
  }

  private createFormGroup(): FormGroup {
    return this.fb.group({
      introduction: [null, Validators.required],
      experiences: this.fb.array(
        [this.generateExperienceForm()],
        [Validators.required, Validators.minLength(1)]
      ),
    });
  }

  get experiences(): FormArray {
    return this.formGroup.get('experiences') as FormArray;
  }

  private generateExperienceForm(): FormGroup {
    return this.fb.group({
      company: [null, Validators.required],
      description: [null, Validators.required],
      periodStartDate: [null, [Validators.required, experienceDateValidator]],
      periodEndDate: [null, [experienceDateValidator]],
    });
  }

  constructor(private fb: FormBuilder) {}

  addExperienceForm(): void {
    this.experiences.push(this.generateExperienceForm());
    this.experiences.markAsTouched();
  }

  removeExperienceForm(index: number): void {
    this.experiences.removeAt(index);
    this.experiences.markAsTouched();
  }

  cancel(): void {
    this.setFormValue(this.originalValue);
  }

  save(): void {
    let value: Resume = this.formGroup.value;
    value = {
      ...value,
      experiences: value.experiences.map((e) => {
        return {
          ...e,
          periodStartDate: DateTime.fromISO(e.periodStartDate).toISO({
            suppressMilliseconds: true,
          }),
          periodEndDate: e.periodEndDate
            ? DateTime.fromISO(e.periodEndDate).toISO({
                suppressMilliseconds: true,
              })
            : undefined,
        };
      }),
    };
    this.saved.emit(value);
  }
}
