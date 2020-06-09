using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IKeyedReadOnlyChildren{P, C, K}"/>,
	/// an immutable collection of 'child' objects belonging to a 'parent',
	/// indexed by their key property and by position.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	/// <typeparam name="K">The type of the children's key.</typeparam>
	[Serializable]
	public class KeyedReadOnlyChildren<P, K, C> : ReadOnlyMap<K, C>, IKeyedReadOnlyChildren<P, K, C>, IOwnedObject<P>
		where C : class, IKeyedChild<P, K>
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
		public KeyedReadOnlyChildren()
			: this(null)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="owner">Optional owner of this collection, else null.</param>
		/// <param name="children">The optional set of children to be contained, else empty set if null.</param>
		public KeyedReadOnlyChildren(P owner, IEnumerable<C> children = null)
		{
			this.owner = owner;

			if (children != null)
			{
				foreach (var child in children)
				{
					AddItem(child);
				}
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

				foreach (var item in this.dictionary.Values)
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
		/// <returns>Returns true if the element's key was not already in the collection and added successfully, else false.</returns>
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
