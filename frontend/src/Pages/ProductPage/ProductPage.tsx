import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import { productGetByIdAPI } from '../../Services/ProductService';
import { ProductGet } from '../../Models/Product';
import Spinner from '../../Components/Spinners/Spinner';
import ProductCardDetails from '../../Components/ProductCard/ProductCardDetails/ProductCardDetails';
import Container from '../../Components/Container/Container';
import ProductComment from '../../Components/Comments/ProductComment/ProductComment';
import { useCart } from '../../Context/useCart';



interface Props {}

const ProductPage = (props: Props) => {
  let { id } = useParams();
  
  const [products, setProduct] = useState<ProductGet | null>(null);
  const [loading, setLoading] = useState<boolean>();
  
  

  useEffect(() => {
    getProducts();
  }, []);


  const getProducts = () => {
      setLoading(true);
      productGetByIdAPI(id).then((res) => {
        setLoading(false);
        setProduct(res?.data!);
      });
      
    };

    if (!products) {
      return <div>Product not found or loading...</div>;
    }

  return (
    <>
      <Container>
        <div className="flex flex-col">
          {loading ? <Spinner /> : <ProductCardDetails product={products!} />}
        </div>
        <div className="flex flex-col mt-20 gap-4">
          <div>Add Rating</div>
        {products?.title &&  <ProductComment productSymbol={products?.title!}  />}
        </div>
      </Container>
    </>
  )
}

export default ProductPage;