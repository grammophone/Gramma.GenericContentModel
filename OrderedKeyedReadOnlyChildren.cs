using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IOrderedKeyedReadOnlyChildren{P, C, K}"/>,
	/// an ordered immutable collection of 'child' objects belonging to a 'parent',
	/// indexed by their key property and by position.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	/// <typeparam name="K">The type of the children's key.</typeparam>
	[Serializable]
	public class OrderedKeyedReadOnlyChildren<P, K, C> : 
		KeyedReadOnlyChildren<P, K, C>, IOrderedKeyedReadOnlyChildren<P, K, C>
		where C : class, IKeyedChild<P, K>
		where P : class
	{
		#region protected fields

		/// <summary>
		/// Holds the sequence of items.
		/// </summary>
		protected List<C> items = new List<C>();

		/// <summary>
		/// Holds the cached result from the <see cref="ToArray"/> call.
		/// </summary>
		[NonSerialized]
		protected C[] cachedArray;

		#endregion

		#region Construction

		/// <summary>
		/// Create an empty collection with no owner.
		/// </summary>
		public OrderedKeyedReadOnlyChildren()
			: this(null)
		{

		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="owner">Optional owner of this collection, else null.</param>
		/// <param name="children">The optional set of children to be contained, else empty set if null.</param>
		public OrderedKeyedReadOnlyChildren(P owner, IEnumerable<C> children = null)
			: base(owner, children)
		{

		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Called when an item is added to the collection.
		/// </summary>
		/// <param name="element">The added element.</param>
		/// <returns>Returns true if the element's key was not already in the collection and added successfully, else false.</returns>
		protected internal override bool AddItem(C element)
		{
			if (base.AddItem(element))
			{
				items.Add(element);
				
				cachedArray = null;

				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region IOrderedKeyedReadOnlyChildren<P,K,C> Members

		/// <summary>
		/// Get an item by index.
		/// </summary>
		/// <param name="index">the zero-based index of the item.</param>
		/// <returns>Returns the item at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown when the <paramref name="index"/> is negative or 
		/// not less than Count.
		/// </exception>
		public C this[int index]
		{
			get { return items[index]; }
		}

		/// <summary>
		/// Get the items in the collection.
		/// </summary>
		/// <returns>Returns an array holding the items in the order they were given.</returns>
		/// <remarks>
		/// The returned array is cached. Changing its items might affect other callers.
		/// </remarks>
		public C[] ToArray()
		{
			if (cachedArray == null) cachedArray = items.ToArray();

			return cachedArray;
		}

		#endregion

		#region IEnumerable<C> Members

		public override IEnumerator<C> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		#endregion
	}
}
