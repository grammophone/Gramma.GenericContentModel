using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// A read-only dictionary of items indexed by their <see cref="IKeyedElement{K}.Key"/>
	/// property.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	public interface IReadOnlyMap<in K, out E> : IReadOnlyBag<E>
		where E : IKeyedElement<K>
	{
		#region Properties

		/// <summary>
		/// Access an element by its key.
		/// </summary>
		/// <param name="key">The key of the element.</param>
		/// <returns>
		/// Returns the element found, else throws a
		/// <see cref="System.Collections.Generic.KeyNotFoundException"/>.
		/// </returns>
		/// <exception cref="System.Collections.Generic.KeyNotFoundException">
		/// No element in the collection was found having key
		/// equal to the supplied <paramref name="key"/> parameter.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// The <paramref name="key"/> was null.
		/// </exception>
		E this[K key] { get; }

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
