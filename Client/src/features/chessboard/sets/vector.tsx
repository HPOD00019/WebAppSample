import type { ChessboardOptions, PieceRenderObject } from "react-chessboard"



export const whiteRook = () => {
    return(
    <svg  
	viewBox="-10 -10 120 120" enable-background="new 0 0 100 100" >
<g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M77,89c0,2.2-1.8,4-4,4H27c-2.2,0-4-1.8-4-4l0,0
			c0-2.2,1.8-4,4-4h46C75.2,85,77,86.8,77,89L77,89z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M67,32c0,1.65-1.35,3-3,3H36c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h28C65.65,29,67,30.35,67,32L67,32z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M72,82c0,1.65-1.35,3-3,3H31c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h38C70.65,79,72,80.35,72,82L72,82z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M31,10v16c0,1.65,1.35,3,3,3h32c1.65,0,3-1.35,3-3
			V10"/>
	</g>
	<polyline fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" points="69,16 69,10 62,10 62,16 56,16 56,10 
		44,10 44,16 38,16 38,10 31,10 31,16 	"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M68.262,79C66.464,72.751,62,70.139,62,35H38
		c0,35.139-4.464,37.751-6.262,44H68.262z"/>
</g>
</svg>        
    )
}
export const whiteKnight = () => {
    return(
        <svg 
	 viewBox="-10 -10 120 120" enable-background="new 0 0 100 100" >
<g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M76,89c0,2.2-1.8,4-4,4H28c-2.2,0-4-1.8-4-4l0,0
			c0-2.2,1.8-4,4-4h44C74.2,85,76,86.8,76,89L76,89z"/>
	</g>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M71.86,47.477C68.391,55.789,64,66.297,64,79H36
		l-2-22c7.18,0,13-5.82,13-13"/>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M71,82c0,1.65-1.35,3-3,3H32c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h36C69.65,79,71,80.35,71,82L71,82z"/>
	</g>
	<line fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" x1="27" y1="45" x2="32" y2="41"/>
	<line fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" x1="41" y1="24" x2="46" y2="21"/>
	<line fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" x1="24" y1="39" x2="27" y2="37"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M52,20V10c13.255,0,24,10.745,24,24
		S65.255,58,52,58"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M20,38c0-8.24,16-14.169,16-16c0-3.866,3.134-7,7-7
		h9"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M20,38c0,3.866,3.134,7,7,7"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M28,45c0,2.209,1.791,4,4,4s4-1.791,4-4l7-3
		c2.539,2.539,6.654,2.539,9.192,0s2.539-6.654,0-9.192"/>
</g>
</svg>
    )
}
export const whiteBishop = () => {
    return(
        <svg  
	viewBox="-10 -10 120 120" enable-background="new 0 0 100 100" >
<g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M73,89c0,2.2-1.8,4-4,4H31c-2.2,0-4-1.8-4-4l0,0
			c0-2.2,1.8-4,4-4h38C71.2,85,73,86.8,73,89L73,89z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M63,40c0,1.65-1.35,3-3,3H40c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h20C61.65,37,63,38.35,63,40L63,40z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M68,82c0,1.65-1.35,3-3,3H35c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h30C66.65,79,68,80.35,68,82L68,82z"/>
	</g>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M50,5c0,5-13,16-13,21s4,11,4,11h18c0,0,4-6,4-11
		S50,10,50,5z"/>
	<line fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" x1="48" y1="23" x2="44" y2="15"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M62.578,79c-0.942-5.738-3.17-8.413-3.528-36h-18.1
		c-0.358,27.587-2.586,30.262-3.528,36H62.578z"/>
</g>
</svg>
    )
}
export const whiteKing = () => {
    return(
        <svg  
	viewBox="-10 -10 120 120" enable-background="new 0 0 100 100" >
<g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M73,89c0,2.2-1.8,4-4,4H31c-2.2,0-4-1.8-4-4l0,0
			c0-2.2,1.8-4,4-4h38C71.2,85,73,86.8,73,89L73,89z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M63,36c0,1.65-1.35,3-3,3H40c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h20C61.65,33,63,34.35,63,36L63,36z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M68,82c0,1.65-1.35,3-3,3H35c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h30C66.65,79,68,80.35,68,82L68,82z"/>
	</g>
	<polygon fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" points="63,20 59,33 41,33 37,20 50,15 	"/>
	<line fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" x1="50" y1="15" x2="50" y2="5"/>
	<line fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" x1="46" y1="9" x2="54" y2="9"/>
	<line fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" x1="38" y1="23" x2="62" y2="23"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M62.61,79c-0.946-6.487-3.306-10.059-3.583-40
		H40.973c-0.277,29.941-2.637,33.513-3.583,40H62.61z"/>
</g>
</svg>
    )
}
export const whiteQueen = () => {
    return(<svg  
	viewBox="-10 -10 120 120" enable-background="new 0 0 100 100" >
<g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M73,89c0,2.2-1.8,4-4,4H31c-2.2,0-4-1.8-4-4l0,0
			c0-2.2,1.8-4,4-4h38C71.2,85,73,86.8,73,89L73,89z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M63,36c0,1.65-1.35,3-3,3H40c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h20C61.65,33,63,34.35,63,36L63,36z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M68,82c0,1.65-1.35,3-3,3H35c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h30C66.65,79,68,80.35,68,82L68,82z"/>
	</g>
	<polyline fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" points="34,15 41,33 59,33 66,15 	"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M36,20c0-1.657,6.268-3,14-3s14,1.343,14,3"/>
	<circle fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" cx="50" cy="13" r="4"/>
	<line fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" x1="37" y1="23" x2="63" y2="23"/>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M62.61,79c-0.946-6.487-3.306-10.059-3.583-40
		H40.973c-0.277,29.941-2.637,33.513-3.583,40H62.61z"/>
</g>
</svg>)
}
export const whitePawn = () => {
    return(
        <svg  
	viewBox="-10 -10 120 120" enable-background="new 0 0 100 100" >
<g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M73,89c0,2.2-1.8,4-4,4H31c-2.2,0-4-1.8-4-4l0,0
			c0-2.2,1.8-4,4-4h38C71.2,85,73,86.8,73,89L73,89z"/>
	</g>
	<circle fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" cx="50" cy="22" r="13"/>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M63,38c0,1.65-1.35,3-3,3H40c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h20C61.65,35,63,36.35,63,38L63,38z"/>
	</g>
	<g>
		<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M68,82c0,1.65-1.35,3-3,3H35c-1.65,0-3-1.35-3-3
			l0,0c0-1.65,1.35-3,3-3h30C66.65,79,68,80.35,68,82L68,82z"/>
	</g>
	<path fill="none" stroke="#000000" stroke-width="4" stroke-miterlimit="10" d="M63.99,79C62.052,74.959,58,74.478,58,41H42
		c0,33.478-4.052,33.959-5.99,38H63.99z"/>
</g>
</svg>
    )
}
export const blackRook = () => {
    return(
    
<svg fill="#000000" 
	viewBox="-10 -10 120 120" enable-background="new 0 0 100 100">
<path d="M31,25V10h7v6h6v-6h12v6h6v-6h7v15c0,2.2-1.8,4-4,4H35C32.8,29,31,27.2,31,25z M65,34c1.1,0,2-0.9,2-2s-0.9-2-2-2H35
	c-1.1,0-2,0.9-2,2s0.9,2,2,2H65z M30,84h40c1.1,0,2-0.9,2-2s-0.9-2-2-2H30c-1.1,0-2,0.9-2,2S28.9,84,30,84z M73,85H27
	c-2.2,0-4,1.8-4,4s1.8,4,4,4h46c2.2,0,4-1.8,4-4S75.2,85,73,85z M68.262,79C66.464,72.752,62,70.139,62,35H38
	c0,35.139-4.464,37.752-6.262,44H68.262z"/>
</svg>        
    )
}
export const blackKnight = () => {
    return(
        <svg fill="#000000"  
	viewBox="-10 -10 120 120" enable-background="new 0 0 100 100">
<path d="M31.375,40.219l1.249,1.563l-5.475,4.379C27.676,48.357,29.645,50,32,50c2.527,0,4.622-1.885,4.954-4.32l5.849-2.508
	c2.944,2.451,7.337,2.297,10.097-0.465c2.924-2.924,2.924-7.682,0-10.606l0.707-0.707c1.605,1.605,2.49,3.739,2.49,6.011
	c0,1.328-0.311,2.607-0.884,3.764l0,0c-0.196,0.396-0.425,0.775-0.681,1.14c-0.024,0.034-0.05,0.066-0.074,0.1
	c-0.256,0.353-0.536,0.692-0.851,1.007c-0.276,0.276-0.57,0.523-0.873,0.752c-0.07,0.053-0.143,0.101-0.213,0.15
	c-0.252,0.179-0.51,0.344-0.775,0.492c-1.508,0.844-3.216,1.203-4.894,1.057C45.944,52.158,40.545,57,34,57l2,22h28
	c0-9.957,2.698-18.563,5.535-25.822C64.908,57.412,58.751,60,52,60v-1c13.785,0,25-11.215,25-25S65.785,9,52,9h-1v10h-1v-4h-7
	c-3.866,0-7,3.134-7,7c0,1.831-16,7.76-16,16c0,3.38,2.395,6.199,5.58,6.855L31.375,40.219z M45.485,20.143l1.029,1.715l-5,3
	l-1.029-1.715L45.485,20.143z M23.445,38.168l3-2l1.109,1.664l-3,2L23.445,38.168z M69,80c1.1,0,2,0.9,2,2s-0.9,2-2,2H31
	c-1.1,0-2-0.9-2-2s0.9-2,2-2H69z M76,89c0,2.2-1.8,4-4,4H28c-2.2,0-4-1.8-4-4s1.8-4,4-4h44C74.2,85,76,86.8,76,89z"/>
</svg>
    )
}
export const blackBishop = () => {
    return(
        <svg fill="#000000" 
	 viewBox="-10 -10 120 120" enable-background="new 0 0 100 100">
<path d="M37,40c0-1.1,0.9-2,2-2h22c1.1,0,2,0.9,2,2s-0.9,2-2,2H39C37.9,42,37,41.1,37,40z M34,84h32c1.1,0,2-0.9,2-2s-0.9-2-2-2H34
	c-1.1,0-2,0.9-2,2S32.9,84,34,84z M69,85H31c-2.2,0-4,1.8-4,4s1.8,4,4,4h38c2.2,0,4-1.8,4-4S71.2,85,69,85z M40.95,43
	c-0.358,27.588-2.586,30.262-3.528,36h25.156c-0.942-5.738-3.17-8.412-3.528-36H40.95z M59,37c0,0,4-6,4-11
	c0-4.411-10.112-13.488-12.496-19h-1.008c-0.871,2.015-2.776,4.506-4.842,7.072l4.24,8.48l-1.789,0.895l-3.834-7.668
	C40.1,19.686,37,23.558,37,26c0,5,4,11,4,11H59z"/>
</svg>
    )
}
export const blackKing = () => {
    return(
<svg fill="#000000" viewBox="-10 -10 120 120" enable-background="new 0 0 100 100">
<path d="M37,36c0-1.1,0.9-2,2-2h22c1.1,0,2,0.9,2,2s-0.9,2-2,2H39C37.9,38,37,37.1,37,36z M34,84h32c1.1,0,2-0.9,2-2s-0.9-2-2-2H34
	c-1.1,0-2,0.9-2,2S32.9,84,34,84z M69,85H31c-2.2,0-4,1.8-4,4s1.8,4,4,4h38c2.2,0,4-1.8,4-4S71.2,85,69,85z M37,20l0.615,2h24.77
	L63,20l-11-4.23V11h2V7h-2V5h-4v2h-2v4h2v4.77L37,20z M59,33l3.077-10H37.923L41,33H59z M40.973,39
	c-0.277,29.941-2.637,33.514-3.583,40H62.61c-0.946-6.486-3.306-10.059-3.583-40H40.973z"/>
</svg>
    )
}
export const blackQueen = () => {
    return(
<svg fill="#000000" viewBox="-10 -10 120 120" enable-background="new 0 0 100 100" >
<path d="M63,36c0,1.1-0.9,2-2,2H39c-1.1,0-2-0.9-2-2s0.9-2,2-2h22C62.1,34,63,34.9,63,36z M34,84h32c1.1,0,2-0.9,2-2s-0.9-2-2-2H34
	c-1.1,0-2,0.9-2,2S32.9,84,34,84z M69,85H31c-2.2,0-4,1.8-4,4s1.8,4,4,4h38c2.2,0,4-1.8,4-4S71.2,85,69,85z M40.973,39
	c-0.277,29.941-2.637,33.514-3.583,40H62.61c-0.946-6.486-3.306-10.059-3.583-40H40.973z M34.965,23l3.89,10h22.291l3.89-10H34.965z
	 M65.424,22l2.44-6.275l-3.729-1.449l-1.361,3.501c-1.851-0.886-5.641-1.543-10.218-1.724C53.432,15.318,54,14.23,54,13
	c0-2.209-1.791-4-4-4s-4,1.791-4,4c0,1.23,0.568,2.318,1.443,3.053c-4.577,0.181-8.367,0.838-10.218,1.724l-1.361-3.501
	l-3.729,1.449L34.576,22H65.424z"/>
</svg>

    )
}
export const blackPawn = () => {
    return(
    <svg fill="#000000" 
	viewBox="-10 -10 120 120" enable-background="new 0 0 100 100">
<path d="M37,38c0-1.1,0.9-2,2-2h22c1.1,0,2,0.9,2,2s-0.9,2-2,2H39C37.9,40,37,39.1,37,38z M34,84h32c1.1,0,2-0.9,2-2s-0.9-2-2-2H34
	c-1.1,0-2,0.9-2,2S32.9,84,34,84z M69,85H31c-2.2,0-4,1.8-4,4s1.8,4,4,4h38c2.2,0,4-1.8,4-4S71.2,85,69,85z M50,35
	c7.18,0,13-5.82,13-13S57.18,9,50,9s-13,5.82-13,13S42.82,35,50,35z M58,41H42c0,33.478-4.052,33.959-5.99,38H63.99
	C62.052,74.959,58,74.478,58,41z"/>
</svg>
    )
}
const dropStyle: React.CSSProperties = {
    backgroundColor: 'rgb(64, 62, 62)',
}
const darkStyle: React.CSSProperties = {
    backgroundColor: 'gray',
}
  const whiteStyle: React.CSSProperties = {
    backgroundColor: 'white',
}

const boardPieces: PieceRenderObject = {
  wP: whitePawn,  
  wN: whiteKnight,  
  wB: whiteBishop,  
  wR: whiteRook,  
  wQ: whiteQueen,  
  wK: whiteKing,  

  bP: blackPawn,  
  bN: blackKnight,  
  bB: blackBishop,  
  bR: blackRook,  
  bQ: blackQueen,  
  bK: blackKing   
}

export const vectorOptions : ChessboardOptions = {
    
    dropSquareStyle: dropStyle,
    darkSquareStyle: darkStyle,
    lightSquareStyle: whiteStyle,
    pieces: boardPieces,
}