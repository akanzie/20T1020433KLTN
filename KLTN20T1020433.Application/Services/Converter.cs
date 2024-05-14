using System.Globalization;

namespace KLTN20T1020433.Application.Services
{
    public static class Converter
    {
        /// <summary>
        /// Chuyển chuỗi s sang giá trị kiểu DateTime theo các formats được quy định
        /// (Hàm trả về null nếu chuyển không thành công)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string s, string formats = "dd/MM/yyyy hh:mm:ss tt;d/M/yyyy;d-M-yyyy;d.M.yyyy;yyyy-MM-ddTHH:mm")
        {
            try
            {
                return DateTime.ParseExact(s, formats.Split(';'), CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
        public const string TimeWithDateAndMonth = "H:mm d 'thg' M";
        public const string DateWithMonth = "d 'thg' M";
        public const string Time = "H:mm";
        public const string DateTimeLocal = "yyyy-MM-ddTHH:mm";
        public const string Date = "yyyy-MM-dd";
        public const string DateTimeSQL = "dd/MM/yyyy hh:mm:ss tt";
    }
}
