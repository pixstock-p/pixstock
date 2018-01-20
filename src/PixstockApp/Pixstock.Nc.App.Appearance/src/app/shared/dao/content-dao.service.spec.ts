import { TestBed, inject } from '@angular/core/testing';

import { ContentDaoService } from './content-dao.service';

describe('ContentDaoService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ContentDaoService]
    });
  });

  it('should be created', inject([ContentDaoService], (service: ContentDaoService) => {
    expect(service).toBeTruthy();
  }));
});
