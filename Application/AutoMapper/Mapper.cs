using Application.Requests.Authorization.Command.Register;
using Application.Requests.Category.Queries.GetAll;
using Application.Requests.Category.Queries.GetById;
using Application.Requests.Product.Commands.Add;
using Application.Requests.Product.Queries.GetAll;
using Application.Requests.Product.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper;

public class Mapper : Profile
{
    public Mapper()
    {
        //RegisterCommands
        CreateMap<RegisterCommand, User>();
        
        //ProductQueries
        CreateMap<Product, GetAllProductsDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<Product, GetProductByIdDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        
        //ProductCommands
        CreateMap<AddProductCommand, Product>();
        
        //CategoryQueries
        CreateMap<Category, GetAllCategoriesDto>();
        CreateMap<Category, GetCategoryByIdDto>();
    }
}