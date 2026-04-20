import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutfitMetadataForm } from './outfit-metadata.form';

describe('OutfitMetadataForm', () => {
  let component: OutfitMetadataForm;
  let fixture: ComponentFixture<OutfitMetadataForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OutfitMetadataForm],
    }).compileComponents();

    fixture = TestBed.createComponent(OutfitMetadataForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
