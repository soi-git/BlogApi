using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        
        [StringLength(200, ErrorMessage = "Длина строки должна быть до 200 символов")]
        public string Title { get; set; } = null!;

        [StringLength(250, ErrorMessage = "Длина строки должна быть до 250 символов")]
        public string? Summary { get; set; }
        public string? ContentText { get; set; }

        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Некорректная дата")]
        public string CreateDate { get; set; } = null!;

        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Некорректная дата")]
        public string? LastUpdateDate { get; set; }

        [Required(ErrorMessage = "Не указано значение Id для автомобиля")]
        public int UserId { get; set; }
        public CommentModel? Comments { get; set; }
    }
}
