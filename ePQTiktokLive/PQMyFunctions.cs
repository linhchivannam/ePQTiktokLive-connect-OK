using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ePQTiktokLive
{
    public static class PQMyFunctions
    {
        static Dictionary<string, string> digitWords = new Dictionary<string, string>()
    {
        {"không", "0"}, {"mot", "1"}, {"một", "1"},
        {"hai", "2"}, {"ba", "3"}, {"bốn", "4"}, {"bon", "4"},
        {"năm", "5"}, {"nam", "5"}, {"sáu", "6"}, {"sau", "6"},
        {"bảy", "7"}, {"bay", "7"}, {"bay", "7"}, {"tám", "8"},
        {"tam", "8"}, {"chín", "9"}, {"chin", "9"},
        {"0", "0"}, {"1", "1"}, {"2", "2"}, {"3", "3"},
        {"4", "4"}, {"5", "5"}, {"6", "6"}, {"7", "7"},
        {"8", "8"}, {"9", "9"}
    };

        public static (string phoneNumber, bool isValidPhone) ExtractPhoneNumber(string input)
        {
            int atIndex = input.IndexOf('@');
            if (atIndex == -1 || atIndex == input.Length - 1)
                return (null, false);

            string afterAt = input.Substring(atIndex + 1).ToLower();

            // Tách từ (chữ cái và số) bỏ ký tự đặc biệt
            var tokens = Regex.Split(afterAt, @"[^a-zA-Z0-9]+")
                              .Where(t => !string.IsNullOrWhiteSpace(t)).ToList();

            string phone = "";
            foreach (var token in tokens)
            {
                if (digitWords.TryGetValue(token, out string digit))
                    phone += digit;
            }

            // Kiểm tra hợp lệ độ dài (VN thường 10 hoặc 11 số)
            bool isValid = phone.Length >= 9 && phone.Length <= 11;
            return (isValid ? phone : null, isValid);
        }
        public static bool IsValidBase64String(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return false;

            // Cắt khoảng trắng và ký tự xuống dòng nếu có
            base64 = base64.Trim();

            // Độ dài phải chia hết cho 4
            if (base64.Length % 4 != 0)
                return false;

            // Regex kiểm tra định dạng Base64
            return Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,2}$", RegexOptions.None);
        }
    }

}
