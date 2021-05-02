import { Component, OnInit } from '@angular/core';
import { Resume } from './model/resume.model';
import { ResumePageStateService } from './resume-page-state.service';

@Component({
  selector: 'app-resume-page',
  templateUrl: './resume-page.component.html',
  styleUrls: ['./resume-page.component.css'],
  providers: [ResumePageStateService],
})
export class ResumePageComponent implements OnInit {
  state$ = this.stateService.state$;

  constructor(private stateService: ResumePageStateService) {}

  async ngOnInit(): Promise<void> {
    await this.stateService.init();
  }

  async onSave(resume: Resume): Promise<void> {
    await this.stateService.save(resume);
  }
}
