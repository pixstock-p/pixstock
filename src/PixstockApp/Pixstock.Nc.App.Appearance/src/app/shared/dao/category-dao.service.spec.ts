import { TestBed, inject } from '@angular/core/testing';

import { CategoryDaoService } from './category-dao.service';

describe('CategoryDaoService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CategoryDaoService]
    });
  });

  it('should be created', inject([CategoryDaoService], (service: CategoryDaoService) => {
    expect(service).toBeTruthy();
  }));
});
