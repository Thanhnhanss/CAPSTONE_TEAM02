namespace VanLangDoctor.Models
{
    public class TrangThaiDatLichKham
    {
        public static string GetDescription(int trangThai)
        {
            if (trangThai == 0)
            {
                return "Chưa duyệt";
            }
            else if (trangThai == 1)
            {
                return "Đã duyệt";
            }
            else
            {
                return "Bận";
            }
        }
    }
}