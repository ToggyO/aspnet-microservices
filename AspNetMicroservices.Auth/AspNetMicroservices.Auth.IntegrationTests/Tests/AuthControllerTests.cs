using System.Net;
using System.Net.Http.Json;

using AspNetMicroservices.Abstractions.Models.Auth;
using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Auth.Application.Features.Auth.Commands;

using FluentAssertions;

namespace AspNetMicroservices.Auth.IntegrationTests.Tests
{
	public class AuthControllerTests : IntegrationTestBase
	{
		[Fact]
		public async Task RefreshToken_GetUsersById_MustRetrieveTestUserAfterTokenRefreshed()
		{
			// Arrange
			var tokens = await AuthenticateAsync();

			// Act
			// TODO: HARDCODE
			var response = await TestClient.PostAsJsonAsync("auth/refresh", new Refresh.Command
			{
				RefreshToken = tokens.RefreshToken
			});


			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var content = await response.Content.ReadAsStringAsync();
			content.Should().NotBeEmpty();

			var tokenResponse = Deserialize<Response<TokenDto>>(content);
			tokenResponse.Should().NotBeNull();
			tokenResponse.Data.Should().NotBeNull();
			tokenResponse.Data.AccessToken.Should().NotBeEmpty();


			SetAuthorizationHeader(tokenResponse.Data.AccessToken);


		}
	}
}