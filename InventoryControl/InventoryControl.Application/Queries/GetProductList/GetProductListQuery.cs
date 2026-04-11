using MediatR;

namespace InventoryControl.Application.Queries.GetProductList;

public record GetProductListQuery() : IRequest<List<ProductDto>>;