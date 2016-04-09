using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Marks an instance belonging to an instance of type <typeparamref name="P"/>.
	/// </summary>
	/// <typeparam name="P">The type of the owner.</typeparam>
	public interface IOwnedObject<P>
	{
		#region Properties

		/// <summary>
		/// The owner of the instance.
		/// </summary>
		P Owner { get; set; }

		#endregion
	}
}
