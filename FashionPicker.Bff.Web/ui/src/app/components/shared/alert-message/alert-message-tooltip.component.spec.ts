import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlertMessageTooltip } from './alert-message-tooltip.component';

describe('AlertMessage', () => {
  let component: AlertMessageTooltip;
  let fixture: ComponentFixture<AlertMessageTooltip>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AlertMessageTooltip],
    }).compileComponents();

    fixture = TestBed.createComponent(AlertMessageTooltip);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
