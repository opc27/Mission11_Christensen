import { useNavigate, useParams } from 'react-router-dom';
import WelcomeBand from '../components/Welcome';
import { useCart } from '../context/CartContext';
import { CartItem } from '../types/CartItem';

function AddToCartPage() {
  const navigate = useNavigate();
  const { title, bookID, price } = useParams();
  const { addToCart } = useCart();

  const handleAddToCart = () => {
    const newItem: CartItem = {
      bookID: Number(bookID),
      title: title || 'No Book Found',
      price: Number(price),
    };
    addToCart(newItem);
    navigate('/cart');
  };

  return (
    <>
      <WelcomeBand />
      <h2>Adding {title} to Cart</h2>
      <div>
        <p>Price: ${Number(price)}</p>
        <button onClick={handleAddToCart}>Add to Cart</button>
      </div>

      <button onClick={() => navigate(-1)}>Go Back</button>
    </>
  );
}

export default AddToCartPage;
