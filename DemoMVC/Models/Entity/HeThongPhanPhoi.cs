using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entity
{
    public class HeThongPhanPhoi
    {
        [Key]
        [Display(Name = "Mã Hệ Thống Phân Phối")]
        public string MaHTPP { get; set; } = default!;
        [Display(Name = "Tên Hệ Thống Phân Phối")]
        public string TenHTPP { get; set; } = default!;

        public virtual ICollection<DaiLy> DaiLys { get; set; } = default!;
    }
}
