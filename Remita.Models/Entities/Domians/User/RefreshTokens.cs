using System.ComponentModel.DataAnnotations.Schema;

namespace Remita.Models.Entities.Domians.User;

public class RefreshTokens
{
    public string UserId { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
}
