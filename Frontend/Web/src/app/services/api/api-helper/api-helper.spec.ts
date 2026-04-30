import { TestBed } from '@angular/core/testing';

import { ApiHelper } from './api-helper';

describe('ApiHelper', () => {
  let service: ApiHelper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiHelper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
