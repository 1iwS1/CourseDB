using System.Security.Cryptography;
using System.Text;

using CourseBD.Enums;

namespace CourseBD.Users
{
  public class User
  {
    public ActiveUser UserName { get; set; }
    public string UserPassword { get; set; }

    public string PasswordHashing()
    {
      byte[] data = Encoding.UTF8.GetBytes(UserPassword);

      SHA256 sha = SHA256.Create();
      byte[] hash = sha.ComputeHash(data);

      StringBuilder str = new();
      foreach (var a in hash)
        str.Append(a.ToString("X2"));

      return str.ToString();
    }
  }
}
