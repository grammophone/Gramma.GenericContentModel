using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// A base implementation for <see cref="IKeyedChild{P, K}"/>.
	/// </summary>
	/// <typeparam name="P">The type of the parent.</typeparam>
	/// <typeparam name="K">The type of the child's key.</typeparam>
	[Serializable]
	public abstract class KeyedChild<P, K> : Child<P>, IKeyedChild<P, K>
		where P : class
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="key">The key of the child.</param>
		public KeyedChild(K key)
		{
			if (key == null) throw new ArgumentNullException("key");

			this.Key = key;
		}

		#endregion

		#region IKeyedChild<P,K> Members

		public K Key
		{
			get;
			protected set;
		}

		#endregion
	}
}
