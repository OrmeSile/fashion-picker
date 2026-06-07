import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EyeClosedIcon } from './eye-closed.icon';

describe('EyeClosedIcon', () => {
  let component: EyeClosedIcon;
  let fixture: ComponentFixture<EyeClosedIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EyeClosedIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(EyeClosedIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
