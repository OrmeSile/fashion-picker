import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutfitEditPage } from './outfit-edit.page';

describe('OutfitEditPage', () => {
  let component: OutfitEditPage;
  let fixture: ComponentFixture<OutfitEditPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OutfitEditPage],
    }).compileComponents();

    fixture = TestBed.createComponent(OutfitEditPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
