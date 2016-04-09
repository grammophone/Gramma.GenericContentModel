using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// A dictionary of elements indexed by their <see cref="IKeyedElement{K}.Key"/>
	/// property, allowing for multiple elements having the same key.
	/// </summary>
	/// <typeparam name="K">The type of the key.</typeparam>
	/// <typeparam name="E">The type of the element.</typeparam>
	public interface IMultiMap<in K, E> : IReadOnlyMultiMap<K, E>, ICollection<E>
		where E : IKeyedElement<K>
	{
		#region Public methods

		/// <summary>
		/// Add a collection of items.
		/// </summary>
		/// <param name="items">The collection of items to add.</param>
		void AddAll(IEnumerable<E> items);

		/// <summary>
		/// Remove all items under a specified <paramref name="key"/>.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		/// Returns true if a non-empty collection was found under the <paramref name="key"/>.
		/// </returns>
		bool RemoveKey(K key);

		#endregion
	}
}
