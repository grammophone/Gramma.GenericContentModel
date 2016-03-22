using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel.DebuggerProxies
{
	/// <summary>
	/// Debugger proxy for <see cref="ReadOnlySequence{T}"/> descendants.
	/// </summary>
	/// <typeparam name="T">The type of items in the <see cref="ReadOnlySequence{T}"/> collection.</typeparam>
	internal class SequenceDebuggerProxy<T>
	{
		#region Private fields

		private ReadOnlySequence<T> sequence;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="sequence">The collection to provide debug information for.</param>
		public SequenceDebuggerProxy(ReadOnlySequence<T> sequence)
		{
			if (sequence == null) throw new ArgumentNullException("sequence");

			this.sequence = sequence;
		}

		#endregion

		#region Public properties
		
		/// <summary>
		/// The items in the collection.
		/// </summary>
		/// <remarks>
		/// This doesn't call <see cref="ReadOnlySequence{T}.ToArray"/> method as this holds a
		/// cached array. It accesses its internal list instead to provide a fresh array.
		/// Use your debugger by explicitly calling <see cref="ReadOnlySequence{T}.ToArray"/> method in the debug expression
		/// to track any descrepancies between the cached array and the actual collection contents.
		/// </remarks>
		public T[] Items
		{
			get
			{
				return sequence.list.ToArray();
			}
		}

		#endregion
	}
}
