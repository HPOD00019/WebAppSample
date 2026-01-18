
import { getTurn } from '@shared/utils/getTurn';
import Countdown from 'react-countdown';
export interface TimerProps{
    totalMilliseconds: number,
    isWhite: boolean,
    fen?: string,
}  
export const Timer = (props : TimerProps) => {
    let isCounting = false;
    if(props.fen){
        if(props.fen != "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"){
            const isWhiteTurn = getTurn(props.fen);
            if(isWhiteTurn){
                if(props.isWhite){
                    isCounting = true;
                }
            }
            else{
                if(!props.isWhite){
                    isCounting = true;
                }
            }
        }
    }
    
    console.log(props.fen);
    console.log(props.isWhite, " : ", isCounting);
    return(
    <div className='timer' >
        <Countdown 
            autoStart={isCounting}
            key={`${props.isWhite}-${isCounting}`}
            date={Date.now() + props.totalMilliseconds} 
            intervalDelay={100} 
            precision={3} 
            renderer={({ hours, minutes, seconds, completed }) => {
                if (completed) {
                    return <span className='timer-text' >0:00</span>;
                }
                return (
                <span className='timer-text'>
                    {minutes}:{seconds.toString().padStart(2, '0')}
                </span>
                );
            }}
            onTick={({ total, minutes, seconds }) => {
                if (minutes === 0 && seconds === 30) {
                    console.log('Осталось 30 секунд!');
                }
            }}
            onComplete={() => {}} 
        />
    </div>
    )
}