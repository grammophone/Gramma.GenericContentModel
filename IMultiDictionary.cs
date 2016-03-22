using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// A dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	/// <typeparam name="C">The type of collection under a key. Must descend from <see cref="IReadOnlyBag{E}"/>.</typeparam>
	public interface IMultiDictionary<K, E, C> : IReadOnlyMultiDictionary<K, E, C>
		where C : IReadOnlyBag<E>
	{
		#region Methods

		/// <summary>
		/// Add an item associated with a key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="item">The item.</param>
		/// <returns>
		/// Returns true if addition was successful and 
		/// no equal item preexisted under the same key, else returns false.
		/// </returns>
		bool Add(K key, E item);

		/// <summary>
		/// Clear all items in the dictionary.
		/// </summary>
		void Clear();

		#endregion
	}

	/// <summary>
	/// A dictionary of items associated with keys, allowing for 
	/// multiple items having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	public interface IMultiDictionary<K, E> : IMultiDictionary<K, E, IReadOnlyBag<E>>
	{
	}
}
