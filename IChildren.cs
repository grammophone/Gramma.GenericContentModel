using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// A mutable collection of 'child' objects belonging to a 'parent'.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	public interface IChildren<out P, C> : IReadOnlyChildren<P, C>
		where C : class, IChild<P>
		where P : class
	{
		#region Methods

		/// <summary>
		/// Add an object to the collection of children.
		/// </summary>
		/// <param name="child">The object to add.</param>
		/// <returns>
		/// Returns true if the object was not already in the collection and added, else false.
		/// </returns>
		/// <remarks>
		/// Upon successful addition, the child's <see cref="IChild{P}.Parent"/> property
		/// is updated to point to the collection's <see cref="IReadOnlyChildren{P, C}.Owner"/>.
		/// </remarks>
		bool Add(C child);

		/// <summary>
		/// Add a collection of objects to the collection of children.
		/// </summary>
		/// <param name="children"></param>
		/// <remarks>
		/// Upon successful addition, the child's <see cref="IChild{P}.Parent"/> property
		/// is updated to point to the collection's <see cref="IReadOnlyChildren{P, C}.Owner"/>.
		/// </remarks>
		void AddAll(IEnumerable<C> children);

		/// <summary>
		/// Attempt to remove a child from the collection.
		/// </summary>
		/// <param name="child">The child to remove.</param>
		/// <returns>Returns true if the child was found and removed, else false.</returns>
		/// <remarks>
		/// Upon successful deletion, the child's <see cref="IChild{P}.Parent"/> property
		/// is updated to null.
		/// </remarks>
		bool Remove(C child);

		/// <summary>
		/// Remove all children from the collection.
		/// </summary>
		/// <remarks>
		/// Upon successful deletion, the child's <see cref="IChild{P}.Parent"/> property
		/// is updated to null.
		/// </remarks>
		void Clear();

		#endregion
	}
}
