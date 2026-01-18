import type { ApiResponse } from "@shared/types/api";
import { matchSearchApi } from "../Api/matchSearch.api"
import type { matchRequest } from "../types/search.types";
import { useRef,  type RefObject } from "react";

export const useMatchSearch = (onMatchFound: (joinLink: string) => void, pingInterval: number): [RefObject<boolean>, (n: number) => void, (n: number) => Promise<ApiResponse<string>>] => {
    const isSearching = useRef(false);
    const requestNewMatch = async (n: number) => {
        if(isSearching.current === true)return "";
        const f: matchRequest = {timeControl: n}
        await matchSearchApi.findMatch(f);
        isSearching.current = true;
        const link = await pingForMatch(pingInterval, n);
        isSearching.current = false;
        onMatchFound(link);
    }
    const pingForMatch = async (interval: number, control: number): Promise<string> => {
        const request: matchRequest = { timeControl: control}
        let response: ApiResponse<string> = await matchSearchApi.pingMatch(request);
        if(response.success === true) return response.data === null ? response.data : "";
        else{
            while (response.success !== true){
                const request: matchRequest = { timeControl: control}
                response = await matchSearchApi.pingMatch(request);
                await new Promise(resolve => setTimeout(resolve, interval));
                if(isSearching.current == false) return "";
                console.log(response);
            }
            console.log(response.data);
            
            const answer = response.data === null ? "" : response.data;
            if(!answer) return "";
            return answer;
        }
    }
    const checkForActiveMatch = async (n: number): Promise<ApiResponse<string>> => {
        const request : matchRequest = {
            timeControl: n,
        }
        const response: ApiResponse<string> = await matchSearchApi.pingMatch(request);
        console.log(response);
        return response;
    }
    return [isSearching, (n:number) => requestNewMatch(n), checkForActiveMatch];
}