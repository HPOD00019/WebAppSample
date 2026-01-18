export const storeAccessToken = (token: string) => {
    localStorage.setItem("accessToken", token);
}
export const getAccessToken = () => {
    return localStorage.getItem('accessToken');
}
export const storeRefreshToken = (token : string ) => {
    localStorage.setItem("refreshToken", token);
}
export const storeId = (ID : string ) => {
    localStorage.setItem("Id", ID);
}
export const getStoredId = (): number => {
    const id = localStorage.getItem('Id');
    if(!id) return -1;
    const ID = parseInt(id);
    return ID;
}
export const getRefreshToken = () => {
    return localStorage.getItem('refreshToken');
}