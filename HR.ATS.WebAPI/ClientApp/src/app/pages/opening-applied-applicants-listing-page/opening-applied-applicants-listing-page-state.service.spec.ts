import { TestBed } from '@angular/core/testing';

import { OpeningAppliedApplicantsListingPageStateService } from './opening-applied-applicants-listing-page-state.service';

describe('OpeningAppliedApplicantsListingPageStateService', () => {
  let service: OpeningAppliedApplicantsListingPageStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OpeningAppliedApplicantsListingPageStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
