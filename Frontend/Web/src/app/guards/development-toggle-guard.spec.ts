import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { developmentToggleGuard } from './development-toggle-guard';

describe('developmentToggleGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) =>
    TestBed.runInInjectionContext(() => developmentToggleGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
