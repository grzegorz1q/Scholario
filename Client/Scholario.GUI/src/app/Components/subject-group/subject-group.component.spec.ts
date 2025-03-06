import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectGroupComponent } from './subject-group.component';

describe('SubjectGroupComponent', () => {
  let component: SubjectGroupComponent;
  let fixture: ComponentFixture<SubjectGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubjectGroupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubjectGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
