using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IReadOnlyMap{K, E}"/>,
	/// an immutable collection of elements
	/// indexed by their key property.
	/// </summary>
	/// <typeparam name="K">The type of the key.</typeparam>
	/// <typeparam name="E">The type of the element.</typeparam>
	[Serializable]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DebuggerProxies.MapDebuggerProxy<,>))]
	public class ReadOnlyMap<K, E> : IReadOnlyMap<K, E>, ICollection<E>, IInitializable<E>
		where E : IKeyedElement<K>
	{
		#region Protected fields

		/// <summary>
		/// The dictionary supporting this collection.
		/// </summary>
		protected Dictionary<K, E> dictionary;

		#endregion

		#region Construction

		/// <summary>
		/// Create an empty collection.
		/// </summary>
		public ReadOnlyMap()
			: this(null)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="elements">The optional set of elements to be contained, else empty set if null.</param>
		public ReadOnlyMap(IEnumerable<E> elements)
		{
			this.dictionary = new Dictionary<K, E>();

			if (elements != null)
			{
				foreach (var child in elements)
				{
					if (!this.dictionary.ContainsKey(child.Key))
					{
						this.dictionary.Add(child.Key, child);
					}
				}
			}

		}

		#endregion

		#region IReadOnlyMap<K,E> Members

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
		public E this[K key]
		{
			get
			{
				return this.dictionary[key];
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
			return this.dictionary.ContainsKey(key);
		}

		#endregion

		#region IReadOnlyBag<E> Members

		/// <summary>
		/// The number of elements in the collection.
		/// </summary>
		public int Count
		{
			get { return this.dictionary.Count; }
		}

		#endregion

		#region IEnumerable<C> Members

		public virtual IEnumerator<E> GetEnumerator()
		{
			return this.dictionary.Values.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.dictionary.Values.GetEnumerator();
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Called when an item is added to the collection.
		/// </summary>
		/// <param name="element">The added element.</param>
		/// <returns>Returns true if the element's key was not already in the collection and added successfully, else false.</returns>
		internal protected virtual bool AddItem(E element)
		{
			if (element == null) throw new ArgumentNullException("element");
			if (element.Key == null) throw new ArgumentException("The element must have a non-null Key property.");

			if (this.dictionary.ContainsKey(element.Key)) return false;

			this.dictionary.Add(element.Key, element);

			return true;
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

			return dictionary.ContainsKey(item.Key);
		}

		void ICollection<E>.CopyTo(E[] array, int arrayIndex)
		{
			if (array == null) throw new ArgumentNullException("array");

			dictionary.Values.CopyTo(array, arrayIndex);
		}

		int ICollection<E>.Count
		{
			get { return dictionary.Count; }
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

		#region IInitializable<E> Members

		bool IInitializable<E>.AddItem(E element)
		{
			return this.AddItem(element);
		}

		#endregion
	}
}
