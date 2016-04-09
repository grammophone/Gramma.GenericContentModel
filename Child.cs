using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// A base implementation for <see cref="IChild{P}"/>.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	[Serializable]
	public abstract class Child<P> : IChild<P>
		where P : class
	{
		#region IChild<P> Members

		public P Parent
		{
			get;
			set;
		}

		#endregion
	}
}
