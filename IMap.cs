using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// A dictionary of items indexed by their <see cref="IKeyedElement{K}.Key"/>
	/// property.
	/// </summary>
	/// <typeparam name="K">The type of the keys.</typeparam>
	/// <typeparam name="E">The type of the items.</typeparam>
	public interface IMap<in K, E> : IReadOnlyMap<K, E>
		where E : IKeyedElement<K>
	{
		#region Methods

		/// <summary>
		/// Add an object to the collection of items.
		/// </summary>
		/// <param name="item">The object to add.</param>
		/// <returns>
		/// Returns true if the object was not already in the collection and added, else false.
		/// </returns>
		/// <remarks>
		/// Upon successful addition, the item's <see cref="IChild{P}.Parent"/> property
		/// is updated to point to the collection's <see cref="IReadOnlyChildren{P, C}.Owner"/>.
		/// </remarks>
		bool Add(E item);

		/// <summary>
		/// Add a collection of objects to the collection of items.
		/// </summary>
		/// <param name="items">The collection to add.</param>
		void AddAll(IEnumerable<E> items);

		/// <summary>
		/// Attempt to remove a item from the collection.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		/// <returns>Returns true if the item was found and removed, else false.</returns>
		bool Remove(E item);

		/// <summary>
		/// Attempt to remove a child specified by its key.
		/// </summary>
		/// <param name="key">The key of the child.</param>
		/// <returns>
		/// Returns true if a child was found having the specified
		/// key in the collection and removed, else false.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// The <paramref name="key"/> was null.
		/// </exception>
		bool RemoveKey(K key);

		/// <summary>
		/// Remove all items from the collection.
		/// </summary>
		void Clear();

		#endregion
	}
}
