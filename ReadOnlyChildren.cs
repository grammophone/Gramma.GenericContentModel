using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IReadOnlyChildren{P, C}"/>, 
	/// an immmutable collection of 'child' objects belonging to a 'parent'.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	[Serializable]
	public class ReadOnlyChildren<P, C> : ReadOnlyBag<C>, IReadOnlyChildren<P, C>, IOwnedObject<P>
		where C : class, IChild<P>
		where P : class
	{
		#region Protected fields

		/// <summary>
		/// The parent owning this collection.
		/// </summary>
		protected P owner;

		#endregion

		#region Construction

		/// <summary>
		/// Create an empty collection with no owner.
		/// </summary>
		public ReadOnlyChildren()
			: this(null)
		{

		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="owner">Optional owner of this collection, else null.</param>
		/// <param name="children">The optional set of children to be contained, else empty set if null.</param>
		public ReadOnlyChildren(P owner, IEnumerable<C> children = null)
			: base(children)
		{
			this.owner = owner;

			foreach (var child in this)
			{
				child.Parent = owner;
			}

		}

		#endregion

		#region IReadOnlyChildren<P,C> Members

		/// <summary>
		/// The owner of the collection of children.
		/// </summary>
		public P Owner
		{
			get { return this.owner; }
		}

		#endregion

		#region IOwnedObject<P> Members

		P IOwnedObject<P>.Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;

				foreach (var item in this.collection)
				{
					item.Parent = value;
				}
			}
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Called when an item is added to the collection.
		/// </summary>
		/// <param name="element">The added element.</param>
		/// <returns>Returns true if the element was not already in the collection and added successfully, else false.</returns>
		internal protected override bool AddItem(C element)
		{
			if (element == null) throw new ArgumentNullException("element");

			if (!base.AddItem(element)) return false;

			element.Parent = this.Owner;

			return true;
		}

		#endregion
	}
}
