import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResumeForRecruiterPageComponent } from './resume-for-recruiter-page.component';

describe('ResumeForRecruiterPageComponent', () => {
  let component: ResumeForRecruiterPageComponent;
  let fixture: ComponentFixture<ResumeForRecruiterPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResumeForRecruiterPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResumeForRecruiterPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
