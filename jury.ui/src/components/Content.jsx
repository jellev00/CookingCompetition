import '../styles/Content-style.css';
import JuryScreen from './JuryScreen';
import wave from '../assets/wave.png'

function Content() {
  return (
    <>
        <div className="Content">
            <img src={wave} alt="" className='wave'/>
            <div className='JureScreenContent'>
                <JuryScreen />
            </div>
        </div>
    </>
  )
}

export default Content
