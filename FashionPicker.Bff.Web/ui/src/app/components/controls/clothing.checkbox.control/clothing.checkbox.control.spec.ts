import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClothingCheckboxControl } from './clothing.checkbox.control';

describe('ClothingCheckboxControl', () => {
  let component: ClothingCheckboxControl;
  let fixture: ComponentFixture<ClothingCheckboxControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClothingCheckboxControl],
    }).compileComponents();

    fixture = TestBed.createComponent(ClothingCheckboxControl);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
