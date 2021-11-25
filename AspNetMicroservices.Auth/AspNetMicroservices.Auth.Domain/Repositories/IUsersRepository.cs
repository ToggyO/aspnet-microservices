using System.Collections.Generic;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Shared.Contracts;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;

namespace AspNetMicroservices.Auth.Domain.Repositories
{
	/// <summary>
	/// Represents user model repository.
	/// </summary>
	public interface IUsersRepository : IBaseRepository<UserModel>
	{
		// TODO: add summary
		Task<PaginationModel<UserModel>> GetList(QueryFilterModel filter);
	}
}