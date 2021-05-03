import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpeningAppliedApplicantsListingPageComponent } from './opening-applied-applicants-listing-page.component';

describe('OpeningAppliedApplicantsListingPageComponent', () => {
  let component: OpeningAppliedApplicantsListingPageComponent;
  let fixture: ComponentFixture<OpeningAppliedApplicantsListingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OpeningAppliedApplicantsListingPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OpeningAppliedApplicantsListingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
