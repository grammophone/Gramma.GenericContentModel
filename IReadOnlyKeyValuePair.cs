using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// A read-only key-value pair.
	/// </summary>
	/// <typeparam name="K">The type of the key.</typeparam>
	/// <typeparam name="V">The type of the value.</typeparam>
	public interface IReadOnlyKeyValuePair<out K, out V>
	{
		#region Properties

		/// <summary>
		/// The key.
		/// </summary>
		K Key { get; }

		/// <summary>
		/// The value.
		/// </summary>
		V Value { get; }

		#endregion
	}
}
