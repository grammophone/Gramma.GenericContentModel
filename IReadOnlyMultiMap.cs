using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// A read-only dictionary of elements indexed by their <see cref="IKeyedElement{K}.Key"/>
	/// property, allowing for multiple elements having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the key.</typeparam>
	/// <typeparam name="E">The type of the element.</typeparam>
	public interface IReadOnlyMultiMap<in K, out E> : IReadOnlyBag<E>
		where E : IKeyedElement<K>
	{
		#region Properties

		/// <summary>
		/// Get the elements having the specified <paramref name="key"/>.
		/// </summary>
		/// <param name="key">The key to search for.</param>
		/// <returns>
		/// Returns a read-only collection of the elements having the specified <paramref name="key"/>.
		/// If no item exists under the given key, returns an empty collection.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// The <paramref name="key"/> was null.
		/// </exception>
		IReadOnlyBag<E> this[K key] { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Check if the collection contains an element having the
		/// given <paramref name="key"/>.
		/// </summary>
		/// <param name="key">The key of the element.</param>
		/// <returns>
		/// Returns true if an element having the supplied <paramref name="key"/> exists
		/// in the collection.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// The <paramref name="key"/> was null.
		/// </exception>
		bool ContainsKey(K key);

		#endregion
	}
}
