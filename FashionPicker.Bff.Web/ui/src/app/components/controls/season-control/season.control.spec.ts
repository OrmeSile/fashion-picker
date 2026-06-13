import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonControl } from './season.control';

describe('SeasonControl', () => {
  let component: SeasonControl;
  let fixture: ComponentFixture<SeasonControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SeasonControl],
    }).compileComponents();

    fixture = TestBed.createComponent(SeasonControl);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
