import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlertMessageOverlay } from './alert-message-overlay';

describe('AlertMessageOverlay', () => {
  let component: AlertMessageOverlay;
  let fixture: ComponentFixture<AlertMessageOverlay>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AlertMessageOverlay],
    }).compileComponents();

    fixture = TestBed.createComponent(AlertMessageOverlay);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
