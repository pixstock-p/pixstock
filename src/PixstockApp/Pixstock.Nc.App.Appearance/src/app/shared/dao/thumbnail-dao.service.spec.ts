import { TestBed, inject } from '@angular/core/testing';

import { ThumbnailDaoService } from './thumbnail-dao.service';

describe('ThumbnailDaoService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ThumbnailDaoService]
    });
  });

  it('should be created', inject([ThumbnailDaoService], (service: ThumbnailDaoService) => {
    expect(service).toBeTruthy();
  }));
});
