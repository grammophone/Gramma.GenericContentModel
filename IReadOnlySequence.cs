using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Interface for read-only container of ordered items.
	/// </summary>
	/// <typeparam name="T">The type of items in the container.</typeparam>
	/// <remarks>
	/// The interface isn't called IReadOnlyList in order to avoid collision with
	/// the upcoming System.Collections.Generic.IReadOnlyCollection interface of .NET 4.5.
	/// </remarks>
	public interface IReadOnlySequence<out T> : 
		IReadOnlyBag<T>
#if NET_45_OR_GREATER
		, IReadOnlyList<T>
#endif
	{
#if !NET_45_OR_GREATER
		
		#region Properties

		/// <summary>
		/// Get collection item by its index.
		/// </summary>
		/// <param name="index">The index of the item.</param>
		/// <returns>Returns the item at the index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// When <paramref name="index"/> is negative or 
		/// greater or equal to <see cref="IReadOnlyBag{T}.Count"/>.
		/// </exception>
		T this[int index] { get; }

		#endregion

#endif

		#region Methods

		/// <summary>
		/// Get the elements of the collection as an array.
		/// </summary>
		/// <returns>Returns the array.</returns>
		/// <remarks>
		/// The returned array is cached. Changing its items might affect other callers.
		/// </remarks>
		T[] ToArray();

		#endregion
	}
}
