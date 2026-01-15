export interface OpponentProps{
    name?: string;
    rating?: number;
}

export const Opponent = (props: OpponentProps) => {
    let name = 'Opponent';
    let rating = 1000;
    if(props.name) name = props.name;
    if(props.rating) rating = props.rating;
    return(
        <p style={{fontSize: '25px',fontWeight:'bold' ,marginRight: 'auto'}}>{name} ({rating})</p>
    )
}