import { TestBed } from '@angular/core/testing';

import { OutfitEdit } from './outfit-edit';

describe('OutfitEdit', () => {
  let service: OutfitEdit;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OutfitEdit);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
