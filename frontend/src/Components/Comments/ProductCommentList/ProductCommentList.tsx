import React from "react";
import { CommentGet } from "../../../Models/Comment";
import ProductCommentListItem from "../ProductCommentListItem/ProductCommentListItem";
import Heading from "../../ProductCard/ProductCardDetails/Heading/Heading";



type Props = {
    comments: CommentGet[];
  };
  
  const ProductCommentList = ({ comments }: Props) => {
    return (
      <>
        <Heading title="Product Review"/>
        <div className="text-sm mt-2">
          {comments
            ? comments.map((comment) => {
              return <ProductCommentListItem key={comment.id} comment={comment} />;
              })
            : ""}
        </div>
      </>
    );
  };
  
  export default ProductCommentList;