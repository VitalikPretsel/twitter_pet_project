import { Component, Input, OnChanges } from '@angular/core';

import { PostsService } from '../_services/posts.service';

import { strings } from 'src/constants/strings';

@Component({
  selector: 'app-feed-scroll',
  templateUrl: './feed-scroll.component.html',
  styleUrls: ['./feed-scroll.component.sass']
})
export class FeedScrollComponent implements OnChanges {

  @Input() profileIds: Array<number>; 

  allPost: Array<any>;

  public feedPostStrings = strings.feedPost;

  constructor(private service: PostsService) {
    this.allPost = new Array<any>();
  }

  ngOnChanges(): void { 
    this.loadPosts(this.profileIds);
  }

  loadPosts(profileIds, lastId = -1) {
    this.service.getProfilesPosts(profileIds, lastId).subscribe(data => {
      let startIndex = this.allPost.length;
      this.allPost = this.allPost.concat(data);
      for (let i = startIndex; i < this.allPost.length; i++) {
        this.getLikesAmount(this.allPost[i].id, i);
        this.getRepliesAmount(this.allPost[i].id, i);
        this.getProfileName(this.allPost[i].profileId, i);
      }
    });
  }

  getLikesAmount(postId, postIndex) {
    this.service.getLikesAmount(postId).subscribe(data => {
      this.allPost[postIndex].likesAmount = data;
    });
  }

  getRepliesAmount(postId, postIndex) {
    this.service.getRepliesAmount(postId).subscribe(data => {
      this.allPost[postIndex].repliesAmount = data;
    });
  }

  getProfileName(profileId, postIndex) {
    this.service.getProfileName(profileId).subscribe(data => {
      this.allPost[postIndex].profileName = data;
    });
  }

  onScroll() {
    let lastId = this.allPost[this.allPost.length - 1].id - 1;
    this.loadPosts(this.profileIds, lastId);
  }

  identify(index, item) {
    return item.id;
  }
}
