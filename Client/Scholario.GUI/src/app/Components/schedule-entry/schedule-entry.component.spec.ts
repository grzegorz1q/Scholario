import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';

import { ScheduleEntryComponent } from './schedule-entry.component';

describe('ScheduleEntryComponent', () => {
  let component: ScheduleEntryComponent;
  let fixture: ComponentFixture<ScheduleEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleEntryComponent],
      providers: [provideHttpClient()]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
