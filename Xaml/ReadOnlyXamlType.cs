using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;
using System.Xaml.Schema;
using System.Reflection;

namespace Gramma.GenericContentModel.Xaml
{
	public class ReadOnlyXamlType : XamlType
	{
		#region Construction

		public ReadOnlyXamlType(Type underlyingType, XamlSchemaContext schemaContext)
			: base(underlyingType, schemaContext)
		{
		}

		public ReadOnlyXamlType(String typeName, IList<XamlType> typeArguments, XamlSchemaContext schemaContext)
			: base(typeName, typeArguments, schemaContext)
		{

		}

		#endregion

		#region Protected methods

		protected override bool LookupIsConstructible()
		{
			if (this.ConstructionRequiresArguments) return true;

			if (this.UnderlyingType.GetConstructor(
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
				null,
				Type.EmptyTypes,
				null) != null)
			{
				return true;
			}

			return false;
		}

		protected override XamlMember LookupMember(string name, bool skipReadOnlyCheck)
		{
			var propertyInfo = this.UnderlyingType.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			if (propertyInfo == null) return null;

			return new ReadOnlyXamlMember(propertyInfo, this.SchemaContext);
		}

		protected override IEnumerable<XamlMember> LookupAllMembers()
		{
			foreach (var propertyInfo in this.UnderlyingType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				yield return new ReadOnlyXamlMember(propertyInfo, this.SchemaContext);
			}
		}

		/* Uncomment these in order to enable adding elements to collections using their private/protected/internal method "AddItem" */

		//protected override XamlCollectionKind LookupCollectionKind()
		//{
		//  var kind = base.LookupCollectionKind();

		//  if (kind != XamlCollectionKind.None) return kind;

		//  var addItemMethodInfo = this.UnderlyingType.GetMethod("AddItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

		//  if (addItemMethodInfo != null)
		//  {
		//    if (addItemMethodInfo.GetParameters().Length == 1)
		//    {
		//      return XamlCollectionKind.Collection;
		//    }
		//  }

		//  return XamlCollectionKind.None;
		//}

		//protected override XamlTypeInvoker LookupInvoker()
		//{
		//  return new ReadOnlyXamlTypeInvoker(this);
		//}

		#endregion
	}
}
