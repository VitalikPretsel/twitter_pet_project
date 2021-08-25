import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeedScrollComponent } from './feed-scroll.component';

describe('InfiniteScrollComponent', () => {
  let component: FeedScrollComponent;
  let fixture: ComponentFixture<FeedScrollComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FeedScrollComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FeedScrollComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
