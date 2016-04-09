using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An ordered immutable collection of 'child' objects belonging to a 'parent',
	/// indexed by their key property.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	/// <typeparam name="K">The type of the children's key.</typeparam>
	public interface IOrderedKeyedReadOnlyChildren<out P, in K, out C> : IKeyedReadOnlyChildren<P, K, C>
		where C : class, IKeyedChild<P, K>
		where P : class
	{
		/// <summary>
		/// Get an item by index.
		/// </summary>
		/// <param name="index">the zero-based index of the item.</param>
		/// <returns>Returns the item at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown when the <paramref name="index"/> is negative or 
		/// not less than Count.
		/// </exception>
		C this[int index] { get; }

		/// <summary>
		/// Get the items in the collection.
		/// </summary>
		/// <returns>Returns an array holding the items in the order they were given.</returns>
		/// <remarks>
		/// The returned array is cached. Changing its items might affect other callers.
		/// </remarks>
		C[] ToArray();
	}
}
