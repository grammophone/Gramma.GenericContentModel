using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
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

		public K Key
		{
			get { return key; }
		}

		public V Value
		{
			get { return value; }
		}

		#endregion

		#region IEquatable<ReadOnlyKeyValuePair<K,V>> Members

		public bool Equals(ReadOnlyKeyValuePair<K, V> other)
		{
			if (other == null) return false;

			return this.Key.Equals(other.Key) && this.Value.Equals(other.Value);
		}

		#endregion

		#region Public methods

		public override bool Equals(object obj)
		{
			return this.Equals(obj as ReadOnlyKeyValuePair<K, V>);
		}

		public override int GetHashCode()
		{
			return this.Key.GetHashCode() + 23 * this.Value.GetHashCode();
		}

		#endregion
	}
}
