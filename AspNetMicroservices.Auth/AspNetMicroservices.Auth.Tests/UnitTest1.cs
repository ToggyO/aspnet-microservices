
using AspNetMicroservices.Abstractions.Models.Pagination;
using AspNetMicroservices.Abstractions.Models.QueryFilter.Implementation;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;

using MediatR;

using Moq;

namespace AspNetMicroservices.Auth.Tests;

public class UnitTest1
{
	// private readonly Mock<IMediator> _mediator = new ();

	private readonly Mock<IUsersRepository> _repoMock = new ();

    [Fact]
    public void Test1()
    {
	    var result = _repoMock.Setup(x => x.GetList(new QueryFilterModel()))
		    .ReturnsAsync(new PaginationModel<UserModel>());
    }
}