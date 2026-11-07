import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';

import { ScheduleEntryService } from './schedule-entry.service';

describe('ScheduleEntryService', () => {
  let service: ScheduleEntryService;

  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [provideHttpClient()] });
    service = TestBed.inject(ScheduleEntryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
