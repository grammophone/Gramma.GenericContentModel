using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// An immutable collection of 'child' objects belonging to a 'parent',
	/// indexed by their key property.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="C">The type of the children.</typeparam>
	/// <typeparam name="K">The type of the children's key.</typeparam>
	public interface IKeyedReadOnlyChildren<out P, in K, out C> : IReadOnlyChildren<P, C>, IReadOnlyMap<K, C>
		where C : class, IKeyedChild<P, K>
		where P : class
	{
	}
}
