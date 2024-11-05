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

        protected override Product AddEntity(Product entity)
        {
            entity.Id = products.Max(u => u.Id) + 1;
            products.Add(entity);
            return entity;
        }

        protected override List<Product> GetAll()
        {
            return products.ToList();
        }

        protected override List<Product> GetByString(string ricerca)

        {
            return products
                     .Where(r => r.Nome.Contains(ricerca, StringComparison.OrdinalIgnoreCase))
                     .ToList();
        }

        protected override Product GetEntityById(int id)
        {
            return products.FirstOrDefault(x => x.Id.Equals(id));
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
                Nome = dto.Nome,
                Price = dto.Price
            };
            return product;
        }

        protected override void Remove(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }

        protected override Product UpdateEntity(Product product, ProductDto dto)
        {
            product.Nome = dto.Nome;
            product.Price = dto.Price;

            return product;
        }
    }
}
