import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryExplorerComponent } from './category-explorer.component';

describe('CategoryExplorerComponent', () => {
  let component: CategoryExplorerComponent;
  let fixture: ComponentFixture<CategoryExplorerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoryExplorerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryExplorerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
