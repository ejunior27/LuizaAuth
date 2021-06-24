using System.ComponentModel.DataAnnotations;

namespace Domain.LuizaAuth.DTOs
{
    public class UserDto: BaseEntityDto
    {
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe um e-mail válido")]
        [EmailAddress(ErrorMessage = "Formato do e-mail é inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Senha não compatível com os requisitos mínimos")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]        
        [Compare("Password", ErrorMessage = "A senha não corresponde")]
        [Display(Name = "Confirmação de senha")]
        public string PasswordConfirm { get; set; }
    }
}
