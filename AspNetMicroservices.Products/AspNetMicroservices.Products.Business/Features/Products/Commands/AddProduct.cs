using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

namespace AspNetMicroservices.Products.Business.Features.Products.Commands
{
    public class TestModel
    {
        public string Name { get; set; } = "KEK";
    }

    public class AddProduct
    {
        public sealed class Command : IRequest<TestModel>
        {
			public string ProductName { get; set; }
        }

        public sealed class Handler : IRequestHandler<Command, TestModel>
        {
            public async Task<TestModel> Handle(Command cmd, CancellationToken ct)
            {
                await Task.Delay(1000);
                return new TestModel();
            }
        }

        public sealed class Validator : AbstractValidator<Command>
        {
	        public Validator()
	        {
		        RuleFor(x => x.ProductName).NotEmpty();
	        }
        }

    }
}