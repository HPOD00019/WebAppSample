import type { pieceColor, pieceType  } from '../types/chessboard.types'
import { useFocus } from '@shared/utils/useFocus';
export interface PieceProps extends React.HTMLAttributes<HTMLDivElement>{
    side : pieceColor;
    kind : pieceType;
    pieceSrc : string;
    pieceId: number;
    isClickable: boolean | null;
    onPieceFocus?: () => void;
    onPieceLoseFocus?: () => void;
}

export const Piece = (props : PieceProps) => {
    const { side, kind} = props;
    const pieceClass = `${side} ${kind}`;
    
    const _style = props.isClickable==false? {...props.style, pointerEvents: 'none' as const} : {...props.style}

    const {hasFocus, setElementRef} = useFocus(props.onPieceFocus, props.onPieceLoseFocus);
    const viewBoxSettings = `0 0 100 100`;
    return (
        <div 
            style= {_style}
            className={pieceClass}
            ref={setElementRef}
        >
            <svg 
                viewBox={viewBoxSettings}
                preserveAspectRatio="xMidYMid meet"
            >
                <image 
                    href={props.pieceSrc} 
                    width="100" 
                    height="100" 
                    preserveAspectRatio="xMidYMid meet"
                />    
            </svg>
            
        </div>
    )
}