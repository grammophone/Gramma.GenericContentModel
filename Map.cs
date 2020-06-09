using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IMap{K, E}"/>,
	/// a dictionary of items indexed by their <see cref="IKeyedElement{K}.Key"/>
	/// property.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	[Serializable]
	public class Map<K, E> : ReadOnlyMap<K, E>, IMap<K, E>, ICollection<E>
		where E : IKeyedElement<K>
	{
		#region Construction

		/// <summary>
		/// Create an empty map.
		/// </summary>
		public Map()
		{

		}

		/// <summary>
		/// Create a map containing the given <paramref name="items"/>.
		/// </summary>
		public Map(IEnumerable<E> items)
			: base(items)
		{

		}

		#endregion

		#region IMap<K,E> Members

		/// <summary>
		/// Add a item to the collection.
		/// </summary>
		/// <param name="item">The item to add.</param>
		/// <returns>Returns whether the item already exists in the collection.</returns>
		public bool Add(E item)
		{
			return AddItem(item);
		}

		/// <summary>
		/// Add a collection of items in te collection.
		/// </summary>
		/// <param name="items">The items to add.</param>
		public void AddAll(IEnumerable<E> items)
		{
			if (items == null) throw new ArgumentNullException("items");

			foreach (var item in items)
			{
				AddItem(item);
			}
		}

		/// <summary>
		/// Remove an item.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		/// <returns>Returns true if the item was a member of the collection and removed, else false.</returns>
		public bool Remove(E item)
		{
			if (item == null) throw new ArgumentNullException("item");

			return dictionary.Remove(item.Key);
		}

		/// <summary>
		/// Remove an item by key.
		/// </summary>
		/// <param name="key">The key of the item.</param>
		/// <returns>Returns if the item was found and removed, else false.</returns>
		public bool RemoveKey(K key)
		{
			return dictionary.Remove(key);
		}

		/// <summary>
		/// Clear all items in this collection.
		/// </summary>
		public void Clear()
		{
			dictionary.Clear();
		}

		#endregion

		#region ICollection<E> Members

		bool ICollection<E>.IsReadOnly
		{
			get { return false; }
		}

		#endregion
	}
}
