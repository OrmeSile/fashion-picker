import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClothingPreviewCard } from './clothing-preview-card';

describe('ClothingPreviewCard', () => {
  let component: ClothingPreviewCard;
  let fixture: ComponentFixture<ClothingPreviewCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClothingPreviewCard],
    }).compileComponents();

    fixture = TestBed.createComponent(ClothingPreviewCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
