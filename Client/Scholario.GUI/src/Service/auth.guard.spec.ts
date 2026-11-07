import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AuthService } from './authService';

import { AuthGuard } from './auth.guard';

describe('AuthGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        AuthGuard,
        { provide: AuthService, useValue: { isAuthenticated: () => true } },
        { provide: Router, useValue: { navigate: jasmine.createSpy('navigate') } }
      ]
    });
  });

  it('should be created', () => {
    expect(TestBed.inject(AuthGuard)).toBeTruthy();
  });
});
