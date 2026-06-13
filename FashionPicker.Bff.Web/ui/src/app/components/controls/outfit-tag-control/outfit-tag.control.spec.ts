import { ComponentFixture, TestBed } from '@angular/core/testing';
import {OutfitTagControl} from './outfit-tag.control';

describe('OutfitTagControl', () => {
  let component: OutfitTagControl;
  let fixture: ComponentFixture<OutfitTagControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OutfitTagControl],
    }).compileComponents();

    fixture = TestBed.createComponent(OutfitTagControl);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
