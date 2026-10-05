export interface AuthResponse {
  success: boolean;
  errorMessage?: string | null;
  accessToken?: string | null;
  refreshToken?: string | null;
}
