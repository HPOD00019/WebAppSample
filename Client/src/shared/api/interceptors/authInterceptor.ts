import type { InternalAxiosRequestConfig } from "axios";

export const authInterceptor = (config: InternalAxiosRequestConfig) => {
    const token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyIiwibmJmIjoxNzY1MDg3Mjk1LCJleHAiOjE3NjUwOTA4OTUsImlhdCI6MTc2NTA4NzI5NX0.hiCCXOakG1s1F4O-tCOrblTbNCTWZd6ooZWifgFl_jPtceGzBcBd4UQ1lLC-jRoPPagkwPcP0z3bHwN-IkB7rIAdaD--zykgMekfyq7hnpUBGN7EeNjDx9fxZ2OLm67hncnJdKhPZcMkAwX-J9-29EgNJQ0SrtBi2KUih4Nn-YrfanbATz40uhSGYZ4WlyQifg3eGgFmM2WX5LqL8R8WoDJhj5JnFzvAhUKxHSUQpTMZ1hwcJi1kvgbW2S61LLfNuEahyRMDFCIvit7jOzQi9Ho5JU3E00pCYtlxr-bZN8PqyClI8jjn0Y_LIhq0JFDxW98x5pCh9JVLcQoY-riSnw";
    config.headers.Authorization = `Bearer ${token}`;
    return config;
}