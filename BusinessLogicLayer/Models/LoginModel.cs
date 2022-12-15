using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [StringLength( 10, ErrorMessage = "Длина строки должна быть до 10 символов")]
        public string UserName { get; set; } = null!;

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{8,}$", ErrorMessage = "Некорректный пароль")]
        public string Password { get; set; } = null!;
    }
}
