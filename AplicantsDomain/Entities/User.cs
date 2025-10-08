using Microsoft.AspNetCore.Identity;

namespace Applicants.Domain.Entities;

public class User : IdentityUser<int>
{
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }


    public static User Instance(string userName, string password)
    {
        return new User
        {
            Email = userName,
            UserName = userName,
            IsActive = true,
            IsDeleted = false
        };
    }
}
