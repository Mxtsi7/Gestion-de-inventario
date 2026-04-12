using MediatR;
using InventoryControl.Application.DTOs;

namespace InventoryControl.Application.Queries.GetProductList;

public record GetProductListQuery() : IRequest<List<ProductDto>>;
