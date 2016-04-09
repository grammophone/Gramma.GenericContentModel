using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// A read-only dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	/// <typeparam name="C">The type of collection under a key. Must descend from <see cref="IReadOnlyBag{E}"/>.</typeparam>
	public interface IReadOnlyMultiDictionary<K, out E, out C> : IEnumerable<IReadOnlyKeyValuePair<K, C>>
		where C : IReadOnlyBag<E>
	{
		#region Properties

		/// <summary>
		/// Number of items in the dictionary.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Get the collection of items by the specified <paramref name="key"/>.
		/// If no items exist under the <paramref name="key"/>, an empty collection is returned.
		/// </summary>
		/// <param name="key">The key of the items.</param>
		/// <returns>
		/// Returns a read-only collection of items associated with the key, else returns an empty collection.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// The <paramref name="key"/> was null.
		/// </exception>
		C this[K key] { get; }

		/// <summary>
		/// The unique keys of all the items in the dictionary.
		/// </summary>
		IEnumerable<K> Keys { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Returns true if at least one item is associated with the specified <paramref name="key"/>.
		/// </summary>
		bool ContainsKey(K key);

		#endregion
	}

	/// <summary>
	/// A read-only dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	public interface IReadOnlyMultiDictionary<K, out E> : IReadOnlyMultiDictionary<K, E, IReadOnlyBag<E>>
	{
	}
}
