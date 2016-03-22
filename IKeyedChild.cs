using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// Represents a child which is indexed under a parent by its key
	/// property.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="K">The type of the key.</typeparam>
	public interface IKeyedChild<P, out K> : IChild<P>, IKeyedElement<K>
		where P : class
	{
	}
}
