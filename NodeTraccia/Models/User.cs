using System.ComponentModel.DataAnnotations;

namespace NodeTraccia.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [MaxLength(100, ErrorMessage = "Il nome non può essere più lungo di 100 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail non valida")]
        [EmailAddress(ErrorMessage = "E-Mail non valida")]
        public string Email { get; set; }
    }
}
