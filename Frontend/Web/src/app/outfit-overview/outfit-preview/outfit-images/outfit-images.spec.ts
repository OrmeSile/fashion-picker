import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutfitImages } from './outfit-images';

describe('OutfitImages', () => {
  let component: OutfitImages;
  let fixture: ComponentFixture<OutfitImages>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OutfitImages],
    }).compileComponents();

    fixture = TestBed.createComponent(OutfitImages);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
