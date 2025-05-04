using Microsoft.AspNetCore.Identity;

namespace IdentityChatEmail.Models
{
    public class CustomIdentityValidator : IdentityErrorDescriber
    {
        // Şifrede büyük harf gereksinimi
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresUpper",
                Description = "Şifreniz en az bir büyük harf içermelidir."
            };
        }

        // Şifrede rakam gereksinimi
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresDigit",
                Description = "Şifreniz en az bir rakam içermelidir."
            };
        }

        // Şifrede küçük harf gereksinimi
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresLower",
                Description = "Şifreniz en az bir küçük harf içermelidir."
            };
        }

        // Şifrede özel karakter gereksinimi
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Şifreniz en az bir özel karakter içermelidir."
            };
        }

        // Şifre uzunluk gereksinimi (minimum uzunluk)
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = "PasswordTooShort",
                Description = $"Şifreniz en az {length} karakter uzunluğunda olmalıdır."
            };
        }

        // Kullanıcı adı için mevcut olmama hatası
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "DuplicateEmail",
                Description = $"Bu e-posta adresi ({email}) zaten kullanılıyor. Lütfen farklı bir e-posta adresi kullanın."
            };
        }

        // Kullanıcı adı için mevcut olmama hatası
        public virtual IdentityError DuplicateUsername()
        {
            return new IdentityError
            {
                Code = "DuplicateUsername",
                Description = "Bu kullanıcı adı zaten mevcut."
            };
        }


        // E-posta formatı hatası
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = "InvalidEmail",
                Description = $"Geçersiz e-posta adresi: {email}. Lütfen geçerli bir e-posta adresi girin."
            };
        }

        // Kullanıcı adı uzunluğu hatası (çok kısa)
        public virtual IdentityError UsernameTooShort()
        {
            return new IdentityError
            {
                Code = "UsernameTooShort",
                Description = "Kullanıcı adı çok kısa."
            };
        }


        // Kullanıcı adı uzunluğu hatası (çok uzun)
        public virtual IdentityError UsernameTooLong()
        {
            return new IdentityError
            {
                Code = "UsernameTooLong",
                Description = "Kullanıcı adı çok uzun."
            };
        }

    }
}
