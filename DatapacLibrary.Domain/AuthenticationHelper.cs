using System.Security.Cryptography;
using System.Text;

namespace DatapacLibrary.Domain;

public static class AuthenticationHelper
{
    private const int keySize = 64;
    private const int iterations = 35000;
    private static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public static byte[] HashPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(keySize);
        return Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize
        );
    }

    public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            hashAlgorithm,
            keySize
        );
        return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
    }
}
