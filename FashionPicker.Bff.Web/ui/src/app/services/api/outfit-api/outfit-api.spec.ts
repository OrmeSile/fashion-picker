import { TestBed } from '@angular/core/testing';

import { OutfitApi } from './outfit-api';

describe('OutfitApi', () => {
  let service: OutfitApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OutfitApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
