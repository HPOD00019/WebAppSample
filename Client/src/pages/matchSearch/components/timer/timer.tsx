
import Countdown from 'react-countdown';
export interface TimerProps{
    totalMilliseconds: number,
    
}  
export const Timer = (props : TimerProps) => {
    return(
    <div className='timer' >

        <Countdown 
            date={Date.now() + props.totalMilliseconds} 
            intervalDelay={100} 
            precision={3} 
            renderer={({ hours, minutes, seconds, completed }) => {
                if (completed) {
                return <span>Время вышло!</span>;
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
            onComplete={() => alert('Время вышло!')} 
        />
    </div>
    )
}