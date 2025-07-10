using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entity
{
    public class HeThongPhanPhoi
    {
        [Key]
        public string MaHTPP { get; set; } = default!;
        public string TenHTPP { get; set; } = default!;

        public virtual ICollection<DaiLy> DaiLys { get; set; } = default!;
    }
}
