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
        this.getLikedPosts(profileIds, lastId, startIndex);
      }
    });
  }

  getLikedPosts(profileIds, lastId, startIndex) {
    this.profileService.currentProfileObservable.subscribe((res) => {
      if (res != null) {
        this.likesService.getProfileLikedPosts(this.profileService.currentProfileValue.id, profileIds, lastId)
          .subscribe(res => {
            for (let i = startIndex; i < this.allPost.length; i++) {
              if (res.find(p => p == this.allPost[i].id)) {
                this.allPost[i].isLiked = true;
              }
              else {
                this.allPost[i].isLiked = false;
              }
            }
          });
      }
    });
  }

  likeAction(postId) {
    let post = this.allPost.find(p => p.id == postId);
    let likeObservable;
    if (post.isLiked) {
      likeObservable = this.likesService.unlike(this.profileService.currentProfileValue.id, postId);
    }
    else {
      likeObservable = this.likesService.like({
        profileId: this.profileService.currentProfileValue.id,
        postId: postId
      });
    }
    likeObservable.subscribe(() => {
      if (post.isLiked) {
        post.likesAmount -= 1;
        post.isLiked = false;
      }
      else {
        post.likesAmount += 1;
        post.isLiked = true;
      }
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
