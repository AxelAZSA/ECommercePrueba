import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticuloAddComponent } from './articulo-add.component';

describe('ArticuloAddComponent', () => {
  let component: ArticuloAddComponent;
  let fixture: ComponentFixture<ArticuloAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ArticuloAddComponent]
    });
    fixture = TestBed.createComponent(ArticuloAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
