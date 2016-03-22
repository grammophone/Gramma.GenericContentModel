using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IKeyedChildren{P, C, K}"/>,
	/// a mutable collection of 'child' objects belonging to a 'parent',
	/// indexed by their key property.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	/// <typeparam name="K">The type of the children's key.</typeparam>
	[Serializable]
	public class KeyedChildren<P, K, C> : KeyedReadOnlyChildren<P, K, C>, IKeyedChildren<P, K, C>, ICollection<C>
		where C : class, IKeyedChild<P, K>
		where P : class
	{
		#region Construction

		/// <summary>
		/// Create an empty collection with no owner.
		/// </summary>
		public KeyedChildren()
			: this(null)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="owner">Optional owner of this collection, else null.</param>
		/// <param name="children">The optional set of initial children to be contained, else empty set if null.</param>
		public KeyedChildren(P owner, IEnumerable<C> children = null)
			: base(owner, children)
		{
			
		}

		#endregion

		#region IKeyedChildren<P,C,K> Members

		public bool RemoveKey(K key)
		{
			C existingChild;

			if (this.dictionary.TryGetValue(key, out existingChild))
			{
				existingChild.Parent = null;
				return this.dictionary.Remove(key);
			}

			return false;
		}

		#endregion

		#region IChildren<P,C> Members

		public bool Add(C child)
		{
			return AddItem(child);
		}

		public void AddAll(IEnumerable<C> children)
		{
			if (children == null) throw new ArgumentNullException("children");

			foreach (var child in children)
			{
				this.AddItem(child);
			}
		}

		public bool Remove(C child)
		{
			if (child == null) throw new ArgumentNullException("child");

			C existingChild;

			if (this.dictionary.TryGetValue(child.Key, out existingChild))
			{
				if (child.Equals(existingChild))
				{
					child.Parent = default(P);
					return this.dictionary.Remove(child.Key);
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public void Clear()
		{
			foreach (var child in this)
			{
				child.Parent = null;
			}

			this.dictionary.Clear();
		}

		#endregion
	}
}
