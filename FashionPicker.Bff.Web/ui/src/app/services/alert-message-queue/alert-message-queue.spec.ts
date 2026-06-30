import { TestBed } from '@angular/core/testing';

import { AlertMessageQueue } from './alert-message-queue';

describe('AlertMessageQueue', () => {
  let service: AlertMessageQueue;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlertMessageQueue);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
