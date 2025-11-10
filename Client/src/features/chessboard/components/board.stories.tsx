/*
import type {Meta, StoryObj } from '@storybook/react';
import boardImg from "../assets/board.png";
import whiteRook from "@shared/pieceSets/white_rook.svg";

import {Board } from './board.tsx';

const meta = {
    title : 'Components/Board',
    component : Board,
} satisfies Meta<typeof Board>;

export default meta;

type Story = StoryObj<typeof Board>

export const Default : Story =  {
     args: {
        boardImgSrc: "",
        pieces: [ 
            {
                id: 1,
                Position: { x: 1, y: 1 },
                piece: "rook",
                color: "white",
                svgSource: whiteRook
            }
        ],
        onPositionChange: () => console.log('Position changes:')
    }
}

*/

