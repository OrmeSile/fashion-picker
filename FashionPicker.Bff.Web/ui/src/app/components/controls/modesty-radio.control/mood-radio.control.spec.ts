import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoodRadioControl } from './mood-radio.control';

describe('ModestyRadioControl', () => {
  let component: MoodRadioControl;
  let fixture: ComponentFixture<MoodRadioControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MoodRadioControl],
    }).compileComponents();

    fixture = TestBed.createComponent(MoodRadioControl);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
