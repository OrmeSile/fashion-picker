import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImageAlbumGridPreview } from './image-album-grid-preview';

describe('ImageAlbumGridPreview', () => {
  let component: ImageAlbumGridPreview;
  let fixture: ComponentFixture<ImageAlbumGridPreview>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ImageAlbumGridPreview],
    }).compileComponents();

    fixture = TestBed.createComponent(ImageAlbumGridPreview);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
