using System;
using System.Collections.Generic;
using MediatR;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Dtos.Products;
using Application.wrappers;
using Domain.Entities;
namespace Application.Features.Product.Queries.GetAll;

public class GetAllProductQuery : RequestParametersBasic,IRequest<PaginationResponse<ProductDto>>,ICacheQuery
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public int HoursSaveData =>1; // 1 hour save data
}