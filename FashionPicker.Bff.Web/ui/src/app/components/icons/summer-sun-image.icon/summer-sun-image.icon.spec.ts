import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SummerSunImageIcon } from './summer-sun-image.icon';

describe('SummerSunImageIcon', () => {
  let component: SummerSunImageIcon;
  let fixture: ComponentFixture<SummerSunImageIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SummerSunImageIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(SummerSunImageIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
