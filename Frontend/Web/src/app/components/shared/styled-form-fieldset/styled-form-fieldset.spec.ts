import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StyledFormFieldset } from './styled-form-fieldset';

describe('StyledFormFieldset', () => {
  let component: StyledFormFieldset;
  let fixture: ComponentFixture<StyledFormFieldset>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StyledFormFieldset],
    }).compileComponents();

    fixture = TestBed.createComponent(StyledFormFieldset);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
