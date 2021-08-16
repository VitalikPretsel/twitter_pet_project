import { Component, OnInit } from '@angular/core';

import { PostsService } from '../_services/posts.service';

@Component({
  selector: 'app-infinite-scroll',
  templateUrl: './infinite-scroll.component.html',
  styleUrls: ['./infinite-scroll.component.sass']
})
export class InfiniteScrollComponent implements OnInit {

  allPost: Array<any>;

  constructor(private service: PostsService) {
    this.allPost = new Array<any>();
    this.loadPosts();
  }

  ngOnInit(): void { }

  loadPosts(lastId = -1) {
    this.service.getPosts(lastId).subscribe(data => {
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
    this.loadPosts(lastId);
  }
}
