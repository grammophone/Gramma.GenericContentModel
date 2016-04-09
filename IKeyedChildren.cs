using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// A mutable collection of 'child' objects belonging to a 'parent',
	/// indexed by their key property.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	/// <typeparam name="K">The type of the children's key.</typeparam>
	public interface IKeyedChildren<P, in K, C> : IKeyedReadOnlyChildren<P, K, C>, IChildren<P, C>
		where C : class, IKeyedChild<P, K>
		where P : class
	{
		#region Methods

		/// <summary>
		/// Attempt to remove a child specified by its key.
		/// </summary>
		/// <param name="key">The key of the child.</param>
		/// <returns>
		/// Returns true if a child was found having the specified
		/// key in the collection and removed, else false.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// The <paramref name="key"/> was null.
		/// </exception>
		bool RemoveKey(K key);

		#endregion
	}
}
