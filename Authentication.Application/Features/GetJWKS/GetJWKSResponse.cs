namespace Authentication.Application.Features.GetJWKS
{
    public class GetJWKSResponse
    {
        public required string Kty {  get; set; }
        public required string Use { get; set; }
        public required string Kid { get; set; }
        public required string Alg {  get; set; }
        public required string N {  get; set; }
        public required string E { get; set; }
    }
}
