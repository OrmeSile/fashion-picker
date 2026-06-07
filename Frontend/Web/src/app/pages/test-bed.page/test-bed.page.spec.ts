import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestBedPage } from './test-bed.page';

describe('TestBedPage', () => {
  let component: TestBedPage;
  let fixture: ComponentFixture<TestBedPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestBedPage],
    }).compileComponents();

    fixture = TestBed.createComponent(TestBedPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
