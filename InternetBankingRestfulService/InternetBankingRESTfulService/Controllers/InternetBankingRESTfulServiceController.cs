using System;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetBankingRESTfulService.Api
{

    [ApiController]
    public class InternetBankingRESTfulServiceController : ControllerBase , IInternetBankingApi
    {

        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("/bank/api/version")]
        [Route("/bank/api-version")]
        public string GetApiVersion()
        {
            return  DateTime.UtcNow.ToMyCulture(CultureInfo.InvariantCulture);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("bank/api/calc/MD5/{value}")]
        [Route("bank/api/calc/{value}/MD5")]
        public string CalculateMD5(string value)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(value);
            var hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("X2"));
            }
            return sb.ToString();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("bank/api/password/strong/{password}")]
        [Route("bank/api/is-password-strong/{password}")]
        public bool IsPasswordStrong(string password)
        {
            const int PASSWORDLENGTH = 8;
            if (password.Length < PASSWORDLENGTH)
                return false;

            var hasUpperCaseLetter = false ;
            var hasLowerCaseLetter = false ;
            var hasDigit = false;
            var hasOtherChar = false;
            foreach (char ch in password)
            {
                if (char.IsUpper(ch))
                    hasUpperCaseLetter = true;
                if (char.IsLower(ch))
                    hasLowerCaseLetter = true;
                if (char.IsDigit(ch))
                    hasDigit = true;

            }
            foreach (char ch in password)
            {
                if (!char.IsUpper(ch) && !char.IsLower(ch) && !char.IsDigit(ch))
                    hasOtherChar = true;
            }

            return hasUpperCaseLetter && hasLowerCaseLetter && hasDigit && hasOtherChar;
        }
    }
}