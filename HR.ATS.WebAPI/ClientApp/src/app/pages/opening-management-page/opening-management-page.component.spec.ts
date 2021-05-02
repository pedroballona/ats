import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpeningManagementPageComponent } from './opening-management-page.component';

describe('OpeningManagementPageComponent', () => {
  let component: OpeningManagementPageComponent;
  let fixture: ComponentFixture<OpeningManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OpeningManagementPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OpeningManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
