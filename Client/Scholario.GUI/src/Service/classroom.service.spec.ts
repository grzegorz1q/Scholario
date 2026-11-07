import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';

import { ClassroomService } from './classroom.service';

describe('ClassroomService', () => {
  let service: ClassroomService;

  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [provideHttpClient()] });
    service = TestBed.inject(ClassroomService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
