using System;

namespace Intune.Shared.Model
{
    public class UserAccountShareRole
    {
        public int UserId { get; set; }
        public UserAccountRole Role { get; set; }

        public UserAccountShareRole() { }
    }
}
