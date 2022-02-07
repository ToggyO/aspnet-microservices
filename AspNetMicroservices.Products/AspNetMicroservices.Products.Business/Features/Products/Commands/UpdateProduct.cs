using System;
using System.Threading;
using System.Threading.Tasks;

using AspNetMicroservices.Products.Business.Features.Products.Models;
using AspNetMicroservices.Products.Business.Features.Products.Validators;
using AspNetMicroservices.Products.DataLayer.Entities.Product;
using AspNetMicroservices.Products.DataLayer.Repositories.Products;

using AutoMapper;

using FluentValidation;

using MediatR;

namespace AspNetMicroservices.Products.Business.Features.Products.Commands
{
	/// <summary>
	/// Update product.
	/// </summary>
	public class UpdateProduct
	{
		/// <summary>
		/// Update product command.
		/// </summary>
		public sealed class Command : ProductModel, IRequest<ProductModel>
		{
		}

		public sealed class Handler : IRequestHandler<Command, ProductModel>
		{
			/// <summary>
			/// Instance of <see cref="IProductsRepository"/>.
			/// </summary>
			private readonly IProductsRepository _repository;

			/// <summary>
			/// Instance of <see cref="IMapper"/>.
			/// </summary>
			private readonly IMapper _mapper;

			/// <summary>
			/// Creates an instance of <see cref="Handler"/>.
			/// </summary>
			/// <param name="repository">Instance of <see cref="IProductsRepository"/>.</param>
			/// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
			public Handler(
				IProductsRepository repository,
				IMapper mapper)
			{
				_repository = repository;
				_mapper = mapper;
			}

			/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
			public async Task<ProductModel> Handle(Command cmd, CancellationToken cancellationToken = default)
			{
				if (await _repository.GetById(cmd.Id) is null)
					return default;

				await using var ts = await _repository.CreateTransactionAsync();
				try
				{
					var entity = await _repository.Update(_mapper.Map<ProductEntity>(cmd));
					await ts.CommitAsync(cancellationToken);
					return _mapper.Map<ProductModel>(entity);
				}
				catch (Exception)
				{
					await ts.RollbackAsync(cancellationToken);
					return default;
				}
			}
		}

		#region Validator

		/// <summary>
		/// Validator for <see cref="Command"/>.
		/// </summary>
		public class Validator : CreateUpdateProductValidator<Command>
		{}

		#endregion

		#region Mapper

		/// <summary>
		/// Mapper profile.
		/// </summary>
		public class MapperProfile : Profile
		{
			/// <summary>
			/// Initialize new instance of <see cref="MapperProfile"/>.
			/// </summary>
			public MapperProfile()
			{
				CreateMap<Command, ProductEntity>();
			}
		}

		#endregion
	}
}