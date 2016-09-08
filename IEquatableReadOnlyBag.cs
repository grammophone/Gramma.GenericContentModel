using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Represents a read-only collection supporting set equality test.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	public interface IEquatableReadOnlyBag<T>
		: IReadOnlyBag<T>, IEquatable<IEquatableReadOnlyBag<T>>
		where T : IEquatable<T>
	{
	}
}
