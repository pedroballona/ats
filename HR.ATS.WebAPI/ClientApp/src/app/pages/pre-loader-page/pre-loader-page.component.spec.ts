import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreLoaderPageComponent } from './pre-loader-page.component';

describe('PreLoaderPageComponent', () => {
  let component: PreLoaderPageComponent;
  let fixture: ComponentFixture<PreLoaderPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PreLoaderPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PreLoaderPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
