import './App.css';
import { ToastContainer } from 'react-toastify';
import { UserProvider } from './Context/useAuth';
import Navbar from './Components/Navbar/Navbar';
import { Outlet } from 'react-router';
import "react-toastify/dist/ReactToastify.css";
import Footer from './Components/Footer/Footer';
import CartProvider from './Providers/CartProvider';



function App() {
  return (
    <>
     
     <UserProvider>
        <CartProvider>
          <Navbar />
          <Outlet />
          <Footer />
          <ToastContainer />
        </CartProvider>
      </UserProvider>
    </>
  );
}

export default App;
