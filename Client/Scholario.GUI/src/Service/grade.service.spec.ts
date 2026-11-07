import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';

import { GradeService } from './grade.service';

describe('GradeService', () => {
  let service: GradeService;

  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [provideHttpClient()] });
    service = TestBed.inject(GradeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
