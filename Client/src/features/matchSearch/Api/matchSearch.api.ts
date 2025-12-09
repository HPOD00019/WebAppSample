import { apiClient } from "@shared/api/ApiClient";
import type { matchRequest } from "../types/search.types";

export const matchSearchApi = {
    findMatch: (request: matchRequest) => apiClient.get('http://localhost:5003/Match/NewGame?_control=0')
}
