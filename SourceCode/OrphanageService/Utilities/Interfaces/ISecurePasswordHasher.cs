using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Utilities.Interfaces
{
    public interface ISecurePasswordHasher
    {
        string Hash(string password);

        string Hash(string password, int iterations);

        bool IsHashSupported(string hashString);

        bool Verify(string password, string hashedPassword);
    }
}