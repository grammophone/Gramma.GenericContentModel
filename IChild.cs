using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Represents a 'child' object that has a 'parent'.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	public interface IChild<P>
		where P : class
	{
		#region Properties

		/// <summary>
		/// The object's parent.
		/// </summary>
		P Parent { get; set; }

		#endregion
	}
}
