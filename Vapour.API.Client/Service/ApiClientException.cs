using System;

namespace Vapour.API.Client.Service
{
	public class ApiClientException : Exception
	{
		public ApiClientException(string message) : base(message)
		{
		}
	}
}
