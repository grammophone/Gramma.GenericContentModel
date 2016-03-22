using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// An implementation of a dictionary of elements indexed by their <see cref="IKeyedElement{K}.Key"/>
	/// property, allowing for multiple elements having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the key.</typeparam>
	/// <typeparam name="E">The type of the element.</typeparam>
	[Serializable]
	public class MultiMap<K, E> : ReadOnlyMultiMap<K, E>, IMultiMap<K, E>
		where E : IKeyedElement<K>
	{
		#region IMultimap<K, E> implementation

		/// <summary>
		/// Add an item.
		/// </summary>
		/// <param name="item">The item to add.</param>
		/// <returns>Returns false if the item is already member of the collection, else true.</returns>
		public bool Add(E item)
		{
			return this.AddItem(item);
		}

		/// <summary>
		/// Add a collection of items.
		/// </summary>
		/// <param name="items">The collection of items to add.</param>
		public void AddAll(IEnumerable<E> items)
		{
			if (items == null) throw new ArgumentNullException("items");

			foreach (var item in items)
			{
				this.AddItem(item);
			}
		}

		/// <summary>
		/// Clear all items.
		/// </summary>
		public void Clear()
		{
			map.Clear();
			count = 0;
		}

		/// <summary>
		/// Remove an item from the collection.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		/// <returns>Returns true if the item was found and removed, else false.</returns>
		public bool Remove(E item)
		{
			if (item == null) throw new ArgumentNullException("item");

			ReadOnlyBag<E> itemsUnderKey;

			if (map.TryGetValue(item.Key, out itemsUnderKey))
			{
				bool removed = itemsUnderKey.RemoveItem(item);

				if (removed)
				{
					count--;

					if (itemsUnderKey.Count == 0)
					{
						map.Remove(item.Key);
					}

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Remove all items under a specified <paramref name="key"/>.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		/// Returns true if a non-empty collection was found under the <paramref name="key"/>.
		/// </returns>
		public bool RemoveKey(K key)
		{
			if (key == null) throw new ArgumentNullException("key");

			ReadOnlyBag<E> itemsUnderKey;

			if (map.TryGetValue(key, out itemsUnderKey))
			{
				count -= itemsUnderKey.Count;

				map.Remove(key);

				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion
	}
}
