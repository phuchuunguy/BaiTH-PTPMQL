using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entity
{
    public class DaiLy
    {
        [Key]
        public string MaDaiLy { get; set; } = default!;
        public string TenDaiLy { get; set; } = default!;
        public string DiaChi { get; set; } = default!;
        public string NguoiDaiDien { get; set; } = default!;
        public string DienThoai { get; set; } = default!;

        [ForeignKey("HeThongPhanPhoi")]
        public string MaHTPP { get; set; } = default!;
        public virtual HeThongPhanPhoi HeThongPhanPhoi { get; set; } = default!;
    }
}
