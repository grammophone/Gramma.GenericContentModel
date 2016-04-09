using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// A read-only collection of bi-grams, indexed by both of their members.
	/// </summary>
	/// <typeparam name="F">The type of the first bi-gram member.</typeparam>
	/// <typeparam name="S">The type of the second bi-gram member.</typeparam>
	[Serializable]
	public class ReadOnlyBiGramSet<F, S> : ReadOnlyBag<Tuple<F, S>>, IReadOnlyBiGramSet<F, S>
	{
		#region Private fields

		[NonSerialized]
		private MultiDictionary<F, Tuple<F, S>, IReadOnlyBiGramSet<F, S>, ReadOnlyBiGramSet<F, S>> tuplesByFirst;

		[NonSerialized]
		private MultiDictionary<S, Tuple<F, S>, IReadOnlyBiGramSet<F, S>, ReadOnlyBiGramSet<F, S>> tuplesBySecond;

		#endregion

		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="tuples">The bi-grams to add to the collection.</param>
		public ReadOnlyBiGramSet(IEnumerable<Tuple<F, S>> tuples)
			: base(tuples)
		{
		}

		/// <summary>
		/// Create an empty bi-grams collection.
		/// </summary>
		public ReadOnlyBiGramSet()
		{
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Called when an item is added to the collection.
		/// </summary>
		/// <param name="element">The added element.</param>
		/// <returns>Returns true if the element was not already in the collection and added successfully, else false.</returns>
		internal protected override bool AddItem(Tuple<F, S> element)
		{
			if (element == null) throw new ArgumentNullException("element");

			if (element.Item1 == null) throw new ArgumentNullException("element", "The Item1 member of the element must not be null.");
			if (element.Item2 == null) throw new ArgumentNullException("element", "The Item2 member of the element must not be null.");

			if (base.AddItem(element))
			{
				if (tuplesByFirst != null) tuplesByFirst.Add(element.Item1, element);
				if (tuplesBySecond != null) tuplesBySecond.Add(element.Item2, element);

				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region IReadOnlyBiGrams<F,S> Members

		/// <summary>
		/// Checks whether an bi-gram exists in the collection.
		/// </summary>
		/// <param name="first">The first element of the bi-gram.</param>
		/// <param name="second">The second element of the bi-gram.</param>
		/// <returns>Returns true if the item is found in the collection.</returns>
		public bool Contains(F first, S second)
		{
			return Contains(new Tuple<F, S>(first, second));
		}

		/// <summary>
		/// Get the collection of bi-grams having the given first member.
		/// </summary>
		/// <param name="first">The item to be matched in the first member of the returned bi-grams.</param>
		/// <returns>Returns the collection of the bi-grams which have the given first memnber.</returns>
		public IReadOnlyBiGramSet<F, S> GetByFirst(F first)
		{
			if (tuplesByFirst == null)
			{
				var entriesByFirst = from tuple in this.collection
														 select new ReadOnlyKeyValuePair<F, Tuple<F, S>>(tuple.Item1, tuple);

				tuplesByFirst = new MultiDictionary<F, Tuple<F, S>, IReadOnlyBiGramSet<F, S>, ReadOnlyBiGramSet<F, S>>(entriesByFirst);
			}

			return tuplesByFirst[first];
		}

		/// <summary>
		/// Get the collection of bi-grams having the given second member.
		/// </summary>
		/// <param name="second">The item to be matched in the second member of the returned bi-grams.</param>
		/// <returns>Returns the collection of the bi-grams which have the given second memnber.</returns>
		public IReadOnlyBiGramSet<F, S> GetBySecond(S second)
		{
			if (tuplesBySecond == null)
			{
				var entriesBySecond = from tuple in this.collection
															select new ReadOnlyKeyValuePair<S, Tuple<F, S>>(tuple.Item2, tuple);

				tuplesBySecond = new MultiDictionary<S, Tuple<F, S>, IReadOnlyBiGramSet<F, S>, ReadOnlyBiGramSet<F, S>>(entriesBySecond);
			}

			return tuplesBySecond[second];
		}

		#endregion
	}

	/// <summary>
	/// A read-only collection of bi-grams, indexed by both of their members.
	/// </summary>
	/// <typeparam name="T">The type of both of the members of the bi-gram.</typeparam>
	[Serializable]
	public class ReadOnlyBiGramSet<T> : ReadOnlyBiGramSet<T, T>, IReadOnlyBiGramSet<T>
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="tuples">The bi-grams to add to the collection.</param>
		public ReadOnlyBiGramSet(IEnumerable<Tuple<T, T>> tuples)
			: base(tuples)
		{
		}

		/// <summary>
		/// Create an empty bi-grams collection.
		/// </summary>
		public ReadOnlyBiGramSet()
		{
		}

		#endregion
	}
}
