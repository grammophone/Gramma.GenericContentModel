using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel.DebuggerProxies
{
	/// <summary>
	/// Used to to provide friendly debug information of <see cref="IReadOnlyBag{T}"/>
	/// descentants within Visual Studio.
	/// </summary>
	/// <typeparam name="T">The type of the items in <see cref="IReadOnlyBag{T}"/> collection.</typeparam>
	internal class BagDebuggerProxy<T>
	{
		#region Private fields

		private IReadOnlyBag<T> bag;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="bag">The collection to provide debug information for.</param>
		public BagDebuggerProxy(IReadOnlyBag<T> bag)
		{
			if (bag == null) throw new ArgumentNullException("bag");

			this.bag = bag;
		}

		#endregion

		#region Public properties

		///// <summary>
		///// Returns the <see cref="IReadOnlyBag{T}.Count"/> property.
		///// </summary>
		//public int Count
		//{
		//  get
		//  {
		//    return bag.Count;
		//  }
		//}

		/// <summary>
		/// Returns the items of the <see cref="IReadOnlyBag{T}"/>.
		/// </summary>
		public T[] Items
		{
			get
			{
				return bag.ToArray();
			}
		}

		#endregion
	}
}
