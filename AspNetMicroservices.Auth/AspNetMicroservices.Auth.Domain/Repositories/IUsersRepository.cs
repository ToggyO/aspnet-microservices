using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Contracts;
using AspNetMicroservices.Abstractions.Models.Pagination;
using AspNetMicroservices.Abstractions.Models.QueryFilter.Implementation;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;

namespace AspNetMicroservices.Auth.Domain.Repositories
{
	/// <summary>
	/// Represents user model repository.
	/// </summary>
	public interface IUsersRepository : IBaseRepository<UserModel>
	{
		/// <summary>
		/// Retrieves a list of users.
		/// </summary>
		/// <param name="filter">Query filter.</param>
		/// <returns>List of users.</returns>
		Task<PaginationModel<UserModel>> GetList(QueryFilterModel filter);

		/// <summary>
		/// Retrieves entity by email from database.
		/// </summary>
		/// <param name="email">User email.</param>
		/// <returns></returns>
		Task<UserModel> GetByEmail(string email);

		/// <summary>
		/// Creates entity of type <see cref="UserDetailModel"/>.
		/// </summary>
		/// <param name="entity">Instance of <see cref="UserDetailModel"/>.</param>
		/// <returns>User details entity.</returns>
		Task<UserDetailModel> CreateDetails(UserDetailModel entity);
	}
}