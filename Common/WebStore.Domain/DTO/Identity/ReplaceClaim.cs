using System.Security.Claims;

namespace WebStore.Domain.DTO.Identity
{
    public class ReplaceClaim : UserDTO
    {
        public Claim Claim { get; set; }

        public Claim NewClaim { get; set; }


    }
}
