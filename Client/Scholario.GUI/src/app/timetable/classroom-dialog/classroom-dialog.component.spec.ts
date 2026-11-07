import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomDialogComponent } from './classroom-dialog.component';

describe('ClassroomDialogComponent', () => {
  let component: ClassroomDialogComponent;
  let fixture: ComponentFixture<ClassroomDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassroomDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassroomDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
