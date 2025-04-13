export type CommentPost = {
    rating: number;
    content: string;
  };
  
export type CommentGet = {
    id: number;
    rating: number;
    content: string;
    createdBy: string;
    userAvatar: string;
    createdOn: number;
  };