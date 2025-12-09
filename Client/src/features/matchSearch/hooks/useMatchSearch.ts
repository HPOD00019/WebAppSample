import { matchSearchApi } from "../Api/matchSearch.api"
import type { matchRequest } from "../types/search.types";

export const useMatchSearch = (): [(n: number) => void] => {
    const find = (n: number) => {
        const f: matchRequest = {timeControl: n}
        matchSearchApi.findMatch(f);
    }
    return [(n:number) => find(n)];
}