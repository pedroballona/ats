import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpeningListingPageComponent } from './opening-listing-page.component';

describe('OpeningListingPageComponent', () => {
  let component: OpeningListingPageComponent;
  let fixture: ComponentFixture<OpeningListingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OpeningListingPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OpeningListingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
