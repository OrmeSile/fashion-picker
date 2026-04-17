import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutfitTag } from './outfit-tag';

describe('OutfitTag', () => {
  let component: OutfitTag;
  let fixture: ComponentFixture<OutfitTag>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OutfitTag],
    }).compileComponents();

    fixture = TestBed.createComponent(OutfitTag);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
