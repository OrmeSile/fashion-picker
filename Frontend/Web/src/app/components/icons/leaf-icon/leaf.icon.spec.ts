import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeafIcon } from './leaf.icon';

describe('LeafIcon', () => {
  let component: LeafIcon;
  let fixture: ComponentFixture<LeafIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeafIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(LeafIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
