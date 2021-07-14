using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection;
using AspNetMicroservices.Products.DataLayer.Entities.Product;

using AutoMapper;
using LinqToDB;
using MediatR;

namespace AspNetMicroservices.Products.Business.Features.Products.Commands
{
	/// <summary>
	/// Create product command.
	/// </summary>
    public class CreateProduct
    {
	    /// <summary>
	    /// Get deal agreement command.
	    /// </summary>
        public sealed class Command : IRequest<ProductModel>
        {
	        /// <summary>
	        /// Gets or sets the product name.
	        /// </summary>
            public string Name { get; set; }

	        /// <summary>
	        /// Gets or sets the product description.
	        /// </summary>
            public string Description { get; set; }

	        /// <summary>
	        /// Gets or sets the product price.
	        /// </summary>
            public long Price { get; set; }
        }

	    /// <summary>
	    /// Handle <see cref="Command"/>.
	    /// </summary>
        public sealed class Handler : IRequestHandler<Command, ProductModel>
        {
            private readonly IAppDataConnection _connection;
            private readonly IMapper _mapper;

            /// <summary>
            /// Creates an instance of <see cref="Handler"/>.
            /// </summary>
            /// <param name="connection">Application database connection.</param>
            /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
            public Handler(
                IAppDataConnection connection,
                IMapper mapper)
            {
                _connection = connection;
                _mapper = mapper;
            }

            /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
            public async Task<ProductModel> Handle(Command cmd, CancellationToken ct)
            {
                var entity = new ProductEntity
                {
                    Name = cmd.Name,
                    Description = cmd.Description,
                    Price = cmd.Price,
                };
                entity.Id = await _connection.InsertWithInt32IdentityAsync(entity);
                return _mapper.Map<ProductEntity, ProductModel>(entity);
            }
        }

	    #region Mapper

	    /// <summary>
	    /// Mapper profile.
	    /// </summary>
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<ProductEntity, ProductModel>().ReverseMap();
            }
        }

	    #endregion
    }
}