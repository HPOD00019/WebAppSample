export const storeAccessToken = (token: string) => {
    localStorage.setItem("accessToken", token);
}
export const getAccessToken = () => {
    return localStorage.getItem('accessToken');
}
export const storeRefreshToken = (token : string ) => {
    localStorage.setItem("refreshToken", token);
}
export const getRefreshToken = () => {
    return localStorage.getItem('refreshToken');
}