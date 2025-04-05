using Authentication.API.Middlewares;
using Authentication.Application;
using Authentication.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Authentication using JWT Bearer tokens
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Define token validation parameters to ensure tokens are valid and trustworthy
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Ensure the token was issued by a trusted issuer
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // The expected issuer value from configuration
        ValidateAudience = false, // Disable audience validation (can be enabled as needed)
        ValidateLifetime = true, // Ensure the token has not expired
        ValidateIssuerSigningKey = true, // Ensure the token's signing key is valid
                                         // Define a custom IssuerSigningKeyResolver to dynamically retrieve signing keys from the JWKS endpoint
        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
        {
            //Console.WriteLine($"Received Token: {token}");
            //Console.WriteLine($"Token Issuer: {securityToken.Issuer}");
            //Console.WriteLine($"Key ID: {kid}");
            //Console.WriteLine($"Validate Lifetime: {parameters.ValidateLifetime}");
            // Initialize an HttpClient instance for fetching the JWKS
            var httpClient = new HttpClient();
            // Synchronously fetch the JWKS (JSON Web Key Set) from the specified URL
            var jwks = httpClient.GetStringAsync($"{builder.Configuration["Jwt:Issuer"]}/.well-known/jwks.json").Result;
            // Parse the fetched JWKS into a JsonWebKeySet object
            var keys = new JsonWebKeySet(jwks);
            // Return the collection of JsonWebKey objects for token validation
            return keys.Keys;
        }
    };
});

var app = builder.Build();
app.UseExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
