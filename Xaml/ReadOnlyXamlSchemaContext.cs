using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;

namespace Gramma.GenericContentModel.Xaml
{
	public class ReadOnlyXamlSchemaContext : XamlSchemaContext
	{
		#region Protected methods

		public override XamlType GetXamlType(Type type)
		{
			return new ReadOnlyXamlType(type, this);
		}

		#endregion
	}
}
