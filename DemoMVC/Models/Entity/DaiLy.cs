using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entity
{
    public class DaiLy
    {
        [Key]
        [Display(Name = "Mã Đại Lý")]
        public string MaDaiLy { get; set; } = default!;
        [Display(Name = "Tên Đại Lý")]
        public string TenDaiLy { get; set; } = default!;
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; } = default!;
         [Display(Name = "Người Đại Diện")]
        public string NguoiDaiDien { get; set; } = default!;
         [Display(Name = "Điện Thoại")]
        public string DienThoai { get; set; } = default!;

        [ForeignKey("MaHTPP")]
        [Display(Name = "Mã Hệ Thống Phân Phối")]
        public string? MaHTPP { get; set; }
        [ForeignKey("MaHTPP")]
        public HeThongPhanPhoi? HTPP { get; set; }
    }
}
