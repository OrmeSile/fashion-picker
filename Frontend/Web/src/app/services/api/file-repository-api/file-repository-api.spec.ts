import { TestBed } from '@angular/core/testing';

import { FileRepositoryApi } from './file-repository-api';

describe('FileRepositoryAPi', () => {
  let service: FileRepositoryApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FileRepositoryApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
