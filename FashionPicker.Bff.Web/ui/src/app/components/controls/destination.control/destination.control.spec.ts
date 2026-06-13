import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DestinationControl } from './destination.control';

describe('DestinationControl', () => {
  let component: DestinationControl;
  let fixture: ComponentFixture<DestinationControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DestinationControl],
    }).compileComponents();

    fixture = TestBed.createComponent(DestinationControl);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
