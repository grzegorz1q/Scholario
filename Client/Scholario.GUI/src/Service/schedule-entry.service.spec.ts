import { TestBed } from '@angular/core/testing';

import { ScheduleEntryService } from './schedule-entry.service';

describe('ScheduleEntryService', () => {
  let service: ScheduleEntryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScheduleEntryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
