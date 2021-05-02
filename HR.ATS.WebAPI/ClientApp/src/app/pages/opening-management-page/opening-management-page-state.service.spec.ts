import { TestBed } from '@angular/core/testing';

import { OpeningManagementPageStateService } from './opening-management-page-state.service';

describe('OpeningManagementPageStateService', () => {
  let service: OpeningManagementPageStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OpeningManagementPageStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
