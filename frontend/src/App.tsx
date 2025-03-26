import './App.css';
import { CartProvider } from './context/CartContext';
import AddToCartPage from './pages/AddToCartPage';
import BooksPage from './pages/BooksPage';
import CartPage from './pages/CartPage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

function App() {
  return (
    <>
      <CartProvider>
        <Router>
          <Routes>
            <Route path="/" element={<BooksPage />} />
            <Route path="/cart" element={<CartPage />} />
            <Route
              path="/addToCart/:title/:bookID/:price"
              element={<AddToCartPage />}
            />
          </Routes>
        </Router>
      </CartProvider>
    </>
  );
}

export default App;
