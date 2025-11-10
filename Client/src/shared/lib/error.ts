export const ErrorCode = {
    PERMISSION_DENIED: 403,
    USER_EXISTS: 410,
} as const;
export type ErrorCodeType = typeof ErrorCode[keyof typeof ErrorCode];

export interface BaseError{
    code: ErrorCodeType;
    message? : string;
}