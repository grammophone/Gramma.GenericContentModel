using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// Helper for building complex relationships between objects
	/// like many-to-many.
	/// </summary>
	public static class RelationshipBuilder
	{
		#region Public methods

		/// <summary>
		/// Takes a definition of a many-to-many relationship
		/// between objects of type <typeparamref name="A"/> and type <typeparamref name="B"/>
		/// expressed as a collection of tuples and
		/// executes for each object in the relationship an action taking as parameters
		/// the object itself and the collection of the associated objects of the other end.
		/// </summary>
		/// <typeparam name="A">The type of objects at the 'A' end of the relationship.</typeparam>
		/// <typeparam name="B">The type of objects at the 'B' end of the relationship.</typeparam>
		/// <param name="tuples">The relationship definition, expressed as a collection of tuples of 'A' and 'B' objects.</param>
		/// <param name="aSideAction">The action to invoke for each object of the 'A' side and its associated 'B' objects.</param>
		/// <param name="bSideAction">The action to invoke for each object of the 'B' side and its associated 'A' objects.</param>
		/// <exception cref="ArgumentNullException">
		/// When any of the input arguments is null.
		/// </exception>
		public static void ActManyToMany<A, B>(
			IEnumerable<Tuple<A, B>> tuples, 
			Action<A, IEnumerable<B>> aSideAction, 
			Action<B, IEnumerable<A>> bSideAction)
		{
			if (tuples == null) throw new ArgumentNullException("tuples");
			if (aSideAction == null) throw new ArgumentNullException("aSideAction");
			if (bSideAction == null) throw new ArgumentNullException("bSideAction");

			var aSideMap = new Dictionary<A, HashSet<B>>();
			var bSideMap = new Dictionary<B, HashSet<A>>();

			foreach (var tuple in tuples)
			{
				HashSet<B> bSet;

				if (!aSideMap.TryGetValue(tuple.Item1, out bSet))
				{
					bSet = new HashSet<B>();
					aSideMap[tuple.Item1] = bSet;
				}

				bSet.Add(tuple.Item2);

				HashSet<A> aSet;

				if (!bSideMap.TryGetValue(tuple.Item2, out aSet))
				{
					aSet = new HashSet<A>();
					bSideMap[tuple.Item2] = aSet;
				}

				aSet.Add(tuple.Item1);
			}

			foreach (var aEntry in aSideMap)
			{
				aSideAction(aEntry.Key, aEntry.Value);
			}

			foreach (var bEntry in bSideMap)
			{
				bSideAction(bEntry.Key, bEntry.Value);
			}
		}

		/// <summary>
		/// Takes a definition of a many-to-many relationship
		/// between objects of type <typeparamref name="A"/> and type <typeparamref name="B"/>
		/// expressed as a collection of tuples and
		/// executes for each object in the relationship an action taking as parameters
		/// the object itself and the collection of the associated objects of the other end.
		/// </summary>
		/// <typeparam name="A">The type of objects at the 'A' end of the relationship.</typeparam>
		/// <typeparam name="B">The type of objects at the 'B' end of the relationship.</typeparam>
		/// <param name="allAs">The collection of all objects at side 'A'.</param>
		/// <param name="allBs">The collections of all objects at side 'B'.</param>
		/// <param name="tuples">The relationship definition, expressed as a collection of tuples of 'A' and 'B' objects.</param>
		/// <param name="aSideAction">The action to invoke for each object of the 'A' side and its associated 'B' objects.</param>
		/// <param name="bSideAction">The action to invoke for each object of the 'B' side and its associated 'A' objects.</param>
		/// <exception cref="ArgumentNullException">
		/// When any of the input arguments is null.
		/// </exception>
		/// <remarks>
		/// When an object of <paramref name="allAs"/> or <paramref name="allBs"/> doesn't
		/// appear in any tuple, the corresponding <paramref name="aSideAction"/> or <paramref name="bSideAction"/>
		/// is called with an empty association collection.
		/// </remarks>
		public static void ActManyToMany<A, B>(
			IEnumerable<A> allAs,
			IEnumerable<B> allBs,
			IEnumerable<Tuple<A, B>> tuples,
			Action<A, IEnumerable<B>> aSideAction,
			Action<B, IEnumerable<A>> bSideAction)
		{
			if (tuples == null) throw new ArgumentNullException("tuples");
			if (aSideAction == null) throw new ArgumentNullException("aSideAction");
			if (bSideAction == null) throw new ArgumentNullException("bSideAction");

			var aSideMap = new Dictionary<A, HashSet<B>>();
			var bSideMap = new Dictionary<B, HashSet<A>>();

			foreach (var a in allAs)
			{
				if (!aSideMap.ContainsKey(a))
				{
					aSideMap[a] = new HashSet<B>();
				}
			}

			foreach (var b in allBs)
			{
				if (!bSideMap.ContainsKey(b))
				{
					bSideMap[b] = new HashSet<A>();
				}
			}

			foreach (var tuple in tuples)
			{
				HashSet<B> bSet;

				if (!aSideMap.TryGetValue(tuple.Item1, out bSet))
				{
					bSet = new HashSet<B>();
					aSideMap[tuple.Item1] = bSet;
				}

				bSet.Add(tuple.Item2);

				HashSet<A> aSet;

				if (!bSideMap.TryGetValue(tuple.Item2, out aSet))
				{
					aSet = new HashSet<A>();
					bSideMap[tuple.Item2] = aSet;
				}

				aSet.Add(tuple.Item1);
			}

			foreach (var aEntry in aSideMap)
			{
				aSideAction(aEntry.Key, aEntry.Value);
			}

			foreach (var bEntry in bSideMap)
			{
				bSideAction(bEntry.Key, bEntry.Value);
			}
		}

		#endregion
	}
}
