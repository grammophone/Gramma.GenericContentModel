using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
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

		public bool Add(E item)
		{
			return AddItem(item);
		}

		public void AddAll(IEnumerable<E> items)
		{
			if (items == null) throw new ArgumentNullException("items");

			foreach (var item in items)
			{
				AddItem(item);
			}
		}

		public bool Remove(E item)
		{
			if (item == null) throw new ArgumentNullException("item");

			return dictionary.Remove(item.Key);
		}

		public bool RemoveKey(K key)
		{
			return dictionary.Remove(key);
		}

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
