import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradeComponentComponent } from './grade-component.component';

describe('GradeComponentComponent', () => {
  let component: GradeComponentComponent;
  let fixture: ComponentFixture<GradeComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GradeComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GradeComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
