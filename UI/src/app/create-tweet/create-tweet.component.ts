import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

import { PostsService } from '../_services/posts.service';
import { ProfileService } from '../_services/profile.service';

import { Profile } from '../_models/profile';

@Component({
  selector: 'app-create-tweet',
  templateUrl: './create-tweet.component.html',
  styleUrls: ['./create-tweet.component.sass']
})
export class CreateTweetComponent implements OnInit {

  profile: Profile;

  constructor(
    private dialogRef: MatDialogRef<CreateTweetComponent>,
    private postsService: PostsService,
    private profileService: ProfileService) { }

  ngOnInit(): void {
    this.profile = this.profileService.currentProfileValue;
  }

  createTweet(createTweetForm: NgForm) {
    createTweetForm.form.patchValue({ profileId: this.profile.id, postingDate: new Date() });
    this.postsService.createPost(createTweetForm.value).subscribe();
    this.closeDialog();
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
