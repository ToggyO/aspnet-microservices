using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Auth.Api;
using AspNetMicroservices.Auth.Application.Dto.Auth;
using AspNetMicroservices.Auth.Application.Dto.Users;
using AspNetMicroservices.Auth.Application.Features.Auth.Commands;
using AspNetMicroservices.Auth.Application.Features.Users.Commands;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetMicroservices.Auth.IntegrationTests
{
	public class IntegrationTestBase
	{
		protected readonly CredentialsDto Credentials = new CredentialsDto
		{
			Email = "qwe@qwe.com",
			Password = "111111"
		};

		protected readonly HttpClient TestClient;

		protected readonly JsonSerializerOptions SerializerOptions;

		protected IntegrationTestBase()
		{
			var appFactory = new WebApplicationFactory<Startup>()
				.WithWebHostBuilder(builder =>
				{
					builder.ConfigureAppConfiguration(((context, configurationBuilder) =>
					{
						context.HostingEnvironment.EnvironmentName = "Testing";
						// TODO: check
						string path = $"../../../../.env.{context.HostingEnvironment.EnvironmentName.ToLower()}";
						path = Path.GetFullPath(path);
						DotNetEnv.Env.Load(path);
					}));
				});

			string apiVersion = appFactory.Services
				.GetRequiredService<IApiVersionDescriptionProvider>()?
				.ApiVersionDescriptions?
				.SingleOrDefault()?
				.GroupName ?? "1";

			// TODO: add generic protocol, host, port, api prefix
			appFactory.ClientOptions.BaseAddress = new Uri($"http://localhost:5000/api/v{apiVersion}/");
			TestClient = appFactory.CreateClient();

			SerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		protected TValue? Deserialize<TValue>(string content)
			=> JsonSerializer.Deserialize<TValue>(content, SerializerOptions);

		protected async Task<TokenDto> AuthenticateAsync()
		{
			var tokens = await GetJwtAsync();
			SetAuthorizationHeader(tokens.AccessToken)
			return tokens;
		}

		protected void SetAuthorizationHeader(string token)
		{
			TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
		}

		private async Task<TokenDto> GetJwtAsync()
		{
			// TODO: uri into constants
			var httpResponseMessage = await TestClient.PostAsJsonAsync("auth/sign-in", new Authenticate.Command
			{
				Email = Credentials.Email,
				Password = Credentials.Password
			});

			var content = await httpResponseMessage.Content.ReadAsStringAsync();
			var authResponse = Deserialize<Response<AuthenticationTicket<UserDto>>>(content);

			return authResponse.Data.Tokens;
		}
	}
}