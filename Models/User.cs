using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace BNI_Users_backend.Models
{
    public class User
    {
        public int id { get; set; }

        [Required(ErrorMessage="Nama depan harus diberikan")]
        public string? fName { get; set; }
        public string? lName { get; set; }
        public string? fullName
        {
            get 
            {
                string fullName = this.fName;
                if (this.lName != null && this.lName != "")
                {
                    fullName += " " + this.lName;
                }
                return fullName;
            }
        }

        [Required(ErrorMessage="Nomor telepon harus diberikan"), RegularExpression(@"^[0-9]+$")]
        public string? telephone { get; set; }

        [Required(ErrorMessage="Email harus diberikan"), EmailAddress]
        public string? email { get; set; }

        [Required(ErrorMessage="Tanggal lahir harus diberikan")]
        [DataType(DataType.Date)]
        public DateTime? dateOfBirth { get; set; }

        [Required(ErrorMessage="Deskripsi singkat harus diberikan")]
        public string? description { get; set; }

        [Required(ErrorMessage="Pertanyaan untuk autentikasi kedua harus diberikan")]
        public string? questionSecondAuthentication { get; set; }
    }

    public class UserValidator : AbstractValidator<User>
    {

        private bool isValidDate(string date)
        {
            DateTime value;
            bool isValid = DateTime.TryParse(date, out value);
            return isValid;
        }

        public UserValidator()
        {
            RuleFor(x => x.id).NotNull().WithMessage("Id dibutuhkan");
            RuleFor(x => x.fName).NotNull().WithMessage("Nama depan harus diberikan");
            RuleFor(x => x.fName).NotEmpty().WithMessage("Nama depan harus diberikan");
            RuleFor(x => x.telephone).NotNull().WithMessage("Nomor telepon harus diberikan");
            RuleFor(x => x.telephone).NotEmpty().WithMessage("Nomor telepon harus diberikan");
            RuleFor(x => x.email).NotNull().WithMessage("Email harus diberikan");
            RuleFor(x => x.email).NotEmpty().WithMessage("Email harus diberikan");
            RuleFor(x => x.dateOfBirth).NotNull().WithMessage("Tanggal lahir harus diberikan");
            RuleFor(x => x.dateOfBirth).NotEmpty().WithMessage("Tanggal lahir harus diberikan");
            RuleFor(x => x.description).NotNull().WithMessage("Deskripsi singkat harus diberikan");
            RuleFor(x => x.description).NotEmpty().WithMessage("Deskripsi singkat harus diberikan");
            RuleFor(x => x.questionSecondAuthentication).NotNull().WithMessage("Pertanyaan untuk autentikasi kedua harus diberikan");
            RuleFor(x => x.questionSecondAuthentication).NotEmpty().WithMessage("Pertanyaan untuk autentikasi kedua harus diberikan");
            RuleFor(x => x.telephone).Matches("^[0-9]+$").WithMessage("Nomor telephone tidak valid");
            RuleFor(x => x.email).EmailAddress().WithMessage("Email tidak valid");
        }
    }
}