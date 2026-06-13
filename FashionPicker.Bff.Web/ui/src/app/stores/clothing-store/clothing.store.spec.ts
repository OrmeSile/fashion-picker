import { TestBed } from '@angular/core/testing';

import { ClothingStore } from './clothing.store';

describe('ClothingStore', () => {
  let service: ClothingStore;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClothingStore);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
