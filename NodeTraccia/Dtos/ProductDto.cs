using System.ComponentModel.DataAnnotations;

namespace NodeTraccia.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        [MaxLength(300, ErrorMessage = "Il nome non può essere più lungo di 300 caratteri")]
        public string Nome { get; set; }

        public decimal Price { get; set; }
    }
}
