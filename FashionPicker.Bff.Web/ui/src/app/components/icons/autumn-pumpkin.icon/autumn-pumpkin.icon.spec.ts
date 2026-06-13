import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutumnPumpkinIcon } from './autumn-pumpkin.icon';

describe('AutumnPumpkinIcon', () => {
  let component: AutumnPumpkinIcon;
  let fixture: ComponentFixture<AutumnPumpkinIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AutumnPumpkinIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(AutumnPumpkinIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
