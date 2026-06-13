import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutumnLeafIcon } from './autumn-leaf.icon';

describe('AutumnLeafIcon', () => {
  let component: AutumnLeafIcon;
  let fixture: ComponentFixture<AutumnLeafIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AutumnLeafIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(AutumnLeafIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
