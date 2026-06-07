import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EyeOpenIcon } from './eye-open.icon';

describe('EyeOpenIcon', () => {
  let component: EyeOpenIcon;
  let fixture: ComponentFixture<EyeOpenIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EyeOpenIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(EyeOpenIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
