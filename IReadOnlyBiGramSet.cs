using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramma.GenericContentModel
{
	/// <summary>
	/// Represents a read-only collection of bi-grams, indexed by both of their members.
	/// </summary>
	/// <typeparam name="F">The type of the first bi-gram member.</typeparam>
	/// <typeparam name="S">The type of the second bi-gram member.</typeparam>
	public interface IReadOnlyBiGramSet<F, S> : IReadOnlyBag<Tuple<F, S>>
	{
		/// <summary>
		/// Get the collection of bi-grams having the given first member.
		/// </summary>
		/// <param name="first">The item to be matched in the first member of the returned bi-grams.</param>
		/// <returns>Returns the collection of the bi-grams which have the given first memnber.</returns>
		IReadOnlyBiGramSet<F, S> GetByFirst(F first);

		/// <summary>
		/// Get the collection of bi-grams having the given second member.
		/// </summary>
		/// <param name="second">The item to be matched in the second member of the returned bi-grams.</param>
		/// <returns>Returns the collection of the bi-grams which have the given second memnber.</returns>
		IReadOnlyBiGramSet<F, S> GetBySecond(S second);

		/// <summary>
		/// Checks whether a bi-gram exists in the collection.
		/// </summary>
		/// <param name="item">The item to check.</param>
		/// <returns>Returns true if the item is found in the collection.</returns>
		bool Contains(Tuple<F, S> item);

		/// <summary>
		/// Checks whether an bi-gram exists in the collection.
		/// </summary>
		/// <param name="first">The first element of the bi-gram.</param>
		/// <param name="second">The second element of the bi-gram.</param>
		/// <returns>Returns true if the item is found in the collection.</returns>
		bool Contains(F first, S second);
	}

	/// <summary>
	/// Represents a read-only collection of bi-grams, indexed by both of their members.
	/// </summary>
	/// <typeparam name="T">The type of both of the members of the bi-gram.</typeparam>
	public interface IReadOnlyBiGramSet<T> : IReadOnlyBiGramSet<T, T>
	{

	}
}
