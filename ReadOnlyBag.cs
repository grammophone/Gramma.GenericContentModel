using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IReadOnlyBag{E}"/>, 
	/// an immmutable collection of elements.
	/// </summary>
	/// <typeparam name="E">The type of the elements.</typeparam>
	[Serializable]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DebuggerProxies.BagDebuggerProxy<>))]
	public class ReadOnlyBag<E> : IReadOnlyBag<E>, ICollection<E>, IInitializable<E>
	{
		#region Protected fields

		/// <summary>
		/// The set supporting this collection.
		/// </summary>
		protected ISet<E> collection;

		#endregion

		#region Construction

		/// <summary>
		/// Create an empty collection.
		/// </summary>
		public ReadOnlyBag()
			: this(null)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="elements">The optional set of elements to be contained, else empty set if null.</param>
		public ReadOnlyBag(IEnumerable<E> elements)
		{
			this.collection = new HashSet<E>();

			if (elements != null)
			{
				foreach (var element in elements)
				{
					AddItem(element);
				}
			}
		}

		#endregion

		#region IReadOnlyBag<C> Members

		/// <summary>
		/// The number of items in the collection.
		/// </summary>
		public int Count
		{
			get { return this.collection.Count; }
		}

		#endregion

		#region IEnumerable<E> Members

		public IEnumerator<E> GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Called when an item is added to the collection.
		/// </summary>
		/// <param name="element">The added element.</param>
		/// <returns>Returns true if the element was not already in the collection and added successfully, else false.</returns>
		internal protected virtual bool AddItem(E element)
		{
			return collection.Add(element);
		}

		/// <summary>
		/// Called when an item is removed from the collection.
		/// </summary>
		/// <param name="element">The element to be removed.</param>
		/// <returns>Returns true if the lement was found and removed.</returns>
		internal protected virtual bool RemoveItem(E element)
		{
			return collection.Remove(element);
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

		/// <summary>
		/// Checks whether an item exists in the collection.
		/// </summary>
		/// <param name="item">The item to check.</param>
		/// <returns>Returns true if the item is found in the collection.</returns>
		public bool Contains(E item)
		{
			return collection.Contains(item);
		}

		/// <summary>
		/// Copy the elements of the collection to an array.
		/// </summary>
		/// <param name="array">The array to copy the elements to.</param>
		/// <param name="arrayIndex">The starting index of the copy.</param>
		public void CopyTo(E[] array, int arrayIndex)
		{
			if (array == null) throw new ArgumentNullException("array");
			if (arrayIndex < 0 || array.Length - arrayIndex < this.Count) throw new ArgumentException("arrayIndex is out of bounds.", "arrayIndex");

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

		#region IInitializable<E> Members

		bool IInitializable<E>.AddItem(E element)
		{
			return this.AddItem(element);
		}

		#endregion
	}
}
