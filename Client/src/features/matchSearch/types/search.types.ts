export interface matchRequest{
    timeControl: number;
}
export interface matchResponse{
    link: string;
    isWhite: boolean;
}
export const timeControl = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7;
export const timeControls = [0,1,2,3,4,5,6,7,]