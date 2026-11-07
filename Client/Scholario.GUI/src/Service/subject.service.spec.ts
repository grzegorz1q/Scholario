import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';

import { SubjectService } from './subject.service';

describe('SubjectService', () => {
  let service: SubjectService;

  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [provideHttpClient()] });
    service = TestBed.inject(SubjectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
