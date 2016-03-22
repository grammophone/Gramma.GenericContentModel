using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// Represents a read-only collection.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	/// <remarks>
	/// The interface isn't called IReadOnlyCollection in order to avoid collision with
	/// the upcoming System.Collections.Generic.IReadOnlyCollection interface of .NET 4.5.
	/// </remarks>
	public interface IReadOnlyBag<out T> : 
		IEnumerable<T>
#if NET_45_OR_GREATER
		, IReadOnlyCollection<T>
#endif 
	{
#if !NET_45_OR_GREATER
		#region Properties

		/// <summary>
		/// The number of elements in the collection.
		/// </summary>
		int Count { get; }

		#endregion
#endif
	}
}
