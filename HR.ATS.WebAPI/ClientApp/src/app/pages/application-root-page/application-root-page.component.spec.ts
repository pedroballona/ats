import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationRootPageComponent } from './application-root-page.component';

describe('ApplicationRootPageComponent', () => {
  let component: ApplicationRootPageComponent;
  let fixture: ComponentFixture<ApplicationRootPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationRootPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationRootPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
