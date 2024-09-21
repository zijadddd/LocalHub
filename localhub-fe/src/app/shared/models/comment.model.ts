import { UserOut } from './user.model';

export class CommentOut {
  id: string;
  content: string;
  user: UserOut;
  postId: string;
  created: string;
  updated: string;
}

export class CommentIn {
  content: string;
}
