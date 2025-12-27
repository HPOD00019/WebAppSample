import { apiClient } from "@shared/api/ApiClient";
import type { matchRequest } from "../types/search.types";
import type { ApiResponse } from "@shared/types/api";

export const matchSearchApi = {
    findMatch: async (request: matchRequest) : Promise<ApiResponse<string>> => {
        const response = (await apiClient.get(`http://localhost:5003/Match/NewGame?_control=${request.timeControl}`)).data as ApiResponse<string>;
        return response;
    },
    pingMatch: async (request: matchRequest) : Promise<ApiResponse<string>> => {
        const response = (await apiClient.get(`http://localhost:5003/Match/PingMatchRequest?_control=${request.timeControl}`)).data as ApiResponse<string>;
        return response;
    },
}
