using System.ComponentModel.DataAnnotations;

namespace Domain.LuizaAuth.DTOs
{
    public class UserAuthenticateRequestDto
    {
        [Required(ErrorMessage = "Informe seu e-mail")]
        [EmailAddress(ErrorMessage = "Formato do e-mail é inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
