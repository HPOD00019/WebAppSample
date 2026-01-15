import { classicalOptions } from "@features/chessboard/sets/classical";
import { vectorOptions } from "@features/chessboard/sets/vector"
import type { boardStyle } from "@features/chessboard/types/chessboard.types";
import { FlatPanelButton } from "@shared/ui/FlatPanelButton";
import { Chessboard, type ChessboardOptions } from "react-chessboard"

export interface styleOptionProps extends React.HTMLAttributes<HTMLDivElement>{
    boardStyle: boardStyle;
    onStyleClicked: () => void;
}
export const StyleOption = (props: styleOptionProps) => {
    let styleOptions: ChessboardOptions = {

    }
    if(props.boardStyle == 'classical'){
        styleOptions = classicalOptions;
    }
    if(props.boardStyle == 'vector'){
        styleOptions = vectorOptions;
    }

    const options: ChessboardOptions = {
        ...styleOptions,
        chessboardRows: 1,
        chessboardColumns: 2,
        position: 'kK w - - 0 1',
        allowDrawingArrows: false,
        showNotation: false,
    }
    return(
        <div  style={{width: `250px`, height: `auto`, display: "flex", flexDirection: "row", backgroundColor: 'black'}}>
            <div style={{width: `150px`, height: `auto`}}>
                <Chessboard options={options} />
            </div>
            <div style={{width: '100px', display: 'flex', alignItems: "center", justifyContent: "center", flexDirection: "column", backgroundColor: '#1E293B'}} onClick={() => props.onStyleClicked()}>
                <FlatPanelButton style={{height: '100%'}}>{props.boardStyle}</FlatPanelButton>
            </div>
        </div>
    )
}