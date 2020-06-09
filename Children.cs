using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IChildren{P, C}"/>,
	/// a mutable collection of 'child' objects belonging to a 'parent'.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	[Serializable]
	public class Children<P, C> : ReadOnlyChildren<P, C>, IChildren<P, C>, ICollection<C>
		where C : class, IChild<P>
		where P : class
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="owner">Optional owner of this collection, else null.</param>
		/// <param name="children">The optional set of initial children to be contained, else empty set if null.</param>
		public Children(P owner = null, IEnumerable<C> children = null)
			: base(owner, children)
		{

		}

		#endregion

		#region IChildren<P,C> Members

		/// <summary>
		/// Add a child item to the collection.
		/// </summary>
		/// <param name="child">The child item to add.</param>
		/// <returns>Returns whether the item already exists in the collection.</returns>
		public bool Add(C child)
		{
			return AddItem(child);
		}

		/// <summary>
		/// Add a collection of child items in te collection.
		/// </summary>
		/// <param name="children">The child items to add.</param>
		public void AddAll(IEnumerable<C> children)
		{
			if (children == null) throw new ArgumentNullException("children");

			foreach (var child in children)
			{
				this.AddItem(child);
			}
		}

		/// <summary>
		/// Remove a child item.
		/// </summary>
		/// <param name="child">The child item to remove.</param>
		/// <returns>Returns true if the child item was a member of the collection and removed, else false.</returns>
		public bool Remove(C child)
		{
			if (this.collection.Contains(child))
			{
				this.collection.Remove(child);
				child.Parent = null;
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Clear all child items in this collection.
		/// </summary>
		public void Clear()
		{
			foreach (var child in this.collection)
			{
				child.Parent = null;
			}

			this.collection.Clear();
		}

		#endregion

		#region ICollection<C> Members

		bool ICollection<C>.Remove(C item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ICollection<C> Members

		bool ICollection<C>.IsReadOnly
		{
			get { return false; }
		}

		#endregion
	}
}
