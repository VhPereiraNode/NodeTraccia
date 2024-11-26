using System.ComponentModel.DataAnnotations;

namespace NodeTraccia.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [MaxLength(100, ErrorMessage = "Il nome non può essere più lungo di 100 caratteri")]
        public string Nome { get; set; }

    }
}
