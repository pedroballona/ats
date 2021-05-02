import { TestBed } from '@angular/core/testing';

import { ResumePageStateService } from './resume-page-state.service';

describe('ResumePageStateService', () => {
  let service: ResumePageStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResumePageStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
