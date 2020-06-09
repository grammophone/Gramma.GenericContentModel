using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// An implementation of <see cref="IReadOnlyKeyValuePair{K, V}"/>.
	/// </summary>
	/// <typeparam name="K">The type of the key.</typeparam>
	/// <typeparam name="V">the type of the value.</typeparam>
	[Serializable]
	[DebuggerDisplay("Key = {Key}, Value = {Value}")]
	public class ReadOnlyKeyValuePair<K, V> : IReadOnlyKeyValuePair<K, V>, IEquatable<ReadOnlyKeyValuePair<K, V>>
	{
		#region Private fields

		private K key;

		private V value;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		public ReadOnlyKeyValuePair(K key, V value)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (value == null) throw new ArgumentNullException("value");

			this.key = key;
			this.value = value;
		}

		#endregion

		#region IReadOnlyKeyValuePair<K,V> Members

		/// <summary>
		/// The key of the entry.
		/// </summary>
		public K Key
		{
			get { return key; }
		}

		/// <summary>
		/// The value of the entry.
		/// </summary>
		public V Value
		{
			get { return value; }
		}

		#endregion

		#region IEquatable<ReadOnlyKeyValuePair<K,V>> Members

		/// <summary>
		/// Returns true if the <see cref="Key"/> and the <see cref="Value"/>
		/// to the respective key and value of an <paramref name="other"/> entry.
		/// </summary>
		/// <param name="other">The other entry.</param>
		public bool Equals(ReadOnlyKeyValuePair<K, V> other)
		{
			if (other == null) return false;

			return this.Key.Equals(other.Key) && this.Value.Equals(other.Value);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Returns true if the <paramref name="otherObject"/> is a <see cref="ReadOnlyKeyValuePair{K, V}"/>
		/// and the <see cref="Key"/> and the <see cref="Value"/> are equal to the
		/// to the respective key and value of an other entry.
		/// </summary>
		public override bool Equals(object otherObject)
		{
			return this.Equals(otherObject as ReadOnlyKeyValuePair<K, V>);
		}

		/// <summary>
		/// Calculate a hash code based on the <see cref="Key"/> and the <see cref="Value"/> properties.
		/// </summary>
		public override int GetHashCode()
		{
			return this.Key.GetHashCode() + 23 * this.Value.GetHashCode();
		}

		#endregion
	}
}
