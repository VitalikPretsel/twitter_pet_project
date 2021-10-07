import { Component, Input, OnChanges } from '@angular/core';

import { PostsService } from '../_services/posts.service';
import { LikesService } from '../_services/likes.service';
import { ProfileService } from '../_services/profile.service';

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

  constructor(
    private postsService: PostsService,
    private profileService: ProfileService,
    private likesService: LikesService
  ) {
    this.allPost = new Array<Post>();
  }

  ngOnChanges(): void {
    this.allPost = [];
    this.loadPosts(this.profileIds);
  }

  loadPosts(profileIds, lastId = null) {
    this.postsService.getProfilesPosts(profileIds, lastId).subscribe(data => {
      let startIndex = this.allPost.length;
      this.allPost = this.allPost.concat(data);   
      if (this.profileService.currentProfileValue != null) {
        for (let i = startIndex; i < this.allPost.length; i++) {
          this.getIsLiked(this.profileService.currentProfileValue.id, this.allPost[i].id, i);
        }
      }
    });
  }

  getIsLiked(profileId, postId, postIndex) {
    this.likesService.isLiked(profileId, postId).subscribe(res => {
      this.allPost[postIndex].isLiked = res;
    });
  }

  like(postId) {
    this.likesService.like({
      profileId: this.profileService.currentProfileValue.id,
      postId: postId
    }).subscribe(() => {
      let post = this.allPost.find(p => p.id == postId);
      post.likesAmount += 1;
      post.isLiked = true;
    })
  }

  unlike(postId) {
    this.likesService.unlike(this.profileService.currentProfileValue.id, postId).subscribe(() => {
      let post = this.allPost.find(p => p.id == postId);
      post.likesAmount -= 1;
      post.isLiked = false;
    })
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
