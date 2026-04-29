import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpringFlowerIcon } from './spring-flower.icon';

describe('SpringFlowerIcon', () => {
  let component: SpringFlowerIcon;
  let fixture: ComponentFixture<SpringFlowerIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpringFlowerIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(SpringFlowerIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
