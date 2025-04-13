import React from "react";
import { CommentGet } from "../../../Models/Comment";
import { Avatar, Rating } from "@mui/material";
import moment from "moment";


type Props = {
    comment: CommentGet;
  };
  
  const ProductCommentListItem = ({ comment }: Props) => {
    return (
      <div className="relative grid grid-cols-1 gap-4 ml-4 p-4 mb-8 max-w-[900px] border rounded-lg bg-white shadow-lg">
          <div className="flex gap-2 items-center">
            <Avatar src={comment?.userAvatar}/>
            <div className="font-semibold">
              {comment?.createdBy}
            </div>
            <div className="font-light">
              {moment(comment?.createdOn).fromNow()}
            </div>
          </div>
          <div className="mt-2">
              <Rating value={comment?.rating} readOnly />
          </div>
          <div className="ml-2">
              {comment?.content}
          </div>
      </div>
    );
  };
  
  export default ProductCommentListItem;