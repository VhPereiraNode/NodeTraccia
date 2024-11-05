using System.ComponentModel.DataAnnotations;

namespace NodeTraccia.Models
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
    }
}
