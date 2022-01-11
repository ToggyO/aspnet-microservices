namespace AspNetMicroservices.Auth.Application.Common.Enums
{
	/// <summary>
	/// Represents token status enumeration.
	/// </summary>
	public enum TokenStatus
	{
		Invalid = -1,
		Valid = 0,
		Expired = 1
	}
}