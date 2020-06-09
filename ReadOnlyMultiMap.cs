using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IReadOnlyMultiMap{K, E}"/>,
	/// a dictionary of elements indexed by their <see cref="IKeyedElement{K}.Key"/>
	/// property, allowing for multiple elements having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the key.</typeparam>
	/// <typeparam name="E">The ype of the element.</typeparam>
	[Serializable]
	public class ReadOnlyMultiMap<K, E> : IReadOnlyMultiMap<K, E>, ICollection<E>, IInitializable<E>, 
		IDeserializationCallback
		where E : IKeyedElement<K>
	{
		#region Private fields

		/// <summary>
		/// Dictionary of collections of elements of type <typeparamref name="E"/>
		/// under keys of type <typeparamref name="K"/>.
		/// </summary>
		protected Dictionary<K, ReadOnlyBag<E>> map;

		/// <summary>
		/// Holds the cached number of elements.
		/// </summary>
		[NonSerialized]
		protected int count;

		private static readonly IReadOnlyBag<E> emptyCollection = new ReadOnlyBag<E>();

		#endregion

		#region Construction

		/// <summary>
		/// Create an empty collection.
		/// </summary>
		public ReadOnlyMultiMap()
			: this(null)
		{

		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="elements">The optional set of elements to be contained, else empty set if null.</param>
		public ReadOnlyMultiMap(IEnumerable<E> elements)
		{
			if (elements == null)
			{
				map = new Dictionary<K, ReadOnlyBag<E>>(0);

				return;
			}
			else
			{
				map = new Dictionary<K, ReadOnlyBag<E>>();
			}

			foreach (var element in elements)
			{
				AddItem(element);
			}
		}

		#endregion

		#region IReadOnlyMultiMap<K,E> Members

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
		public IReadOnlyBag<E> this[K key]
		{
			get
			{
				ReadOnlyBag<E> collectionUnderKey;

				if (map.TryGetValue(key, out collectionUnderKey))
					return collectionUnderKey;
				else
					return emptyCollection; 
			}
		}

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
		public bool ContainsKey(K key)
		{
			return map.ContainsKey(key);
		}

		#endregion

		#region IReadOnlyBag<E> Members

		/// <summary>
		/// The number of items in the collection.
		/// </summary>
		public int Count
		{
			get { return count; }
		}

		#endregion

		#region IEnumerable<E> Members

		public IEnumerator<E> GetEnumerator()
		{
			return this.EnumerateElements().GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.EnumerateElements().GetEnumerator();
		}

		#endregion

		#region Internal methods

		internal virtual bool AddItem(E element)
		{
			ReadOnlyBag<E> keyBag;

			if (!map.TryGetValue(element.Key, out keyBag))
			{
				keyBag = new ReadOnlyBag<E>();
				map.Add(element.Key, keyBag);
			}

			if (keyBag.AddItem(element))
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

		#region IDeserialziationCallback members

		void IDeserializationCallback.OnDeserialization(object sender)
		{
			// Count all items upon deserialization and set the cached count field, 
			// as it is not serialized.

			count = 0;

			foreach (var entry in map)
			{
				count += entry.Value.Count;
			}
		}

		#endregion

		#region ICollection<E> Members

		void ICollection<E>.Add(E item)
		{
			AddItem(item);
		}

		void ICollection<E>.Clear()
		{
			throw new NotImplementedException();
		}

		bool ICollection<E>.Contains(E item)
		{
			if (item == null) throw new ArgumentNullException("item");

			ReadOnlyBag<E> collectionUnderKey;

			if (map.TryGetValue(item.Key, out collectionUnderKey))
			{
				return collectionUnderKey.Contains(item);
			}

			return false;
		}

		void ICollection<E>.CopyTo(E[] array, int arrayIndex)
		{
			if (array == null) throw new ArgumentNullException("array");

			if (arrayIndex < 0 || arrayIndex + this.Count > array.Length)
				throw new ArgumentException("arrayIndex is out of bounds.", "arrayIndex");

			int currentIndex = arrayIndex;

			foreach (var item in this)
			{
				array[currentIndex++] = item;
			}
		}

		bool ICollection<E>.IsReadOnly
		{
			get { return true; }
		}

		bool ICollection<E>.Remove(E item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IInitializable<E> members

		bool IInitializable<E>.AddItem(E element)
		{
			return this.AddItem(element);
		}

		#endregion

		#region Private methods

		private IEnumerable<E> EnumerateElements()
		{
			foreach (var entry in this.map)
			{
				foreach (var element in entry.Value)
				{
					yield return element;
				}
			}
		}

		#endregion
	}
}
