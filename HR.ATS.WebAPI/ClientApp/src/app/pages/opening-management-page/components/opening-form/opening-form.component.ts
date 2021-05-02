import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PoModalAction, PoModalComponent } from '@po-ui/ng-components';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Opening } from '../../models/opening.model';

@Component({
  selector: 'app-opening-form',
  templateUrl: './opening-form.component.html',
  styleUrls: ['./opening-form.component.css'],
})
export class OpeningFormComponent {
  @Output() saved = new EventEmitter<Opening>();

  @ViewChild(PoModalComponent)
  modal?: PoModalComponent;

  formGroup = this.fb.group({
    name: [null, Validators.required],
    description: [null, Validators.required],
  });

  primaryAction$: Observable<PoModalAction> = this.formGroup.valueChanges.pipe(
    startWith(null),
    map(() => ({
      label: 'Salvar',
      action: this.save.bind(this),
      disabled: this.formGroup.invalid,
    }))
  );

  secondaryAction: PoModalAction = {
    label: 'Cancel',
    action: () => this.modal?.close()
  };

  constructor(private fb: FormBuilder) {}

  open(): void {
    if (this.modal) {
      this.formGroup.reset();
      this.modal.open();
    }
  }

  close(): void {
    if (this.modal) {
      this.formGroup.reset();
      this.modal.close();
    }
  }

  save(): void {
    this.saved.emit(this.formGroup.value);
  }
}
