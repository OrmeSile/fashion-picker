import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SnowflakeIcon } from './snowflake.icon';

describe('SnowflakeIcon', () => {
  let component: SnowflakeIcon;
  let fixture: ComponentFixture<SnowflakeIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SnowflakeIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(SnowflakeIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
