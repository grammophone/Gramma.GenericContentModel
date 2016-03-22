using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel.DebuggerProxies
{
	/// <summary>
	/// Used to to provide friendly debug information of <see cref="IReadOnlyMap{K, E}"/>
	/// descentants within Visual Studio.
	/// </summary>
	/// <typeparam name="K">The type of the key.</typeparam>
	/// <typeparam name="E">The type of the element.</typeparam>
	internal class MapDebuggerProxy<K, E>
		where E : IKeyedElement<K>
	{
		#region Private fields

		private IReadOnlyMap<K, E> map;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="map">The collection to provide debug information for.</param>
		public MapDebuggerProxy(IReadOnlyMap<K, E> map)
		{
			if (map == null) throw new ArgumentNullException("map");

			this.map = map;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Returns the items of the <see cref="IReadOnlyMap{K, E}"/>.
		/// </summary>
		public E[] Items
		{
			get
			{
				return map.ToArray();
			}
		}

		#endregion

	}
}
