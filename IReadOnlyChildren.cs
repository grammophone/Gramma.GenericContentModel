using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An immmutable collection of 'child' objects belonging to a 'parent'.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	public interface IReadOnlyChildren<out P, out C> : IReadOnlyBag<C>
		where C : class, IChild<P>
		where P : class
	{
		#region Properties

		/// <summary>
		/// The 'parent' who owns the collection.
		/// </summary>
		P Owner { get; }

		#endregion
	}
}
