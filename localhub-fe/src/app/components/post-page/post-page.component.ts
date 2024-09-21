import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PostService } from '../../shared/services/post.service';
import { PostOut } from '../../shared/models/post.model';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { CommentService } from '../../shared/services/comment.service';
import { CommentIn, CommentOut } from '../../shared/models/comment.model';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { UserLikedPostOut } from '../../shared/models/user-like.model';
import { Modal } from '../../shared/models/modal.model';
import { CommunicationService } from '../../shared/services/communication.service';
import { Title } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-post-page',
  standalone: true,
  imports: [NgIf, NgFor, NgClass, RouterModule, FormsModule],
  templateUrl: './post-page.component.html',
  styleUrl: './post-page.component.css',
})
export class PostPageComponent implements OnInit {
  public post: PostOut;
  public comments: CommentOut[];
  public commentDropDownIsOpened: Map<string, boolean> = new Map<
    string,
    boolean
  >();
  public authenticatedUserId: string;
  public didUserLikePost: UserLikedPostOut;
  public isEditCommentInputShowed: boolean = true;

  public editCommentInputs: any[] = [];

  private commentIdForDeleting: string;

  @ViewChild('comment') commentInput: ElementRef;

  constructor(
    private readonly postService: PostService,
    private readonly route: ActivatedRoute,
    private readonly commentService: CommentService,
    private readonly authService: AuthenticationService,
    private readonly communicationService: CommunicationService,
    private readonly titleService: Title
  ) {}

  ngOnInit(): void {
    const postId = this.route.snapshot.paramMap.get('id')!;

    this.postService.getPost(postId).subscribe((response) => {
      this.post = response;
      this.post.created = this.dateTimeFormatter(this.post.created);
      this.post.updated =
        this.post.updated !== '0001-01-01T00:00:00'
          ? this.dateTimeFormatter(this.post.updated)
          : '';
      this.authenticatedUserId = this.authService.getNameIdentifierFromToken(
        localStorage.getItem('token')!
      )!;

      this.postService
        .didUserLikePost(this.authenticatedUserId, this.post.id)
        .subscribe((response) => {
          this.didUserLikePost = response;
        });

      this.titleService.setTitle('LocaLHub - ' + this.post.name);
    });

    this.commentService.getAllComments(postId).subscribe((response) => {
      this.comments = response.map((comment) => {
        comment.created = this.dateTimeFormatter(comment.created);
        comment.updated =
          comment.updated !== '0001-01-01T00:00:00'
            ? this.dateTimeFormatter(comment.updated)
            : '';
        this.commentDropDownIsOpened.set(comment.id, false);
        this.editCommentInputs.push({ isShowed: false });
        return comment;
      });
    });

    this.communicationService.data$.subscribe((response) => {
      if (response == true) this.deleteComment(this.commentIdForDeleting);
    });
  }

  private dateTimeFormatter(dateTime: string): string {
    const [date, timeWithSeconds] = dateTime.split('T');
    const [hour, minute] = timeWithSeconds.split(':');

    return date + ' ' + `${hour}:${minute}`;
  }

  toggleDropDown(id: string) {
    this.commentDropDownIsOpened.set(id, !this.commentDropDownIsOpened.get(id));
  }

  likePost() {
    this.postService
      .likePost(this.authenticatedUserId, this.post.id)
      .subscribe((response) => {
        this.didUserLikePost.isLiked = response.isLiked;
        this.post.likes = response.numberOfLikes;
      });
  }

  commentPost(comment: string) {
    const request: CommentIn = new CommentIn();
    request.content = comment;

    this.commentService
      .commentPost(this.post.id, this.authenticatedUserId, request)
      .subscribe((response) => {
        response.created = this.dateTimeFormatter(response.created);
        response.updated =
          response.updated !== '0001-01-01T00:00:00'
            ? this.dateTimeFormatter(response.updated)
            : '';
        this.comments.push(response);
        this.editCommentInputs.push({ isShowed: false });
        this.commentInput.nativeElement.value = '';
      });
  }

  openDeleteCommentModal(id: string, event: Event) {
    event.preventDefault();
    event.stopPropagation();

    const deleteModal: Modal = new Modal();
    deleteModal.heading = 'Are you sure?';
    deleteModal.content =
      'This action CANNOT be undone. This will permanently delete the comment.';
    deleteModal.isWarning = true;
    deleteModal.isShowed = true;

    this.communicationService.openModal(deleteModal);
    this.commentIdForDeleting = id;
  }

  deleteComment(id: string) {
    this.commentService.deleteComment(id).subscribe((response) => {
      this.comments = this.comments.filter((comment) => comment.id !== id);
    });
  }

  toggleEditComment(editCommentInputId: number, event: Event) {
    event.preventDefault();
    event.stopPropagation();

    this.editCommentInputs[editCommentInputId].isShowed =
      !this.editCommentInputs[editCommentInputId].isShowed;

    if (
      this.commentDropDownIsOpened.get(this.comments[editCommentInputId].id)
    ) {
      this.toggleDropDown(this.comments[editCommentInputId].id);
    }
  }

  editComment(commentId: string) {
    const commentRequest: CommentIn = new CommentIn();
    const comment = this.comments.find((comment) => comment.id === commentId)!;
    commentRequest.content = comment?.content!;

    this.commentService
      .editComment(commentId, commentRequest)
      .subscribe((response) => {
        comment.content = response.content;
        comment.updated = this.dateTimeFormatter(response.updated);
      });

    this.editCommentInputs[
      this.comments.findIndex((comment) => comment.id === commentId)
    ].isShowed = false;
  }
}
