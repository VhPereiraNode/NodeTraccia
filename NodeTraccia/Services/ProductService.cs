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

        public override ProductDto Create(ProductDto dto)
        {
            var product = MapEntity(dto);
            products.Add(product);
            return Read(product.Id);
        }

        public override bool Delete(int id)
        {
            products.RemoveAll(p => p.Id == id);
            if (products.Any(p => p.Id == id))
            {
                return false;
            }
            return true;
        }

        public override ProductDto Read(int id)
        { 
            ProductDto result = new ProductDto();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is not null)
            {
                result = MapDto(product);
            }
            return result ;
        }

        public override List<ProductDto> Read(string? ricerca = null)
        {
            List<ProductDto> result = new List<ProductDto>();
            if (ricerca == null)
            {
                foreach (var item in products)
                {
                    result.Add(MapDto(item));
                }
            }
            else
            {
                var list = products
                    .Where(r => r.Nome.Contains(ricerca, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var item in list)
                {
                    result.Add(MapDto(item));
                }
            }
            return result;
        }

        public override ProductDto Update(int id, ProductDto dto)
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
                Id = entity.Id,
                Nome = entity.Nome,
                Price = entity.Price,
            };
            return dto;
        }

        protected override Product MapEntity(ProductDto dto)
        {
            Product product = new Product
            {
                Id = products.Count + 1,
                Nome = dto.Nome,
                Price = dto.Price
            };
            return product;
        }
    }
}
