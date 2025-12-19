using AuthService.Application.Services;
using AuthService.Domain.Models;
using System.Diagnostics.CodeAnalysis;
using Xunit.Abstractions;

namespace AuthService.UnitTests
{
    public class TokenServiceTests
    {

        ITestOutputHelper _out;

        public TokenServiceTests(ITestOutputHelper Out)
        {
            _out = Out;
        }
        [Fact]
        public async Task TokenService_Refresh_Token_Guid_User_Id_generation_test()
        {
            var Test_privateKey = "-----BEGIN PRIVATE KEY-----MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCsDjWZHoeO8ooQKCmcVqlvjMdmD4Ft8wJs6CmB0vU1SDxPGxr0GdNWcT5kmVs2/WiZMtFP4qQNxBEXZNh9gY4cQOk33wS9rE9w/Si0pu5ppfykWHVWi5BHk4DHAyIRtD9E2SBCVeL2XO9EpvOJr2pjUTEo7mxJEJwTNGjV71HoTRS3hDCY+Ocg34E0phOwCuTCQ7llkUSu8vA0GsbClw0XUN+d4GkFuhW+P8oUXxQwYvh6SYfWxfiNRn5WxZz2fuH9Pnn53VeK7pXOBAf+vc8mamOrqoFtn4FeSQanDMdGswlAYeouKawO6SVAPMVCvPuokaNkOstj1+pqLAHk2Bt9AgMBAAECggEAEQhaaP1F490B1ZtMPJRds1PR8joVu4844xQmoRoDuVHHL/jvIFF62Piz+bHZfvueLRBZT+8QA5GF3vWA8wfDduuRdZJSk1OpYkua3ytpYwm8wCjw+2+B3ybB3bG4iofNUvZVrwt2lkyunnK9SEOWehzcYNYdioF7mfEbHGv+hUsL6whQ35tiKKPnq/6qLsRKi9XjiRRjhfIPkDS2Vobt3OlCXr5b+aObL1I6p9I6vDw5nzt6e9GCjAVwapXqaaIXR/koOmtvvL0YZ01oREQopeso2jwx27+qRZkpdnwIkDe7GSdATFIwHQyO1nkLJzm8hQAJL2GkHwYBiyyEe3VCtwKBgQDbGKUzeB0QbDCj7gJresP7zrYbqRYGAqrpDnPfL4ndJgXPWXXcDB5fpR5Pdz0R2xA2GwYHDxKWRq/etew//1dycasuY6OUJsbj6FFafsNMdSSmyGQQFnry14iYB/dZyusF4M5V+toiit0NNTjqI4HgPFXwUoPimppRyD2aMImXIwKBgQDJCS7DrgtPVgNaNJY/USl5iQ6tQFJeTITW84Cl2nyzhewA6UVwHHkiBsrlE81a9VryKiKtoEZ/OU+nQzbTwoQsT+CMCFpPb4M2lkTZUynKrnBhus8C6whARRNfFQgkNK9stloCywkgSKsrLC9BWtgGF7y9uetvXapLixbHMd383wKBgDP354+KyRIRJ4RquyY1S3FZ+bYb6D5quZFoPTHOO9w53ZzuMleMhFPZfZQJy3GFzZWX2VkpSRZeE+82paAUd/CMZ7csKSvF+t0qSMm3Uam8C4KH+7wZKPabCypMkW46BH8zcJ3ST5Vr7LzqR+K5o1/Fz/ieCexhEYOehIYjRy+TAoGBAJ3Xl+ALxtWDsx4gK9eK4gWKlvtwhTuE2MfsaBXQdhh+Dc6pWNutf2435xX1dkb0XXPFoXSxYqiBVwj8vQ+GqkAgdy17YWz9IQi681Ou+CEq1RHmmqKv5sqPcaY13S6QLywsAIAw6flvFPXQu0mVCge+m3Jbh37pC8xEaJ39iU4BAoGAGgllzW+TbDJKJX2Yeeck4DQlv1lCQJAMnt2HY4SikPVAs2T/NwQD043XRRzYnb2PoA2l4wtMLLVGGUP83pSSEKetgG1BUT/hAywwsxdmKSZyIzSGV4kwQP97GH69uTFKvpS+hczdWuhdJ3NtX55lDdifgKOcKbHmwggreso9dZs=-----END PRIVATE KEY-----";
            var Test_publicKey = "-----BEGIN PUBLIC KEY-----MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArA41mR6HjvKKECgpnFapb4zHZg+BbfMCbOgpgdL1NUg8Txsa9BnTVnE+ZJlbNv1omTLRT+KkDcQRF2TYfYGOHEDpN98EvaxPcP0otKbuaaX8pFh1VouQR5OAxwMiEbQ/RNkgQlXi9lzvRKbzia9qY1ExKO5sSRCcEzRo1e9R6E0Ut4QwmPjnIN+BNKYTsArkwkO5ZZFErvLwNBrGwpcNF1DfneBpBboVvj/KFF8UMGL4ekmH1sX4jUZ+VsWc9n7h/T55+d1Xiu6VzgQH/r3PJmpjq6qBbZ+BXkkGpwzHRrMJQGHqLimsDuklQDzFQrz7qJGjZDrLY9fqaiwB5NgbfQIDAQAB-----END PUBLIC KEY-----";

            var service = new TokenService(Test_publicKey, Test_privateKey);
            var TestUser = new User();

            TestUser.UserName = "TestName";
            TestUser.Role = UserRole.RegularUser;
            TestUser.Id = 5;
            var serviceResult = await service.GenerateRefreshToken(TestUser);
            Assert.True(serviceResult.IsSuccess);

            string ans = serviceResult.Value;
            _out.WriteLine(ans);
        }

        [Fact]
        public async Task TokenService_Rsa_Keys_correct_test()
        {
            var Test_privateKey = "-----BEGIN PRIVATE KEY-----MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCsDjWZHoeO8ooQKCmcVqlvjMdmD4Ft8wJs6CmB0vU1SDxPGxr0GdNWcT5kmVs2/WiZMtFP4qQNxBEXZNh9gY4cQOk33wS9rE9w/Si0pu5ppfykWHVWi5BHk4DHAyIRtD9E2SBCVeL2XO9EpvOJr2pjUTEo7mxJEJwTNGjV71HoTRS3hDCY+Ocg34E0phOwCuTCQ7llkUSu8vA0GsbClw0XUN+d4GkFuhW+P8oUXxQwYvh6SYfWxfiNRn5WxZz2fuH9Pnn53VeK7pXOBAf+vc8mamOrqoFtn4FeSQanDMdGswlAYeouKawO6SVAPMVCvPuokaNkOstj1+pqLAHk2Bt9AgMBAAECggEAEQhaaP1F490B1ZtMPJRds1PR8joVu4844xQmoRoDuVHHL/jvIFF62Piz+bHZfvueLRBZT+8QA5GF3vWA8wfDduuRdZJSk1OpYkua3ytpYwm8wCjw+2+B3ybB3bG4iofNUvZVrwt2lkyunnK9SEOWehzcYNYdioF7mfEbHGv+hUsL6whQ35tiKKPnq/6qLsRKi9XjiRRjhfIPkDS2Vobt3OlCXr5b+aObL1I6p9I6vDw5nzt6e9GCjAVwapXqaaIXR/koOmtvvL0YZ01oREQopeso2jwx27+qRZkpdnwIkDe7GSdATFIwHQyO1nkLJzm8hQAJL2GkHwYBiyyEe3VCtwKBgQDbGKUzeB0QbDCj7gJresP7zrYbqRYGAqrpDnPfL4ndJgXPWXXcDB5fpR5Pdz0R2xA2GwYHDxKWRq/etew//1dycasuY6OUJsbj6FFafsNMdSSmyGQQFnry14iYB/dZyusF4M5V+toiit0NNTjqI4HgPFXwUoPimppRyD2aMImXIwKBgQDJCS7DrgtPVgNaNJY/USl5iQ6tQFJeTITW84Cl2nyzhewA6UVwHHkiBsrlE81a9VryKiKtoEZ/OU+nQzbTwoQsT+CMCFpPb4M2lkTZUynKrnBhus8C6whARRNfFQgkNK9stloCywkgSKsrLC9BWtgGF7y9uetvXapLixbHMd383wKBgDP354+KyRIRJ4RquyY1S3FZ+bYb6D5quZFoPTHOO9w53ZzuMleMhFPZfZQJy3GFzZWX2VkpSRZeE+82paAUd/CMZ7csKSvF+t0qSMm3Uam8C4KH+7wZKPabCypMkW46BH8zcJ3ST5Vr7LzqR+K5o1/Fz/ieCexhEYOehIYjRy+TAoGBAJ3Xl+ALxtWDsx4gK9eK4gWKlvtwhTuE2MfsaBXQdhh+Dc6pWNutf2435xX1dkb0XXPFoXSxYqiBVwj8vQ+GqkAgdy17YWz9IQi681Ou+CEq1RHmmqKv5sqPcaY13S6QLywsAIAw6flvFPXQu0mVCge+m3Jbh37pC8xEaJ39iU4BAoGAGgllzW+TbDJKJX2Yeeck4DQlv1lCQJAMnt2HY4SikPVAs2T/NwQD043XRRzYnb2PoA2l4wtMLLVGGUP83pSSEKetgG1BUT/hAywwsxdmKSZyIzSGV4kwQP97GH69uTFKvpS+hczdWuhdJ3NtX55lDdifgKOcKbHmwggreso9dZs=-----END PRIVATE KEY-----";
            var Test_publicKey = "-----BEGIN PUBLIC KEY-----MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArA41mR6HjvKKECgpnFapb4zHZg+BbfMCbOgpgdL1NUg8Txsa9BnTVnE+ZJlbNv1omTLRT+KkDcQRF2TYfYGOHEDpN98EvaxPcP0otKbuaaX8pFh1VouQR5OAxwMiEbQ/RNkgQlXi9lzvRKbzia9qY1ExKO5sSRCcEzRo1e9R6E0Ut4QwmPjnIN+BNKYTsArkwkO5ZZFErvLwNBrGwpcNF1DfneBpBboVvj/KFF8UMGL4ekmH1sX4jUZ+VsWc9n7h/T55+d1Xiu6VzgQH/r3PJmpjq6qBbZ+BXkkGpwzHRrMJQGHqLimsDuklQDzFQrz7qJGjZDrLY9fqaiwB5NgbfQIDAQAB-----END PUBLIC KEY-----";

            var service = new TokenService(Test_publicKey, Test_privateKey);
            var TestUser = new User();

            TestUser.UserName = "TestName";
            TestUser.Role = UserRole.RegularUser;
            var serviceResult = await service.GenerateRefreshToken(TestUser);
            Assert.True(serviceResult.IsSuccess);

            string ans = serviceResult.Value;
            _out.WriteLine(ans);
        }

        [Fact]
        public async Task TokenService_Rsa_Refresh_Token_Signature_Correct_test()
        {
            var Test_privateKey = "-----BEGIN PRIVATE KEY-----MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCsDjWZHoeO8ooQKCmcVqlvjMdmD4Ft8wJs6CmB0vU1SDxPGxr0GdNWcT5kmVs2/WiZMtFP4qQNxBEXZNh9gY4cQOk33wS9rE9w/Si0pu5ppfykWHVWi5BHk4DHAyIRtD9E2SBCVeL2XO9EpvOJr2pjUTEo7mxJEJwTNGjV71HoTRS3hDCY+Ocg34E0phOwCuTCQ7llkUSu8vA0GsbClw0XUN+d4GkFuhW+P8oUXxQwYvh6SYfWxfiNRn5WxZz2fuH9Pnn53VeK7pXOBAf+vc8mamOrqoFtn4FeSQanDMdGswlAYeouKawO6SVAPMVCvPuokaNkOstj1+pqLAHk2Bt9AgMBAAECggEAEQhaaP1F490B1ZtMPJRds1PR8joVu4844xQmoRoDuVHHL/jvIFF62Piz+bHZfvueLRBZT+8QA5GF3vWA8wfDduuRdZJSk1OpYkua3ytpYwm8wCjw+2+B3ybB3bG4iofNUvZVrwt2lkyunnK9SEOWehzcYNYdioF7mfEbHGv+hUsL6whQ35tiKKPnq/6qLsRKi9XjiRRjhfIPkDS2Vobt3OlCXr5b+aObL1I6p9I6vDw5nzt6e9GCjAVwapXqaaIXR/koOmtvvL0YZ01oREQopeso2jwx27+qRZkpdnwIkDe7GSdATFIwHQyO1nkLJzm8hQAJL2GkHwYBiyyEe3VCtwKBgQDbGKUzeB0QbDCj7gJresP7zrYbqRYGAqrpDnPfL4ndJgXPWXXcDB5fpR5Pdz0R2xA2GwYHDxKWRq/etew//1dycasuY6OUJsbj6FFafsNMdSSmyGQQFnry14iYB/dZyusF4M5V+toiit0NNTjqI4HgPFXwUoPimppRyD2aMImXIwKBgQDJCS7DrgtPVgNaNJY/USl5iQ6tQFJeTITW84Cl2nyzhewA6UVwHHkiBsrlE81a9VryKiKtoEZ/OU+nQzbTwoQsT+CMCFpPb4M2lkTZUynKrnBhus8C6whARRNfFQgkNK9stloCywkgSKsrLC9BWtgGF7y9uetvXapLixbHMd383wKBgDP354+KyRIRJ4RquyY1S3FZ+bYb6D5quZFoPTHOO9w53ZzuMleMhFPZfZQJy3GFzZWX2VkpSRZeE+82paAUd/CMZ7csKSvF+t0qSMm3Uam8C4KH+7wZKPabCypMkW46BH8zcJ3ST5Vr7LzqR+K5o1/Fz/ieCexhEYOehIYjRy+TAoGBAJ3Xl+ALxtWDsx4gK9eK4gWKlvtwhTuE2MfsaBXQdhh+Dc6pWNutf2435xX1dkb0XXPFoXSxYqiBVwj8vQ+GqkAgdy17YWz9IQi681Ou+CEq1RHmmqKv5sqPcaY13S6QLywsAIAw6flvFPXQu0mVCge+m3Jbh37pC8xEaJ39iU4BAoGAGgllzW+TbDJKJX2Yeeck4DQlv1lCQJAMnt2HY4SikPVAs2T/NwQD043XRRzYnb2PoA2l4wtMLLVGGUP83pSSEKetgG1BUT/hAywwsxdmKSZyIzSGV4kwQP97GH69uTFKvpS+hczdWuhdJ3NtX55lDdifgKOcKbHmwggreso9dZs=-----END PRIVATE KEY-----";
            var Test_publicKey = "-----BEGIN PUBLIC KEY-----MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArA41mR6HjvKKECgpnFapb4zHZg+BbfMCbOgpgdL1NUg8Txsa9BnTVnE+ZJlbNv1omTLRT+KkDcQRF2TYfYGOHEDpN98EvaxPcP0otKbuaaX8pFh1VouQR5OAxwMiEbQ/RNkgQlXi9lzvRKbzia9qY1ExKO5sSRCcEzRo1e9R6E0Ut4QwmPjnIN+BNKYTsArkwkO5ZZFErvLwNBrGwpcNF1DfneBpBboVvj/KFF8UMGL4ekmH1sX4jUZ+VsWc9n7h/T55+d1Xiu6VzgQH/r3PJmpjq6qBbZ+BXkkGpwzHRrMJQGHqLimsDuklQDzFQrz7qJGjZDrLY9fqaiwB5NgbfQIDAQAB-----END PUBLIC KEY-----";

            var service = new TokenService(Test_publicKey, Test_privateKey);
            var TestUser = new User();

            TestUser.UserName = "TestName";
            TestUser.Role = UserRole.RegularUser;

            var token = await service.GenerateRefreshToken(TestUser);
            Assert.True(token.IsSuccess);
            Assert.True(service.ValidateRefreshToken(token.Value).IsSuccess);
            _out.WriteLine("Token is correct, signature is valid, no exception");
            
            
        }
        [Fact]
        public void TokenService_Rsa_Refresh_Token_Signature_Correct_Should_Return_False_test()
        {
            var Test_privateKey = "-----BEGIN PRIVATE KEY-----MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCsDjWZHoeO8ooQKCmcVqlvjMdmD4Ft8wJs6CmB0vU1SDxPGxr0GdNWcT5kmVs2/WiZMtFP4qQNxBEXZNh9gY4cQOk33wS9rE9w/Si0pu5ppfykWHVWi5BHk4DHAyIRtD9E2SBCVeL2XO9EpvOJr2pjUTEo7mxJEJwTNGjV71HoTRS3hDCY+Ocg34E0phOwCuTCQ7llkUSu8vA0GsbClw0XUN+d4GkFuhW+P8oUXxQwYvh6SYfWxfiNRn5WxZz2fuH9Pnn53VeK7pXOBAf+vc8mamOrqoFtn4FeSQanDMdGswlAYeouKawO6SVAPMVCvPuokaNkOstj1+pqLAHk2Bt9AgMBAAECggEAEQhaaP1F490B1ZtMPJRds1PR8joVu4844xQmoRoDuVHHL/jvIFF62Piz+bHZfvueLRBZT+8QA5GF3vWA8wfDduuRdZJSk1OpYkua3ytpYwm8wCjw+2+B3ybB3bG4iofNUvZVrwt2lkyunnK9SEOWehzcYNYdioF7mfEbHGv+hUsL6whQ35tiKKPnq/6qLsRKi9XjiRRjhfIPkDS2Vobt3OlCXr5b+aObL1I6p9I6vDw5nzt6e9GCjAVwapXqaaIXR/koOmtvvL0YZ01oREQopeso2jwx27+qRZkpdnwIkDe7GSdATFIwHQyO1nkLJzm8hQAJL2GkHwYBiyyEe3VCtwKBgQDbGKUzeB0QbDCj7gJresP7zrYbqRYGAqrpDnPfL4ndJgXPWXXcDB5fpR5Pdz0R2xA2GwYHDxKWRq/etew//1dycasuY6OUJsbj6FFafsNMdSSmyGQQFnry14iYB/dZyusF4M5V+toiit0NNTjqI4HgPFXwUoPimppRyD2aMImXIwKBgQDJCS7DrgtPVgNaNJY/USl5iQ6tQFJeTITW84Cl2nyzhewA6UVwHHkiBsrlE81a9VryKiKtoEZ/OU+nQzbTwoQsT+CMCFpPb4M2lkTZUynKrnBhus8C6whARRNfFQgkNK9stloCywkgSKsrLC9BWtgGF7y9uetvXapLixbHMd383wKBgDP354+KyRIRJ4RquyY1S3FZ+bYb6D5quZFoPTHOO9w53ZzuMleMhFPZfZQJy3GFzZWX2VkpSRZeE+82paAUd/CMZ7csKSvF+t0qSMm3Uam8C4KH+7wZKPabCypMkW46BH8zcJ3ST5Vr7LzqR+K5o1/Fz/ieCexhEYOehIYjRy+TAoGBAJ3Xl+ALxtWDsx4gK9eK4gWKlvtwhTuE2MfsaBXQdhh+Dc6pWNutf2435xX1dkb0XXPFoXSxYqiBVwj8vQ+GqkAgdy17YWz9IQi681Ou+CEq1RHmmqKv5sqPcaY13S6QLywsAIAw6flvFPXQu0mVCge+m3Jbh37pC8xEaJ39iU4BAoGAGgllzW+TbDJKJX2Yeeck4DQlv1lCQJAMnt2HY4SikPVAs2T/NwQD043XRRzYnb2PoA2l4wtMLLVGGUP83pSSEKetgG1BUT/hAywwsxdmKSZyIzSGV4kwQP97GH69uTFKvpS+hczdWuhdJ3NtX55lDdifgKOcKbHmwggreso9dZs=-----END PRIVATE KEY-----";
            var Test_publicKey = "-----BEGIN PUBLIC KEY-----MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArA41mR6HjvKKECgpnFapb4zHZg+BbfMCbOgpgdL1NUg8Txsa9BnTVnE+ZJlbNv1omTLRT+KkDcQRF2TYfYGOHEDpN98EvaxPcP0otKbuaaX8pFh1VouQR5OAxwMiEbQ/RNkgQlXi9lzvRKbzia9qY1ExKO5sSRCcEzRo1e9R6E0Ut4QwmPjnIN+BNKYTsArkwkO5ZZFErvLwNBrGwpcNF1DfneBpBboVvj/KFF8UMGL4ekmH1sX4jUZ+VsWc9n7h/T55+d1Xiu6VzgQH/r3PJmpjq6qBbZ+BXkkGpwzHRrMJQGHqLimsDuklQDzFQrz7qJGjZDrLY9fqaiwB5NgbfQIDAQAB-----END PUBLIC KEY-----";

            var service = new TokenService(Test_publicKey, Test_privateKey);

            string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6IlRlc3ROYW1lIiwiVXNlclJvbGUiOiJSZWd1bGFyVXNlciIsIm5iZiI6MTc1NTQ0MjE0MCwiZXhwIjoxNzU1NDQ1NzQwLCJpYXQiOjE3NTU0NDIxNDB9.C7P4aHC4cMd648Hi1EGHsv_npjvMBffmKG87pb99qaxcl6nk8PV6ix0SakKm63lMTe6TflAnwAqF5AdkFwER4j2CP_ysLX86IIj955y3k7Lyq_oFvODw-Ct1jZO8mZPZtJuUshPrtn-HBflxGvt8Xl1zbU-YGIe-kLjGHccJtaf3pNBzN9G6Dt5TIhIy8qXiHLa9iIGDLJi7ZSVvL9RIbzYGJIhOguOPu6gVqEGjgJDYwasdfawe2aA52tCYR7EHwSQllCmRvMYCbg4K0coBbodYaTPfKrSkZGpi7CXi-4-q8dj8kT7IUcD4S5JEDTIqdAHlhg";
            Assert.False(service.ValidateRefreshToken(token).IsSuccess);
            _out.WriteLine("Token signature is incorrect, so false bool value was returned");

        }
        [Fact]
        public void TokenService_Validation_Should_Return_Exception()
        {
            var Test_privateKey = "-----BEGIN PRIVATE KEY-----MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCsDjWZHoeO8ooQKCmcVqlvjMdmD4Ft8wJs6CmB0vU1SDxPGxr0GdNWcT5kmVs2/WiZMtFP4qQNxBEXZNh9gY4cQOk33wS9rE9w/Si0pu5ppfykWHVWi5BHk4DHAyIRtD9E2SBCVeL2XO9EpvOJr2pjUTEo7mxJEJwTNGjV71HoTRS3hDCY+Ocg34E0phOwCuTCQ7llkUSu8vA0GsbClw0XUN+d4GkFuhW+P8oUXxQwYvh6SYfWxfiNRn5WxZz2fuH9Pnn53VeK7pXOBAf+vc8mamOrqoFtn4FeSQanDMdGswlAYeouKawO6SVAPMVCvPuokaNkOstj1+pqLAHk2Bt9AgMBAAECggEAEQhaaP1F490B1ZtMPJRds1PR8joVu4844xQmoRoDuVHHL/jvIFF62Piz+bHZfvueLRBZT+8QA5GF3vWA8wfDduuRdZJSk1OpYkua3ytpYwm8wCjw+2+B3ybB3bG4iofNUvZVrwt2lkyunnK9SEOWehzcYNYdioF7mfEbHGv+hUsL6whQ35tiKKPnq/6qLsRKi9XjiRRjhfIPkDS2Vobt3OlCXr5b+aObL1I6p9I6vDw5nzt6e9GCjAVwapXqaaIXR/koOmtvvL0YZ01oREQopeso2jwx27+qRZkpdnwIkDe7GSdATFIwHQyO1nkLJzm8hQAJL2GkHwYBiyyEe3VCtwKBgQDbGKUzeB0QbDCj7gJresP7zrYbqRYGAqrpDnPfL4ndJgXPWXXcDB5fpR5Pdz0R2xA2GwYHDxKWRq/etew//1dycasuY6OUJsbj6FFafsNMdSSmyGQQFnry14iYB/dZyusF4M5V+toiit0NNTjqI4HgPFXwUoPimppRyD2aMImXIwKBgQDJCS7DrgtPVgNaNJY/USl5iQ6tQFJeTITW84Cl2nyzhewA6UVwHHkiBsrlE81a9VryKiKtoEZ/OU+nQzbTwoQsT+CMCFpPb4M2lkTZUynKrnBhus8C6whARRNfFQgkNK9stloCywkgSKsrLC9BWtgGF7y9uetvXapLixbHMd383wKBgDP354+KyRIRJ4RquyY1S3FZ+bYb6D5quZFoPTHOO9w53ZzuMleMhFPZfZQJy3GFzZWX2VkpSRZeE+82paAUd/CMZ7csKSvF+t0qSMm3Uam8C4KH+7wZKPabCypMkW46BH8zcJ3ST5Vr7LzqR+K5o1/Fz/ieCexhEYOehIYjRy+TAoGBAJ3Xl+ALxtWDsx4gK9eK4gWKlvtwhTuE2MfsaBXQdhh+Dc6pWNutf2435xX1dkb0XXPFoXSxYqiBVwj8vQ+GqkAgdy17YWz9IQi681Ou+CEq1RHmmqKv5sqPcaY13S6QLywsAIAw6flvFPXQu0mVCge+m3Jbh37pC8xEaJ39iU4BAoGAGgllzW+TbDJKJX2Yeeck4DQlv1lCQJAMnt2HY4SikPVAs2T/NwQD043XRRzYnb2PoA2l4wtMLLVGGUP83pSSEKetgG1BUT/hAywwsxdmKSZyIzSGV4kwQP97GH69uTFKvpS+hczdWuhdJ3NtX55lDdifgKOcKbHmwggreso9dZs=-----END PRIVATE KEY-----";
            var Test_publicKey = "-----BEGIN PUBLIC KEY-----MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArA41mR6HjvKKECgpnFapb4zHZg+BbfMCbOgpgdL1NUg8Txsa9BnTVnE+ZJlbNv1omTLRT+KkDcQRF2TYfYGOHEDpN98EvaxPcP0otKbuaaX8pFh1VouQR5OAxwMiEbQ/RNkgQlXi9lzvRKbzia9qY1ExKO5sSRCcEzRo1e9R6E0Ut4QwmPjnIN+BNKYTsArkwkO5ZZFErvLwNBrGwpcNF1DfneBpBboVvj/KFF8UMGL4ekmH1sX4jUZ+VsWc9n7h/T55+d1Xiu6VzgQH/r3PJmpjq6qBbZ+BXkkGpwzHRrMJQGHqLimsDuklQDzFQrz7qJGjZDrLY9fqaiwB5NgbfQIDAQAB-----END PUBLIC KEY-----";

            var service = new TokenService(Test_publicKey, Test_privateKey);

            string token = "just_random_string";
            var exception = Assert.ThrowsAny<Exception>(() => service.ValidateRefreshToken(token));
            _out.WriteLine(exception.Message);

        }

        [Fact]
        public async void TokenService_Generation_AccessToken_Should_Return_Token()
        {
            string RefreshToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJVc2VyUm9sZSI6IlJlZ3VsYXJVc2VyIiwibmJmIjoxNzU1NjAyNjMxLCJleHAiOjE3NTU2MDYyMzEsImlhdCI6MTc1NTYwMjYzMX0.U1VCCZql7IYHejJ5STU9auGRZr9oFYgHRKnEPYXf-Perc77RK80tjUOpDSxqXdxE1hrx2k85s6XlSIOXjKpGX9lAsfOM81WzlFIjV7Z1es16eo6dq_X_98fRcfP2azJNpyqX4pqZNz2PNLmrXtA5m1PUrwGu7KOUIyKyxfMk3WcVbKLPdI2HpdKPQG_OB299iEtjPVsLSaXMQ9qV2j5XZLTWWsZq7ucoY4Mh0eb5NzEg8py-xJHCzjMLWQf-5HFgxQXWS0aJDK_rgA0V7vpj9kMn3iceUnZ5pWIaVDqJqXgVTJMxcj_AUvooPCvadlO7K2gOPr4_b3NvapCliDqwgg";
            var Test_privateKey = "-----BEGIN PRIVATE KEY-----MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCsDjWZHoeO8ooQKCmcVqlvjMdmD4Ft8wJs6CmB0vU1SDxPGxr0GdNWcT5kmVs2/WiZMtFP4qQNxBEXZNh9gY4cQOk33wS9rE9w/Si0pu5ppfykWHVWi5BHk4DHAyIRtD9E2SBCVeL2XO9EpvOJr2pjUTEo7mxJEJwTNGjV71HoTRS3hDCY+Ocg34E0phOwCuTCQ7llkUSu8vA0GsbClw0XUN+d4GkFuhW+P8oUXxQwYvh6SYfWxfiNRn5WxZz2fuH9Pnn53VeK7pXOBAf+vc8mamOrqoFtn4FeSQanDMdGswlAYeouKawO6SVAPMVCvPuokaNkOstj1+pqLAHk2Bt9AgMBAAECggEAEQhaaP1F490B1ZtMPJRds1PR8joVu4844xQmoRoDuVHHL/jvIFF62Piz+bHZfvueLRBZT+8QA5GF3vWA8wfDduuRdZJSk1OpYkua3ytpYwm8wCjw+2+B3ybB3bG4iofNUvZVrwt2lkyunnK9SEOWehzcYNYdioF7mfEbHGv+hUsL6whQ35tiKKPnq/6qLsRKi9XjiRRjhfIPkDS2Vobt3OlCXr5b+aObL1I6p9I6vDw5nzt6e9GCjAVwapXqaaIXR/koOmtvvL0YZ01oREQopeso2jwx27+qRZkpdnwIkDe7GSdATFIwHQyO1nkLJzm8hQAJL2GkHwYBiyyEe3VCtwKBgQDbGKUzeB0QbDCj7gJresP7zrYbqRYGAqrpDnPfL4ndJgXPWXXcDB5fpR5Pdz0R2xA2GwYHDxKWRq/etew//1dycasuY6OUJsbj6FFafsNMdSSmyGQQFnry14iYB/dZyusF4M5V+toiit0NNTjqI4HgPFXwUoPimppRyD2aMImXIwKBgQDJCS7DrgtPVgNaNJY/USl5iQ6tQFJeTITW84Cl2nyzhewA6UVwHHkiBsrlE81a9VryKiKtoEZ/OU+nQzbTwoQsT+CMCFpPb4M2lkTZUynKrnBhus8C6whARRNfFQgkNK9stloCywkgSKsrLC9BWtgGF7y9uetvXapLixbHMd383wKBgDP354+KyRIRJ4RquyY1S3FZ+bYb6D5quZFoPTHOO9w53ZzuMleMhFPZfZQJy3GFzZWX2VkpSRZeE+82paAUd/CMZ7csKSvF+t0qSMm3Uam8C4KH+7wZKPabCypMkW46BH8zcJ3ST5Vr7LzqR+K5o1/Fz/ieCexhEYOehIYjRy+TAoGBAJ3Xl+ALxtWDsx4gK9eK4gWKlvtwhTuE2MfsaBXQdhh+Dc6pWNutf2435xX1dkb0XXPFoXSxYqiBVwj8vQ+GqkAgdy17YWz9IQi681Ou+CEq1RHmmqKv5sqPcaY13S6QLywsAIAw6flvFPXQu0mVCge+m3Jbh37pC8xEaJ39iU4BAoGAGgllzW+TbDJKJX2Yeeck4DQlv1lCQJAMnt2HY4SikPVAs2T/NwQD043XRRzYnb2PoA2l4wtMLLVGGUP83pSSEKetgG1BUT/hAywwsxdmKSZyIzSGV4kwQP97GH69uTFKvpS+hczdWuhdJ3NtX55lDdifgKOcKbHmwggreso9dZs=-----END PRIVATE KEY-----";
            var Test_publicKey = "-----BEGIN PUBLIC KEY-----MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArA41mR6HjvKKECgpnFapb4zHZg+BbfMCbOgpgdL1NUg8Txsa9BnTVnE+ZJlbNv1omTLRT+KkDcQRF2TYfYGOHEDpN98EvaxPcP0otKbuaaX8pFh1VouQR5OAxwMiEbQ/RNkgQlXi9lzvRKbzia9qY1ExKO5sSRCcEzRo1e9R6E0Ut4QwmPjnIN+BNKYTsArkwkO5ZZFErvLwNBrGwpcNF1DfneBpBboVvj/KFF8UMGL4ekmH1sX4jUZ+VsWc9n7h/T55+d1Xiu6VzgQH/r3PJmpjq6qBbZ+BXkkGpwzHRrMJQGHqLimsDuklQDzFQrz7qJGjZDrLY9fqaiwB5NgbfQIDAQAB-----END PUBLIC KEY-----";

            var service = new TokenService(Test_publicKey, Test_privateKey);
            string AccessToken = service.GenerateAccessToken(RefreshToken).Result.Value;
            _out.WriteLine(AccessToken);
        }

       
    }
}