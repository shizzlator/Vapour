using System;

namespace Vapour.Domain
{
	/// <summary>
	/// The exception that is thrown when a vapour task does not complete correctly. 
	/// </summary>
	public class VapourException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VapourException"/> class.
		/// </summary>
		/// <param name="message">The exception message.</param>
		public VapourException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VapourException"/> class.
		/// </summary>
		/// <param name="message">The message as an <see cref="IFormattable"/> string.</param>
		/// <param name="args">Arguments for the message format.</param>
		public VapourException(string message, params object[] args) : base(string.Format(message, args))
		{
		}
	}
}
