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

		/// <summary>
		/// Create multi-dictionary from a "master-detail" collection whose entries are 
		/// objects having a key and the corresponding values.
		/// </summary>
		/// <typeparam name="T">The type of the entries in the "master-detail" collection.</typeparam>
		/// <param name="sourceCollection">The "master-detail" collection.</param>
		/// <param name="keyMapper">Maps an entry in the <paramref name="sourceCollection"/> to its key.</param>
		/// <param name="elementsMapper">Maps an entry in the <paramref name="sourceCollection"/> to its values.</param>
		/// <returns>Returns the dictionary.</returns>
		public static new MultiDictionary<K, E, C, CI> CreateByMasterDetailCollection<T>(
			IEnumerable<T> sourceCollection,
			Func<T, K> keyMapper,
			Func<T, IEnumerable<E>> elementsMapper)
		{
			if (sourceCollection == null) throw new ArgumentNullException("sourceCollection");
			if (keyMapper == null) throw new ArgumentNullException("keyMapper");
			if (elementsMapper == null) throw new ArgumentNullException("elementsMapper");

			var dictionary = new MultiDictionary<K, E, C, CI>();

			foreach (var entry in sourceCollection)
			{
				var key = keyMapper(entry);

				foreach (var element in elementsMapper(entry))
				{
					dictionary.AddItem(key, element);
				}
			}

			return dictionary;
		}

		/// <summary>
		/// Create using a source collection and functions to map keys and values from items of the source collection.
		/// </summary>
		/// <typeparam name="I">The type of items in the sourcecollection.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to get a key from an item in the <paramref name="sourceCollection"/>.</param>
		/// <param name="valueMapper">Function to get a value from an item in the <paramref name="sourceCollection"/>.</param>
		/// <returns>Returns the dictionary.</returns>
		public static new MultiDictionary<K, E, C, CI> Create<I>(
			IEnumerable<I> sourceCollection,
			Func<I, K> keyMapper,
			Func<I, E> valueMapper)
		{
			if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
			if (keyMapper == null) throw new ArgumentNullException(nameof(keyMapper));
			if (valueMapper == null) throw new ArgumentNullException(nameof(valueMapper));

			var dictionary = new MultiDictionary<K, E, C, CI>();

			foreach (var item in sourceCollection)
			{
				dictionary.AddItem(keyMapper(item), valueMapper(item));
			}

			return dictionary;
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

		/// <summary>
		/// Create multi-dictionary from a "master-detail" collection whose entries are 
		/// objects having a key and the corresponding values.
		/// </summary>
		/// <typeparam name="T">The type of the entries in the "master-detail" collection.</typeparam>
		/// <param name="sourceCollection">The "master-detail" collection.</param>
		/// <param name="keyMapper">Maps an entry in the <paramref name="sourceCollection"/> to its key.</param>
		/// <param name="elementsMapper">Maps an entry in the <paramref name="sourceCollection"/> to its values.</param>
		/// <returns>Returns the dictionary.</returns>
		public static new MultiDictionary<K, E> CreateByMasterDetailCollection<T>(
			IEnumerable<T> sourceCollection,
			Func<T, K> keyMapper,
			Func<T, IEnumerable<E>> elementsMapper)
		{
			if (sourceCollection == null) throw new ArgumentNullException("sourceCollection");
			if (keyMapper == null) throw new ArgumentNullException("keyMapper");
			if (elementsMapper == null) throw new ArgumentNullException("elementsMapper");

			var dictionary = new MultiDictionary<K, E>();

			foreach (var entry in sourceCollection)
			{
				var key = keyMapper(entry);

				foreach (var element in elementsMapper(entry))
				{
					dictionary.AddItem(key, element);
				}
			}

			return dictionary;
		}

		/// <summary>
		/// Create using a source collection and functions to map keys and values from items of the source collection.
		/// </summary>
		/// <typeparam name="I">The type of items in the sourcecollection.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to get a key from an item in the <paramref name="sourceCollection"/>.</param>
		/// <param name="valueMapper">Function to get a value from an item in the <paramref name="sourceCollection"/>.</param>
		/// <returns>Returns the dictionary.</returns>
		public static new MultiDictionary<K, E> Create<I>(
			IEnumerable<I> sourceCollection,
			Func<I, K> keyMapper,
			Func<I, E> valueMapper)
		{
			if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
			if (keyMapper == null) throw new ArgumentNullException(nameof(keyMapper));
			if (valueMapper == null) throw new ArgumentNullException(nameof(valueMapper));

			var dictionary = new MultiDictionary<K, E>();

			foreach (var item in sourceCollection)
			{
				dictionary.AddItem(keyMapper(item), valueMapper(item));
			}

			return dictionary;
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
