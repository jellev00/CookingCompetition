import '../styles/Hero-style.css'
import wave from '../assets/wave.png'
import cookie from '../assets/Image-challanges.png'

function Hero() {
  return (
    <>
        <div className="Hero">
            <img src={wave} alt="" className='wave'/>
            <div className="Hero-main">
                <div className="Left-section-hero">
                    <p className='sub-txt'>Challenge van de Maand</p>
                    <h1>Wie maakt de grappigste cupcakes ever?</h1>
                    <button className='Action-btn'>
                        <p className="btn-txt">doe mee!</p>
                    </button>
                </div>
                <div className="Right-section-hero">
                    <div className="Circle">
                        <img src={cookie} alt="" className='Image-cookie'/>
                    </div>
                </div>
            </div>
        </div>
    </>
  )
}

export default Hero