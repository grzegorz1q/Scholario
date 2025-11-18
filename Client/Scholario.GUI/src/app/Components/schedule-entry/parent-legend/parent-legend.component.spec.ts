import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentLegendComponent } from './parent-legend.component';

describe('ParentLegendComponent', () => {
  let component: ParentLegendComponent;
  let fixture: ComponentFixture<ParentLegendComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParentLegendComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentLegendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
