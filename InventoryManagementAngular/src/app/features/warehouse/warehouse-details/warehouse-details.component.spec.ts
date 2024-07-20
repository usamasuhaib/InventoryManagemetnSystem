import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseDetailsComponent } from './warehouse-details.component';

describe('WarehouseDetailsComponent', () => {
  let component: WarehouseDetailsComponent;
  let fixture: ComponentFixture<WarehouseDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WarehouseDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WarehouseDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
