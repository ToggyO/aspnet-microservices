using System.Threading.Tasks;

namespace AspNetMicroservices.Products.DataLayer.Repositories
{
	/// <summary>
	/// Represents basic entity repository.
	/// </summary>
	/// <typeparam name="TEntity">Type of entity.</typeparam>
    public interface IBaseRepository<TEntity>
    {
	    /// <summary>
	    /// Retrieves entity by id from database.
	    /// </summary>
	    /// <param name="id">Entity id in database.</param>
	    /// <returns>Instance of <see cref="TEntity"/> from database.</returns>
        Task<TEntity> GetById(int id);

	    /// <summary>
	    /// Creates entity of type <see cref="TEntity"/>.
	    /// </summary>
	    /// <param name="entity">Instance of <see cref="TEntity"/>.</param>
	    /// <returns>Created instance of <see cref="TEntity"/> in database.</returns>
        Task<TEntity> Create(TEntity entity);

	    /// <summary>
	    /// Updates entity of type <see cref="TEntity"/>.
	    /// </summary>
	    /// <param name="entity">Instance of <see cref="TEntity"/>.</param>
	    /// <returns>Created instance of <see cref="TEntity"/> in database.</returns>
        Task<TEntity> Update(TEntity entity);

	    /// <summary>
	    /// Removes entity in database by id.
	    /// </summary>
	    /// <param name="id"></param>
	    /// <returns></returns>
        Task<TEntity> Delete(int id);
    }
}