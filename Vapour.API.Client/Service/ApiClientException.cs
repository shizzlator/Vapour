using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vapour.API.Client.Service
{
	public class ApiClientException : Exception
	{
		public ApiClientException(string message) : base(message)
		{
		}
	}
}
