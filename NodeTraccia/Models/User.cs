using System.ComponentModel.DataAnnotations;

namespace NodeTraccia.Models
{
    public class User:BaseEntity
    {
        
        [Required(ErrorMessage = "Campo Obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail non valida")]
        [EmailAddress(ErrorMessage = "E-Mail non valida")]
        public string Email { get; set; }
    }
}
