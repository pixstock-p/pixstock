import { TestBed, inject } from '@angular/core/testing';

import { PixstockNetService } from './pixstock-net.service';

describe('PixstockNetService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PixstockNetService]
    });
  });

  it('should be created', inject([PixstockNetService], (service: PixstockNetService) => {
    expect(service).toBeTruthy();
  }));
});
