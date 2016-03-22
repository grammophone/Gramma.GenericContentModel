using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel.DebuggerProxies
{
	/// <summary>
	/// A read-only dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	/// <typeparam name="C">The type of collection under a key. Must descend from <see cref="IReadOnlyBag{E}"/>.</typeparam>
	internal class MultiDictionaryDebuggerProxy<K, E, C>
		where C : IReadOnlyBag<E>
	{
		#region Private fields

		private IReadOnlyMultiDictionary<K, E, C> dictionary;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="dictionary">The dictionary to provide debug information for.</param>
		public MultiDictionaryDebuggerProxy(IReadOnlyMultiDictionary<K, E, C> dictionary)
		{
			if (dictionary == null) throw new ArgumentNullException("dictionary");

			this.dictionary = dictionary;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The entries of the dictionary. The value of each entry is the collection of items under the same entry key.
		/// </summary>
		public IReadOnlyKeyValuePair<K, C>[] Entries
		{
			get
			{
				return dictionary.ToArray();
			}
		}

		#endregion
	}
}
