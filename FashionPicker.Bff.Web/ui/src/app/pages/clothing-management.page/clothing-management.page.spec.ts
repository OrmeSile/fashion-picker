import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClothingManagementPage } from './clothing-management.page';

describe('ClothingManagementPage', () => {
  let component: ClothingManagementPage;
  let fixture: ComponentFixture<ClothingManagementPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClothingManagementPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ClothingManagementPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
