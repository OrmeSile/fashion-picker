import { TestBed } from '@angular/core/testing';

import { ClothingApi } from './clothing-api';

describe('ClothingApi', () => {
  let service: ClothingApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClothingApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
