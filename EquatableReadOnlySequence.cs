using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Implements a read-only container of ordered items,
	/// which is capable for equality comparison to another container.
	/// It is appropriate as a dictionary key or a set member.
	/// </summary>
	/// <typeparam name="T">The type of items in the container.</typeparam>
	[Serializable]
	public class EquatableReadOnlySequence<T> : 
		ReadOnlySequence<T>, IEquatable<EquatableReadOnlySequence<T>>, IDeserializationCallback
		where T : IEquatable<T>
	{
		#region Protected fields

		[NonSerialized]
		protected int hashCode;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="items">The collection of items to include in the container.</param>
		public EquatableReadOnlySequence(IEnumerable<T> items)
			: base(items)
		{
			CalculateHashCode();
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="items">The array of items to include in the container.</param>
		public EquatableReadOnlySequence(T[] items)
			: base(items)
		{
			CalculateHashCode();
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="items">The collection of items to include in the container.</param>
		public EquatableReadOnlySequence(ICollection<T> items)
			: base(items)
		{
			CalculateHashCode();
		}

		#endregion

		#region IEquatable<EquatableReadOnlySequence<T>> Members

		public bool Equals(EquatableReadOnlySequence<T> other)
		{
			if (other == null) return false;

			if (hashCode != other.hashCode) return false;

			if (list.Count != other.list.Count) return false;

			for (int i = 0; i < list.Count; i++)
			{
				if (!list[i].Equals(other.list[i])) return false;
			}

			return true;
		}

		#endregion

		#region Public methods

		public override int GetHashCode()
		{
			return hashCode;
		}

		public override bool Equals(object other)
		{
			return this.Equals(other as EquatableReadOnlySequence<T>);
		}

		#endregion

		#region Protected methods

		protected void CalculateHashCode()
		{
			hashCode = 17;

			int hashGeneratingItemsCount = Math.Min(list.Count, 6);

			for (int i = 0; i < hashGeneratingItemsCount; i++)
			{
				hashCode = hashCode * 23;

				T item = list[i];

				if (item != null) hashCode += item.GetHashCode();
			}
		}

		#endregion

		#region IDeserializationCallback Members

		void IDeserializationCallback.OnDeserialization(object sender)
		{
			CalculateHashCode();
		}

		#endregion
	}
}
