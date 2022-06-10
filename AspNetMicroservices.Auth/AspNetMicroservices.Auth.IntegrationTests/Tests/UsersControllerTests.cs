using System.Net;
using System.Net.Http.Json;

using AspNetMicroservices.Abstractions.Models.Response;
using AspNetMicroservices.Auth.Application.Dto.Users;

using FluentAssertions;

namespace AspNetMicroservices.Auth.IntegrationTests.Tests
{
	public class UsersControllerTests : IntegrationTestBase
	{
		[Fact]
		public async Task GetUsersById_MustRetrieveTestUser()
		{
			// Arrange
			await AuthenticateAsync();

			// Act
			// TODO: HARDCODE
			var response = await TestClient.GetAsync("users/1");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var content = await response.Content.ReadAsStringAsync();
			content.Should().NotBeEmpty();

			var userResponse = Deserialize<Response<UserDto>>(content);
			userResponse.Should().NotBeNull();
			userResponse?.Data.Should().NotBeNull();
			userResponse?.Data.Email.Should().Be(Credentials.Email);
		}

	}
}