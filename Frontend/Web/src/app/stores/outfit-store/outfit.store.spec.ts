import { TestBed } from '@angular/core/testing';

import { OutfitStore } from './outfit.store';

describe('OutfitStore', () => {
  let service: OutfitStore;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OutfitStore);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
