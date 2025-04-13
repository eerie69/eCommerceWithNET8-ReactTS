import React, { useEffect, useState } from "react";
import { CommentGet } from "../../../Models/Comment";
import { commentGetAPI, commentPostAPI } from "../../../Services/CommentService";
import { toast } from "react-toastify";
import Spinner from "../../Spinners/Spinner";
import StockCommentList from "../ProductCommentList/ProductCommentList";
import StockCommentForm from "./ProductCommentForm/ProductCommentForm";




type Props = {
    productSymbol: string | undefined;
  };
  
  type CommentFormInputs = {
    rating: number;
    content: string;
  };
  
  const StockComment = ({ productSymbol }: Props) => {
    const [comments, setComment] = useState<CommentGet[] | null>(null);
    const [loading, setLoading] = useState<boolean>();
  
    useEffect(() => {
      getComments();
      
    }, []);
  
    const handleComment = (e: CommentFormInputs) => {
      commentPostAPI(e.rating, e.content, productSymbol)
        .then((res) => {
          if (res) {
            toast.success("Comment created successfully!");
            getComments();
          }
        })
        .catch((e) => {
          toast.warning(e);
        });
    };
  
    const getComments = () => {
      setLoading(true);
      commentGetAPI(productSymbol).then((res) => {
        setLoading(false);
        setComment(res?.data!);
      });
    };
    return (
      <div className="flex flex-col">
        <StockCommentForm symbol={productSymbol} handleComment={handleComment} />
        {loading ? <Spinner /> : <StockCommentList comments={comments!} />}
      </div>
    );
  };
  
  export default StockComment;