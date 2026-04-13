import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DropUpload } from './drop-upload';

describe('DropUpload', () => {
  let component: DropUpload;
  let fixture: ComponentFixture<DropUpload>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DropUpload],
    }).compileComponents();

    fixture = TestBed.createComponent(DropUpload);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
