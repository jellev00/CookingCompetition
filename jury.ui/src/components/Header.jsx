import '../styles/Header-style.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch, faShoppingCart } from '@fortawesome/free-solid-svg-icons';
import { faUser, faHeart } from '@fortawesome/free-regular-svg-icons';
import collect from '../assets/Collect-&-Go.png';

function Header() {
  return (
    <>
      <header className='Header'>
        <div className="Left-section-header">
            <img className='Logo' src={collect} alt="logo" />
            <ul className='List'>
                <li className="List-Item">RECEPTEN</li>
                <li className="List-Item">PRODUCTEN</li>
                <li className="List-Item">PLANNER</li>
                <li className="List-Item">PREMIUM</li>
            </ul>
        </div>
        <div className="Right-section-header">
            <div className="Search-Bar">
                <p className="Search-Bar-Text">
                    Vind je recept of product
                </p>
                <FontAwesomeIcon icon={faSearch} className='Search Icon'/>
            </div>
            <FontAwesomeIcon icon={faUser} className='Icon'/>
            <FontAwesomeIcon icon={faHeart} className='Icon'/>
            <FontAwesomeIcon icon={faShoppingCart} className='Icon'/>
        </div>
      </header>
    </>
  )
}

export default Header