import { TestBed } from '@angular/core/testing';

import { OpeningListingPageStateService } from './opening-listing-page-state.service';

describe('OpeningListingPageStateService', () => {
  let service: OpeningListingPageStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OpeningListingPageStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
