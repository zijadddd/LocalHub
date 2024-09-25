export class PostApi {
  public static GET_POSTS = 'https://localhost:7202/api/Post';
  public static GET_POST = 'https://localhost:7202/api/Post/#';
  public static GET_USER_LIKES_AND_COMMENTS_COUNT =
    'https://localhost:7202/api/Post/#/getLikeAndCommentCount';
  public static DID_USER_LIKE_POST =
    'https://localhost:7202/api/Post/#/!/didUserLikeThePost';
  public static LIKE_POST = 'https://localhost:7202/api/Post/#/!/likePost';
  public static CREATE_POST = 'https://localhost:7202/api/Post/#';
}
