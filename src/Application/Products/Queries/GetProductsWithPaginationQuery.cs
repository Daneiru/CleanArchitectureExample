using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Products.Queries;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<Product>>
{
    public string? Query { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<Product>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Product>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Product> Products;

        if (request.Query != null) // TODO: IsNullOrEmpty() extension
        {
            int searchId = int.Parse(request.Query);


            Products = _context.Products
                .Where(o =>
                    (searchId != 0 ? o.Id.Equals(searchId) : o.Id > 0)
                    || o.Name.Contains(request.Query)
                    || o.Description.Contains(request.Query)
                );
        }
        else
        {
            Products = _context.Products.AsQueryable();
        }

        var productResult = await Products
                                    .Skip(request.PageSize * (request.PageNumber - 1))
                                    .Take(request.PageSize)
                                    .ToListAsync();

        return new PaginatedList<Product>(productResult, Products.Count(), request.PageNumber, request.PageSize);
    }
}
