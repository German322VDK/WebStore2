using System;

namespace WebStore.Domain.DTO.Identity
{
    public class SetLockOutDTO : UserDTO
    {
        public DateTimeOffset? LockOutEnd { get; set; }
    }
}
