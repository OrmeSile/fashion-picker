import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutfitPreview } from './outfit-preview';

describe('OutfitPreview', () => {
  let component: OutfitPreview;
  let fixture: ComponentFixture<OutfitPreview>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OutfitPreview],
    }).compileComponents();

    fixture = TestBed.createComponent(OutfitPreview);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
