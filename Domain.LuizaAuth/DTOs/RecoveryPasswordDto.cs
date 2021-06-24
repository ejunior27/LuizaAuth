using System.ComponentModel.DataAnnotations;

namespace Domain.LuizaAuth.DTOs
{
    public class RecoveryPasswordDto
    {
        [Required(ErrorMessage = "Informe o e-mail do seu cadastro")]
        [EmailAddress(ErrorMessage = "Formato do email é inválido")]
        public string Email { get; set; }
    }
}
