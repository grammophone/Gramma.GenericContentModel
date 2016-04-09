using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IMultiDictionary{K, E, C}"/>,
	/// a dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	/// <typeparam name="C">The type of collection under a key. Must descend from <see cref="IReadOnlyBag{E}"/>.</typeparam>
	/// <typeparam name="CI">The implementation of <typeparamref name="C"/>. Must have default a constructor.</typeparam>
	[Serializable]
	public class MultiDictionary<K, E, C, CI> : ReadOnlyMultiDictionary<K, E, C, CI>, IMultiDictionary<K, E, C>
		where C : IReadOnlyBag<E>
		where CI : C, IInitializable<E>, new()
	{
		#region Construction

		/// <summary>
		/// Create empty dictionary.
		/// </summary>
		public MultiDictionary()
		{

		}

		/// <summary>
		/// Create from key-value pairs.
		/// </summary>
		public MultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, E>> keyValuePairs)
			: base(keyValuePairs)
		{

		}

		/// <summary>
		/// Create from key-collection pairs.
		/// </summary>
		public MultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, C>> keyCollectionPairs)
			: base(keyCollectionPairs)
		{

		}

		/// <summary>
		/// Create from items collection and key maper.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="keyMapper">A function to return the key corresponding to an item.</param>
		public MultiDictionary(IEnumerable<E> items, Func<E, K> keyMapper)
			: base(items, keyMapper)
		{

		}

		#endregion

		#region IMultiDictionary<K,E> Members

		/// <summary>
		/// Add an item associated with a key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="item">The item.</param>
		/// <returns>
		/// Returns true if addition was successful and 
		/// no equal item preexisted under the same key, else returns false.
		/// </returns>
		public bool Add(K key, E item)
		{
			return AddItem(key, item);
		}

		/// <summary>
		/// Clear all items in the dictionary.
		/// </summary>
		public void Clear()
		{
			itemsDictionary.Clear();
			count = 0;
		}

		#endregion
	}

	/// <summary>
	/// An implementation of <see cref="IMultiDictionary{K, E, C}"/>,
	/// a dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	[Serializable]
	public class MultiDictionary<K, E> : MultiDictionary<K, E, IReadOnlyBag<E>, ReadOnlyBag<E>>, IMultiDictionary<K, E>
	{
		#region Construction

		/// <summary>
		/// Create empty dictionary.
		/// </summary>
		public MultiDictionary()
		{

		}

		/// <summary>
		/// Create from key-value pairs.
		/// </summary>
		public MultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, E>> keyValuePairs)
			: base(keyValuePairs)
		{

		}

		/// <summary>
		/// Create from key-collection pairs.
		/// </summary>
		public MultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, ReadOnlyBag<E>>> keyCollectionPairs)
			: base(keyCollectionPairs)
		{

		}

		/// <summary>
		/// Create from items collection and key maper.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="keyMapper">A function to return the key corresponding to an item.</param>
		public MultiDictionary(IEnumerable<E> items, Func<E, K> keyMapper)
			: base(items, keyMapper)
		{

		}

		#endregion

		#region IReadOnlyMultiDictionary<K,E,IReadOnlyBag<E>> Members

		IReadOnlyBag<E> IReadOnlyMultiDictionary<K, E, IReadOnlyBag<E>>.this[K key]
		{
			get { return this[key]; }
		}

		#endregion

		#region IEnumerable<IReadOnlyKeyValuePair<K,IReadOnlyBag<E>>> Members

		IEnumerator<IReadOnlyKeyValuePair<K, IReadOnlyBag<E>>> IEnumerable<IReadOnlyKeyValuePair<K, IReadOnlyBag<E>>>.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
