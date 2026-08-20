using Infrastructure.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.JwtModule
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(string userId,string email,IEnumerable<string> roles);
    }
}
