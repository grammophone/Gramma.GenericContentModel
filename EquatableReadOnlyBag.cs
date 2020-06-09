using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Implementation of a read-only collection supporting set equality test,
	/// suitable for hashing algorithms.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	[Serializable]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DebuggerProxies.BagDebuggerProxy<>))]
	public class EquatableReadOnlyBag<T> 
		: ReadOnlyBag<T>, IEquatableReadOnlyBag<T>, IDeserializationCallback
		where T : IEquatable<T>
	{
		#region Protected fields

		[NonSerialized]
		protected int hashCode;

		#endregion

		#region Construction

		/// <summary>
		/// Create empty.
		/// </summary>
		public EquatableReadOnlyBag()
			: this(null)
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="elements">
		/// The optional set of elements to be contained, else empty set if null.
		/// The elements themselves must not me null.
		/// </param>
		public EquatableReadOnlyBag(IEnumerable<T> elements)
			: base(elements)
		{
			CalculateHashCode();
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Returns true if the set of items is equal to
		/// the set in the <paramref name="other"/> collection.
		/// </summary>
		public bool Equals(IEquatableReadOnlyBag<T> other)
		{
			if (other == null) return false;

			if (this.GetHashCode() != other.GetHashCode()) return false;

			return this.collection.SetEquals(other);
		}

		/// <summary>
		/// Returns true if the <paramref name="obj"/> parameter 
		/// is an <see cref="IEquatableReadOnlyBag{T}"/> and
		/// its set of items is equal to
		/// the set in this collection.
		/// </summary>
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IEquatableReadOnlyBag<T>);
		}

		/// <summary>
		/// Calculates a hash code based on the set
		/// of items in this collection.
		/// </summary>
		public override int GetHashCode()
		{
			return hashCode;
		}

		#endregion

		#region IDeserializationCallback implementation

		void IDeserializationCallback.OnDeserialization(object sender)
		{
			CalculateHashCode();
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Recalculates a hash code based on the set of
		/// items in the collection.
		/// </summary>
		protected void CalculateHashCode()
		{
			hashCode = 0;

			foreach (var item in this)
			{
				hashCode += item.GetHashCode();
			}
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Called when an item is added to the collection. Only allow non-null elements to be added.
		/// </summary>
		/// <param name="element">The added element.</param>
		/// <returns>Returns true if the element was not already in the collection and added successfully, else false.</returns>
		protected internal override bool AddItem(T element)
		{
			if (element == null) throw new ArgumentNullException(nameof(element));

			return base.AddItem(element);
		}

		/// <summary>
		/// Called when an item is removed from the collection. Only allow non-null elements to be removed.
		/// </summary>
		/// <param name="element">The element to be removed.</param>
		/// <returns>Returns true if the lement was found and removed.</returns>
		protected internal override bool RemoveItem(T element)
		{
			if (element == null) throw new ArgumentNullException(nameof(element));

			return base.RemoveItem(element);
		}

		#endregion
	}
}
