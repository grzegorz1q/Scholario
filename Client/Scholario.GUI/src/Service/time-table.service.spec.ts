import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';

import { TimeTableService } from './time-table.service';

describe('TimeTableService', () => {
  let service: TimeTableService;

  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [provideHttpClient()] });
    service = TestBed.inject(TimeTableService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
