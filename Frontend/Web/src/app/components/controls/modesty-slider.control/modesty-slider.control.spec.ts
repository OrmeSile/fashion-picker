import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModestySliderControl } from './modesty-slider.control';

describe('ModestySliderControl', () => {
  let component: ModestySliderControl;
  let fixture: ComponentFixture<ModestySliderControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModestySliderControl],
    }).compileComponents();

    fixture = TestBed.createComponent(ModestySliderControl);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
