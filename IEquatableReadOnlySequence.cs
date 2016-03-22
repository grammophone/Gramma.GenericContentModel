using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	public interface IEquatableReadOnlySequence<T> : IReadOnlySequence<T>, IEquatable<IEquatableRead
	{
	}
}
