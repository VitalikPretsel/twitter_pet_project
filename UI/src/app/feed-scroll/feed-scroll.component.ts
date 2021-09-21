import { Component, Input, OnChanges } from '@angular/core';

import { PostsService } from '../_services/posts.service';

import { Post } from '../_models/post';
import { strings } from 'src/constants/strings';
import { scrollTo } from '../_helpers/scrollTo';

@Component({
  selector: 'app-feed-scroll',
  templateUrl: './feed-scroll.component.html',
  styleUrls: ['./feed-scroll.component.sass']
})
export class FeedScrollComponent implements OnChanges {

  @Input() profileIds: Array<number>;
  
  allPost: Array<Post>;

  public feedPostStrings = strings.feedPost;

  constructor(private service: PostsService) {
    this.allPost = new Array<Post>();
  }

  ngOnChanges(): void {
    this.allPost = [];
    this.loadPosts(this.profileIds);
  }

  loadPosts(profileIds, lastId = null) {
    this.service.getProfilesPosts(profileIds, lastId).subscribe(data => {
      this.allPost = this.allPost.concat(data);
    });
  }

  onScroll() {
    let lastId = this.allPost[this.allPost.length - 1].id - 1;
    this.loadPosts(this.profileIds, lastId);
  }

  identify(index, item) {
    return item.id;
  }

  scrollTo = scrollTo;
}
