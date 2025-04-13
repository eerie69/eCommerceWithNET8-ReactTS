import React, { useEffect, useState } from "react";
import Hero from "../../Components/Hero/Hero";
import Container from "../../Components/Container/Container";
import HomeBanner from "../../Components/HomeBanner/HomeBanner";
import { ProductGet } from "../../Models/Product";
import { productGetAPI } from "../../Services/ProductService";
import Spinner from "../../Components/Spinners/Spinner";
import ProductCardList from "../../Components/ProductCard/ProductCardList/ProductCardList";
import { CommentGet } from "../../Models/Comment";


type Props = {};

const HomePage = (props: Props) => {
  const [products, setProduct] = useState<ProductGet[] | null>(null);
  const [loading, setLoading] = useState<boolean>();

  useEffect(() => {
      getProducts();
    }, []);

  const getProducts = () => {
      setLoading(true);
      productGetAPI().then((res) => {
        setLoading(false);
        setProduct(res?.data ?? []);
      });
    };

  return (
    <div className="p-8">
      <Container>
        <div>
          <HomeBanner />
        </div>
        <div>
        {loading ? <Spinner /> : <ProductCardList products={products!} />}
        </div>
      </Container>
    </div>
  );
};

export default HomePage;