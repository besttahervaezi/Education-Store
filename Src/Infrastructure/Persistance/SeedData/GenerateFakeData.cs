using Domain.Entities;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistance.SeedData;

public class GenerateFakeData
{
    public static async Task SeedDataAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {

        try
        {
            if (!await context.ProductBrand.AnyAsync())
            {
                var brands = ProductBrands();
               
                await context.ProductBrand.AddRangeAsync(brands);
                await context.SaveChangesAsync();
            }
            if (!await context.ProductType.AnyAsync())
            {
                var types = ProductTypes();

                await context.ProductType.AddRangeAsync(types);
                await context.SaveChangesAsync();
            }
            if (!await context.Products.AnyAsync())
            {
                var products = Products();
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<GenerateFakeData>();
            logger.LogError(e,"error in seed data");
        }
    }

    private static List<ProductType> ProductTypes()
    {
        var producttypes = new List<ProductType>()
        {
            new()
            {
                Description = "type 1 created",
                Summary = "this type 1",
                Title = "type 1"
            },
            new()
            {
                Description = "type 2 created",
                Summary = "this type 2",
                Title = "type 2"
            }
        };
        return producttypes;
    }

    private static List<ProductBrand> ProductBrands()
    {
        var productBrands = new List<ProductBrand>()
        {
            new()
            {
                Description = "brand 1 created",
                Summary = "this brand 1",
                Title = "brand 1"
            },
            new()
            {
                Description = "brand 2 created",
                Summary = "this brand 2",
                Title = "brand 2"
            }
        };
        return productBrands;
    }

    private static List<Product> Products()
    {
        var products = new List<Product>()
        {
            new()
            {
                Description = "test",
                PictureUrl = "",
                Price = 15000,
                Title = "product1",
                ProductBrandId = 1,
                ProductTypeId = 1,
                Summary = "this is the product number 1"
            },
            new()
            {
                Description = "test",
                PictureUrl = "",
                Price = 15000,
                Title = "product2",
                ProductBrandId = 1,
                ProductTypeId = 1,
                Summary = "this is the product number 2"
            },
            new()
            {
                Description = "test",
                PictureUrl = "",
                Price = 15000,
                Title = "product3",
                ProductBrandId = 1,
                ProductTypeId = 1,
                Summary = "this is the product number 3"
            },
            new()
            {
                Description = "test",
                PictureUrl = "",
                Price = 15000,
                Title = "product4",
                ProductBrandId = 1,
                ProductTypeId = 1,
                Summary = "this is the product number 4"
            },
            new()
            {
                Description = "test",
                PictureUrl = "",
                Price = 15000,
                Title = "product5",
                ProductBrandId = 1,
                ProductTypeId = 1,
                Summary = "this is the product number 5"
            },
        };
        return products;
    }
}