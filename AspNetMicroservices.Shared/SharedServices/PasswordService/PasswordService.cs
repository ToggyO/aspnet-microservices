using System;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AspNetMicroservices.Shared.SharedServices.PasswordService
{
	/// <inheritdoc cref="IPasswordService"/>.
	public class PasswordService : IPasswordService
	{
		/// <inheritdoc cref="IPasswordService.CreatePassword(string)"/>.
		public PasswordModel CreatePassword(string password)
			=> CreatePassword(password, Convert.ToBase64String(GenerateRandomBytes()));

		/// <inheritdoc cref="IPasswordService.CreatePassword(string, string)"/>.
		public PasswordModel CreatePassword(string password, string salt)
		{
			var hashBytes = KeyDerivation.Pbkdf2(
				password, Convert.FromBase64String(salt), KeyDerivationPrf.HMACSHA1, 10000, 32);

			return new PasswordModel
			{
				Salt = salt,
				Hash = Convert.ToBase64String(hashBytes)
			};
		}

		/// <inheritdoc cref="IPasswordService.VerifyPassword"/>.
		public bool VerifyPassword(PasswordModel currentPassword, string verifyPassword)
		{
			var verifyModel = CreatePassword(verifyPassword, currentPassword.Salt);
			return string.Equals(currentPassword.Hash, verifyModel.Hash, StringComparison.Ordinal);
		}

		/// <inheritdoc cref="IPasswordService.GenerateSalt"/>.
		public string GenerateSalt() => Convert.ToBase64String(GenerateRandomBytes());

		private byte[] GenerateRandomBytes()
		{
			var bytes = new byte[16];

			using var rngCrypto = new RNGCryptoServiceProvider();
			rngCrypto.GetNonZeroBytes(bytes);

			return bytes;
		}
	}
}