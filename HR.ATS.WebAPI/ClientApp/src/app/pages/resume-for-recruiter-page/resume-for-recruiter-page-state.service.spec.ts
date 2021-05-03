import { TestBed } from '@angular/core/testing';

import { ResumeForRecruiterPageStateService } from './resume-for-recruiter-page-state.service';

describe('ResumeForRecruiterPageStateService', () => {
  let service: ResumeForRecruiterPageStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResumeForRecruiterPageStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
