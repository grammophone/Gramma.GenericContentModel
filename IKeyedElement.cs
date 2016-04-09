using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Represents an element containing a <see cref="IKeyedElement{K}.Key"/> property
	/// which can be used as an indexer when the element lies inside a collection.
	/// </summary>
	/// <typeparam name="K">the type of the key.</typeparam>
	public interface IKeyedElement<out K>
	{
		#region Properties

		/// <summary>
		/// The key of the element.
		/// </summary>
		K Key { get; }

		#endregion
	}
}
