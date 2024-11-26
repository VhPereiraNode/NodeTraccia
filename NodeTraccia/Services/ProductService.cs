using NodeTraccia.Dtos;
using NodeTraccia.Models;
using NodeTraccia.Repositories;

namespace NodeTraccia.Services
{
    public class ProductService : CrudServiceBase<Product, ProductDto>
    {
        public static List<Product> products = new List<Product> { 
            new Product { Id = 1, Nome = "Laptop", Price = 999.99m },
            new Product { Id = 2, Nome = "Smartphone", Price = 499.99m },
            new Product { Id = 3, Nome = "Tablet", Price = 299.99m },
            new Product { Id = 4, Nome = "Monitor", Price = 199.99m }
        };

        public  Product Create(ProductDto dto);
       

        public override bool Delete(int id)
        {
            products.RemoveAll(p => p.Id == id);
            if (products.Any(p => p.Id == id))
            {
                return false;
            }
            return true;
        }

        public  Product Read(int id);
     

        public override List<Product> Read(string? ricerca = null)
        {            
            if (ricerca is not null)
            {
                return products
                    .Where(r => r.Nome.Contains(ricerca, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }            
            return products;
        }

        public override Product Update(int id, ProductDto dto)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is not null)
            {
                product.Nome = dto.Nome;
                product.Price = dto.Price;
            }
            return Read(id);
        }

        protected override ProductDto MapDto(Product entity)
        {
            ProductDto dto = new ProductDto
            {               
                Nome = entity.Nome,
                Price = entity.Price,
            };
            return dto;
        }

        protected override Product MapEntity(ProductDto dto)
        {
            Product product = new Product
            {
                Id = products.Max(p=>p.Id) + 1,
                Nome = dto.Nome,
                Price = dto.Price
            };
            return product;
        }
    }
}
