using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Implementation of <see cref="IReadOnlyMultiDictionary{K, E, C}"/>,
	/// a read-only dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	/// <typeparam name="C">The type of collection under a key. Must descend from <see cref="IReadOnlyBag{E}"/>.</typeparam>
	/// <typeparam name="CI">The implementation of <typeparamref name="C"/>. Must have default a constructor.</typeparam>
	[Serializable]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DebuggerProxies.MultiDictionaryDebuggerProxy<, ,>))]
	public class ReadOnlyMultiDictionary<K, E, C, CI> : IReadOnlyMultiDictionary<K, E, C>
		where C : IReadOnlyBag<E>
		where CI : C, IInitializable<E>, new()
	{
		#region Protected fields

		protected readonly Dictionary<K, CI> itemsDictionary;

		protected int count;

		protected static readonly CI emptyItemsCollection = new CI();

		#endregion

		#region Construction

		/// <summary>
		/// Create an empty dictionary.
		/// </summary>
		public ReadOnlyMultiDictionary()
		{
			this.itemsDictionary = new Dictionary<K, CI>();
			this.count = 0;
		}

		/// <summary>
		/// Create an emty dictionary with an initial capacity of keys.
		/// </summary>
		/// <param name="keysCapacity">The expected number of keys.</param>
		public ReadOnlyMultiDictionary(int keysCapacity)
		{
			this.itemsDictionary = new Dictionary<K, CI>(keysCapacity);
			this.count = 0;
		}

		/// <summary>
		/// Create from key-value pairs.
		/// </summary>
		public ReadOnlyMultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, E>> keyValuePairs)
			: this()
		{
			if (keyValuePairs == null) throw new ArgumentNullException("keyValuePairs");

			foreach (var pair in keyValuePairs)
			{
				AddItem(pair.Key, pair.Value);
			}
		}

		/// <summary>
		/// Create from key-collection pairs.
		/// </summary>
		public ReadOnlyMultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, C>> keyCollectionPairs)
			: this(keyCollectionPairs.Count())
		{
			if (keyCollectionPairs == null) throw new ArgumentNullException("keyCollectionPairs");

			foreach (var keyCollectionPair in keyCollectionPairs)
			{
				foreach (var item in keyCollectionPair.Value)
				{
					AddItem(keyCollectionPair.Key, item);
				}
			}
		}

		/// <summary>
		/// Create from items collection and key maper.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="keyMapper">A function to return the key corresponding to an item.</param>
		public ReadOnlyMultiDictionary(IEnumerable<E> items, Func<E, K> keyMapper)
			: this()
		{
			if (items == null) throw new ArgumentNullException("items");
			if (keyMapper == null) throw new ArgumentNullException("keyMapper");

			foreach (var item in items)
			{
				AddItem(keyMapper(item), item);
			}
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
		public static ReadOnlyMultiDictionary<K, E, C, CI> CreateByMasterDetailCollection<T>(
			IEnumerable<T> sourceCollection,
			Func<T, K> keyMapper,
			Func<T, IEnumerable<E>> elementsMapper)
		{
			if (sourceCollection == null) throw new ArgumentNullException("sourceCollection");
			if (keyMapper == null) throw new ArgumentNullException("keyMapper");
			if (elementsMapper == null) throw new ArgumentNullException("elementsMapper");

			var dictionary = new ReadOnlyMultiDictionary<K, E, C, CI>(sourceCollection.Count());

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
		public static ReadOnlyMultiDictionary<K, E, C, CI> Create<I>(
			IEnumerable<I> sourceCollection,
			Func<I, K> keyMapper,
			Func<I, E> valueMapper)
		{
			if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
			if (keyMapper == null) throw new ArgumentNullException(nameof(keyMapper));
			if (valueMapper == null) throw new ArgumentNullException(nameof(valueMapper));

			var dictionary = new ReadOnlyMultiDictionary<K, E, C, CI>();

			foreach (var item in sourceCollection)
			{
				dictionary.AddItem(keyMapper(item), valueMapper(item));
			}

			return dictionary;
		}

		#endregion

		#region IReadOnlyMultiDictionary<K,E> Members

		/// <summary>
		/// The number of all items in the dictionary.
		/// </summary>
		/// <remarks>
		/// This is the count of items, not keys.
		/// </remarks>
		public int Count
		{
			get { return count; }
		}

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
		public C this[K key]
		{
			get
			{
				CI items;

				if (itemsDictionary.TryGetValue(key, out items))
				{
					return items;
				}
				else
				{
					return emptyItemsCollection;
				}
			}
		}

		/// <summary>
		/// The unique keys of all the items in the dictionary.
		/// </summary>
		public IEnumerable<K> Keys
		{
			get
			{
				return itemsDictionary.Keys;
			}
		}

		/// <summary>
		/// Returns true if at least one item is associated with the specified <paramref name="key"/>.
		/// </summary>
		public bool ContainsKey(K key)
		{
			return itemsDictionary.ContainsKey(key);
		}

		#endregion

		#region IEnumerable<IReadOnlyKeyValuePair<K,C>> Members

		public IEnumerator<IReadOnlyKeyValuePair<K, C>> GetEnumerator()
		{
			var query = from entry in itemsDictionary
									select new ReadOnlyKeyValuePair<K, C>(entry.Key, entry.Value);

			return query.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Called when an item is added to the collection.
		/// </summary>
		/// <param name="key">The key of the item.</param>
		/// <param name="item">The added item.</param>
		/// <returns>Returns true if the element was not already in the collection and added successfully, else false.</returns>
		internal protected virtual bool AddItem(K key, E item)
		{
			if (key == null) throw new ArgumentNullException("key");

			CI items;

			if (!itemsDictionary.TryGetValue(key, out items))
			{
				items = new CI();
				itemsDictionary.Add(key, items);
			}

			if (items.AddItem(item))
			{
				count++;
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion
	}

	/// <summary>
	/// Implementation of <see cref="IReadOnlyMultiDictionary{K, E}"/>,
	/// a read-only dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// Allows specification of the implementation of collection of items under a key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	/// <typeparam name="CI">The implementation of <see cref="IReadOnlyBag{E}"/>. Must have default a constructor.</typeparam>
	[Serializable]
	public class ReadOnlyMultiDictionary<K, E, CI> :
		ReadOnlyMultiDictionary<K, E, IReadOnlyBag<E>, CI>, IReadOnlyMultiDictionary<K, E>
		where CI : IReadOnlyBag<E>, IInitializable<E>, new()
	{
		#region Construction

		/// <summary>
		/// Create an empty dictionary.
		/// </summary>
		public ReadOnlyMultiDictionary()
		{
		}

		/// <summary>
		/// Create an emty dictionary with an initial capacity of keys.
		/// </summary>
		/// <param name="keysCapacity">The expected number of keys.</param>
		public ReadOnlyMultiDictionary(int keysCapacity)
			: base(keysCapacity)
		{

		}

		/// <summary>
		/// Create from key-value pairs.
		/// </summary>
		public ReadOnlyMultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, E>> keyValuePairs)
			: base(keyValuePairs)
		{
		}

		/// <summary>
		/// Create from key-collection pairs.
		/// </summary>
		public ReadOnlyMultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, ReadOnlyBag<E>>> keyCollectionPairs)
			: base(keyCollectionPairs)
		{
		}

		/// <summary>
		/// Create from items collection and key maper.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="keyMapper">A function to return the key corresponding to an item.</param>
		public ReadOnlyMultiDictionary(IEnumerable<E> items, Func<E, K> keyMapper)
			: base(items, keyMapper)
		{
		}

		#endregion
	}

	/// <summary>
	/// Implementation of <see cref="IReadOnlyMultiDictionary{K, E}"/>,
	/// a read-only dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	[Serializable]
	public class ReadOnlyMultiDictionary<K, E> : 
		ReadOnlyMultiDictionary<K, E, ReadOnlyBag<E>>
	{
		#region Construction

		/// <summary>
		/// Create an empty dictionary.
		/// </summary>
		public ReadOnlyMultiDictionary()
		{
		}

		/// <summary>
		/// Create an emty dictionary with an initial capacity of keys.
		/// </summary>
		/// <param name="keysCapacity">The expected number of keys.</param>
		public ReadOnlyMultiDictionary(int keysCapacity)
			: base(keysCapacity)
		{

		}

		/// <summary>
		/// Create from key-value pairs.
		/// </summary>
		public ReadOnlyMultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, E>> keyValuePairs)
			: base(keyValuePairs)
		{
		}

		/// <summary>
		/// Create from key-collection pairs.
		/// </summary>
		public ReadOnlyMultiDictionary(IEnumerable<IReadOnlyKeyValuePair<K, ReadOnlyBag<E>>> keyCollectionPairs)
			: base(keyCollectionPairs)
		{
		}

		/// <summary>
		/// Create from items collection and key maper.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="keyMapper">A function to return the key corresponding to an item.</param>
		public ReadOnlyMultiDictionary(IEnumerable<E> items, Func<E, K> keyMapper)
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
		public static new ReadOnlyMultiDictionary<K, E> CreateByMasterDetailCollection<T>(
			IEnumerable<T> sourceCollection,
			Func<T, K> keyMapper,
			Func<T, IEnumerable<E>> elementsMapper)
		{
			if (sourceCollection == null) throw new ArgumentNullException("sourceCollection");
			if (keyMapper == null) throw new ArgumentNullException("keyMapper");
			if (elementsMapper == null) throw new ArgumentNullException("elementsMapper");

			var dictionary = new ReadOnlyMultiDictionary<K, E>();

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
		public static new ReadOnlyMultiDictionary<K, E> Create<I>(
			IEnumerable<I> sourceCollection,
			Func<I, K> keyMapper,
			Func<I, E> valueMapper)
		{
			if (sourceCollection == null) throw new ArgumentNullException(nameof(sourceCollection));
			if (keyMapper == null) throw new ArgumentNullException(nameof(keyMapper));
			if (valueMapper == null) throw new ArgumentNullException(nameof(valueMapper));

			var dictionary = new ReadOnlyMultiDictionary<K, E>();

			foreach (var item in sourceCollection)
			{
				dictionary.AddItem(keyMapper(item), valueMapper(item));
			}

			return dictionary;
		}

		#endregion
	}
}
