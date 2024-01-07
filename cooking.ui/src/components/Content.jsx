import '../styles/Content-style.css'
// import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { } from '@fortawesome/free-solid-svg-icons';
import { } from '@fortawesome/free-regular-svg-icons';
import Challenges from './Challenges';

function Content() {
  return (
    <>
        <div className="Content">
            <h2 className='content-title'>Vorige Challenges</h2>
            <Challenges />
        </div>
    </>
  )
}

export default Content
