using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Used to add initial elements to mutable and unmutable collections.
	/// </summary>
	/// <remarks>
	/// This interface is expected to be implemented explicitly, in order to preserve the
	/// concept of unmutable collections.
	/// </remarks>
	public interface IInitializable<E>
	{
		/// <summary>
		/// Add an element to the collection.
		/// </summary>
		/// <param name="element">The element to add.</param>
		/// <returns>Returns true if the element was added, false if not.</returns>
		bool AddItem(E element);
	}
}
