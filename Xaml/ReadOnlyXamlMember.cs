using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;
using System.Xaml.Schema;
using System.Reflection;

namespace Grammophone.GenericContentModel.Xaml
{
	public class ReadOnlyXamlMember : XamlMember
	{
		#region Construction

		public ReadOnlyXamlMember(PropertyInfo propertyInfo, XamlSchemaContext schemaContext)
			: base(propertyInfo, schemaContext)
		{

		}

		#endregion

		#region Protected methods

		protected override bool LookupIsWritePublic()
		{
			var propertyInfo = (PropertyInfo)this.UnderlyingMember;

			if (!propertyInfo.CanWrite) return false;

			return true;
		}

		protected override bool LookupIsReadOnly()
		{
			var propertyInfo = (PropertyInfo)this.UnderlyingMember;

			if (!propertyInfo.CanWrite) return true;

			return false;
		}

		protected override bool LookupIsReadPublic()
		{
			var propertyInfo = (PropertyInfo)this.UnderlyingMember;

			if (!propertyInfo.CanRead || !propertyInfo.GetGetMethod().IsPublic) return false;

			return true;
		}

		#endregion
	}
}
