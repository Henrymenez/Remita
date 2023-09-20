using System.ComponentModel.DataAnnotations.Schema;

namespace Remita.Models.Entities.Domians.User;

public class RefreshToken : BaseEntity
{
    public string UserId { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;
    public string Token { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime ExpiryDate { get; set; }
}
