using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace Core
{
    public sealed class PasswordGenerator
    {
        public const int SaltSize = 8;
        public const string InvalidSaltSize = "The salt size is incorrect.  It is {0} but should be {1}.";
        public const string InvalidPassword = "Invalid password.";

        public string OriginalPassword { get; private set; }

        public ReadOnlyCollection<byte> Password { get; private set; }

        public ReadOnlyCollection<byte> Salt { get; private set; }

        public PasswordGenerator(string password)
            : this(password, PasswordGenerator.CreateSalt())
        {

        }

        public PasswordGenerator(string password, byte[] salt)
        {
            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }

            if (salt.Length != PasswordGenerator.SaltSize)
            {
                throw new ArgumentException(string.Format(PasswordGenerator.InvalidSaltSize, salt.Length, PasswordGenerator.SaltSize), "salt");
            }

            var saltMaterial = new byte[PasswordGenerator.SaltSize];
            Array.Copy(salt, saltMaterial, PasswordGenerator.SaltSize);
            this.Initialize(password, saltMaterial);
        }

        private void Initialize(string password, byte[] salt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(PasswordGenerator.InvalidPassword, "password");
            }

            this.OriginalPassword = password;
            this.Salt = new ReadOnlyCollection<byte>(salt);
            this.Password = this.CreatePassword(password);
        }

        private ReadOnlyCollection<byte> CreatePassword(string password)
        {
            var passwordBuffer = Encoding.Unicode.GetBytes(password);
            var material = new byte[passwordBuffer.Length + this.Salt.Count];

            Array.Copy(passwordBuffer, material, passwordBuffer.Length);
            using (var sha = new SHA512Managed())
            {
                return new ReadOnlyCollection<byte>(sha.ComputeHash(material));
            }
        }

        private static byte[] CreateSalt()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[PasswordGenerator.SaltSize];
                provider.GetNonZeroBytes(buffer);
                return buffer;
            }
        }
    }
}
