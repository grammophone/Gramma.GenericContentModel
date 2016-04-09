using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Implements a read-only container of ordered items.
	/// </summary>
	/// <typeparam name="T">The type of items in the container.</typeparam>
	[Serializable]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DebuggerProxies.SequenceDebuggerProxy<>))]
	public class ReadOnlySequence<T> : IReadOnlySequence<T>, ICollection<T>, IInitializable<T>
	{
		#region Protected fields

		protected internal List<T> list;

		[NonSerialized]
		protected T[] cachedArray;

		#endregion

		#region Construction

		/// <summary>
		/// Create an empty sequence.
		/// </summary>
		public ReadOnlySequence()
		{
			list = new List<T>(0);
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="items">The collection of items to include in the container.</param>
		public ReadOnlySequence(IEnumerable<T> items)
		{
			if (items == null) throw new ArgumentNullException("items");

			list = new List<T>(items);
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="items">The array of items to include in the container.</param>
		public ReadOnlySequence(T[] items)
		{
			if (items == null) throw new ArgumentNullException("items");

			list = new List<T>(items.Length);

			list.AddRange(items);
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="items">The array of items to include in the container.</param>
		public ReadOnlySequence(ICollection<T> items)
		{
			if (items == null) throw new ArgumentNullException("items");

			list = new List<T>(items.Count);

			list.AddRange(items);
		}

		#endregion

		#region IReadOnlySequence<T> Members

		/// <summary>
		/// Get collection item by its index.
		/// </summary>
		/// <param name="index">The index of the item.</param>
		/// <returns>Returns the item at the index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// When <paramref name="index"/> is negative or 
		/// greater or equal to Count.
		/// </exception>
		public T this[int index]
		{
			get { return list[index]; }
		}

		/// <summary>
		/// Get the elements of the collection as an array.
		/// </summary>
		/// <returns>Returns the array.</returns>
		/// <remarks>
		/// The returned array is cached. Changing its items might affect other callers.
		/// </remarks>
		public T[] ToArray()
		{
			if (cachedArray == null) cachedArray = list.ToArray();

			return cachedArray;
		}

		#endregion

		#region IReadOnlyBag<T> Members

		public int Count
		{
			get { return list.Count; }
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		#region ICollection<T> Members

		void ICollection<T>.Add(T item)
		{
			cachedArray = null;

			list.Add(item);
		}

		void ICollection<T>.Clear()
		{
			throw new NotImplementedException();
		}

		bool ICollection<T>.Contains(T item)
		{
			return list.Contains(item);
		}

		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		bool ICollection<T>.IsReadOnly
		{
			get { return true; }
		}

		bool ICollection<T>.Remove(T item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IInitializable<T> Members

		bool IInitializable<T>.AddItem(T element)
		{
			cachedArray = null;

			list.Add(element);
			
			return true;
		}

		#endregion
	}
}
