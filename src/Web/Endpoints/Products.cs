using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Products.Queries;
using CleanArchitecture.Domain.Entities;

namespace ReactExample.Server.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization() NOTE: Commenting out for now so its easier to use API page
            .MapGet(GetProductsWithPagination);
    }

    public Task<PaginatedList<Product>> GetProductsWithPagination(ISender sender, [AsParameters] GetProductsWithPaginationQuery query)
    {
        return sender.Send(query);
    }
}
