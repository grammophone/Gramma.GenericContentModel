using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.GenericContentModel
{
	/// <summary>
	/// Extensions for building containers and collections.
	/// </summary>
	public static class ContainerExtensions
	{
		/// <summary>
		/// Create a <see cref="MultiDictionary{K, E}"/> from a collection of items of type <typeparamref name="E"/>.
		/// The multiple values are stored in instances of <see cref="ReadOnlyBag{E}"/>.
		/// </summary>
		/// <typeparam name="K">The type of the keys of the dictionary.</typeparam>
		/// <typeparam name="E">The type of elements in the source collection and the items stored in the dictionary.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to specify a dictionary key from an item of the collection.</param>
		/// <returns>
		/// Returns a <see cref="MultiDictionary{K, E}"/> whose multiple values are stored in
		/// instances of <see cref="ReadOnlyBag{E}"/>.
		/// </returns>
		public static MultiDictionary<K, E> ToMultiDictionary<K, E>(
			this IEnumerable<E> sourceCollection,
			Func<E, K> keyMapper)
		{
			return ToMultiDictionary(sourceCollection, keyMapper, e => e);
		}

		/// <summary>
		/// Create a <see cref="MultiDictionary{K, E}"/> from a collection of items of type <typeparamref name="E"/>.
		/// The multiple values are stored in instances of <see cref="ReadOnlyBag{E}"/>.
		/// </summary>
		/// <typeparam name="K">The type of the keys of the dictionary.</typeparam>
		/// <typeparam name="E">The type of elements stored in the dictionary.</typeparam>
		/// <typeparam name="I">The type of items in the source collection.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to specify a dictionary key from an item of the collection.</param>
		/// <param name="valueMapper">Function to specify a value from an item of the collection.</param>
		/// <returns>
		/// Returns a <see cref="MultiDictionary{K, E}"/> whose multiple values are stored in
		/// instances of <see cref="ReadOnlyBag{E}"/>.
		/// </returns>
		public static MultiDictionary<K, E> ToMultiDictionary<K, E, I>(
			this IEnumerable<I> sourceCollection,
			Func<I, K> keyMapper,
			Func<I, E> valueMapper)
		{
			return MultiDictionary<K, E>.Create(sourceCollection, keyMapper, valueMapper);
		}

		/// <summary>
		/// Create a <see cref="MultiDictionary{K, E, C, CI}"/> from a collection of items of type <typeparamref name="E"/>.
		/// The multiple values are stored in instances of <typeparamref name="C"/> implemented by <typeparamref name="CI"/>.
		/// </summary>
		/// <typeparam name="K">The type of the keys of the dictionary.</typeparam>
		/// <typeparam name="E">The type of elements in the source collection and the items stored in the dictionary.</typeparam>
		/// <typeparam name="C">The type of interface for the values collection at each key. Must be derived from <see cref="IReadOnlyBag{E}"/>.</typeparam>
		/// <typeparam name="CI">The type of implementation for the values collection at each key. Must implement <typeparamref name="C"/>.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to specify a dictionary key from an item of the collection.</param>
		/// <returns>
		/// Returns a <see cref="MultiDictionary{K, E, C, CI}"/> whose multiple values are stored in
		/// in instances of <typeparamref name="C"/> implemented by <typeparamref name="CI"/>.
		/// </returns>
		public static MultiDictionary<K, E, C, CI> ToMultiDictionary<K, E, C, CI>(
			this IEnumerable<E> sourceCollection,
			Func<E, K> keyMapper)
		where C : IReadOnlyBag<E>
		where CI : C, IInitializable<E>, new()
		{
			return ToMultiDictionary<K, E, E, C, CI>(sourceCollection, keyMapper, e => e);
		}

		/// <summary>
		/// Create a <see cref="MultiDictionary{K, E, C, CI}"/> from a collection of items of type <typeparamref name="E"/>.
		/// The multiple values are stored in instances of <typeparamref name="C"/> implemented by <typeparamref name="CI"/>.
		/// </summary>
		/// <typeparam name="K">The type of the keys of the dictionary.</typeparam>
		/// <typeparam name="E">The type of elements stored in the dictionary.</typeparam>
		/// <typeparam name="I">The type of items in the source collection.</typeparam>
		/// <typeparam name="C">The type of interface for the values collection at each key. Must be derived from <see cref="IReadOnlyBag{E}"/>.</typeparam>
		/// <typeparam name="CI">The type of implementation for the values collection at each key. Must implement <typeparamref name="C"/>.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to specify a dictionary key from an item of the collection.</param>
		/// <param name="valueMapper">Function to specify a value from an item of the collection.</param>
		/// <returns>
		/// Returns a <see cref="MultiDictionary{K, E, C, CI}"/> whose multiple values are stored in
		/// in instances of <typeparamref name="C"/> implemented by <typeparamref name="CI"/>.
		/// </returns>
		public static MultiDictionary<K, E, C, CI> ToMultiDictionary<K, E, I, C, CI>(
			this IEnumerable<I> sourceCollection,
			Func<I, K> keyMapper,
			Func<I, E> valueMapper)
		where C : IReadOnlyBag<E>
		where CI : C, IInitializable<E>, new()
		{
			return MultiDictionary<K, E, C, CI>.Create(sourceCollection, keyMapper, valueMapper);
		}

		/// <summary>
		/// Create a <see cref="ReadOnlyMultiDictionary{K, E}"/> from a collection of items of type <typeparamref name="E"/>.
		/// The multiple values are stored in instances of <see cref="ReadOnlyBag{E}"/>.
		/// </summary>
		/// <typeparam name="K">The type of the keys of the dictionary.</typeparam>
		/// <typeparam name="E">The type of elements in the source collection and the items stored in the dictionary.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to specify a dictionary key from an item of the collection.</param>
		/// <returns>
		/// Returns a <see cref="ReadOnlyMultiDictionary{K, E}"/> whose multiple values are stored in
		/// instances of <see cref="ReadOnlyBag{E}"/>.
		/// </returns>
		public static ReadOnlyMultiDictionary<K, E> ToReadOnlyMultiDictionary<K, E>(
			this IEnumerable<E> sourceCollection,
			Func<E, K> keyMapper)
		{
			return ToReadOnlyMultiDictionary(sourceCollection, keyMapper, e => e);
		}

		/// <summary>
		/// Create a <see cref="ReadOnlyMultiDictionary{K, E}"/> from a collection of items of type <typeparamref name="E"/>.
		/// The multiple values are stored in instances of <see cref="ReadOnlyBag{E}"/>.
		/// </summary>
		/// <typeparam name="K">The type of the keys of the dictionary.</typeparam>
		/// <typeparam name="E">The type of elements stored in the dictionary.</typeparam>
		/// <typeparam name="I">The type of items in the source collection.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to specify a dictionary key from an item of the collection.</param>
		/// <param name="valueMapper">Function to specify a value from an item of the collection.</param>
		/// <returns>
		/// Returns a <see cref="ReadOnlyMultiDictionary{K, E}"/> whose multiple values are stored in
		/// instances of <see cref="ReadOnlyBag{E}"/>.
		/// </returns>
		public static ReadOnlyMultiDictionary<K, E> ToReadOnlyMultiDictionary<K, E, I>(
			this IEnumerable<I> sourceCollection,
			Func<I, K> keyMapper,
			Func<I, E> valueMapper)
		{
			return ReadOnlyMultiDictionary<K, E>.Create(sourceCollection, keyMapper, valueMapper);
		}

		/// <summary>
		/// Create a <see cref="ReadOnlyMultiDictionary{K, E, C, CI}"/> from a collection of items of type <typeparamref name="E"/>.
		/// The multiple values are stored in instances of <typeparamref name="C"/> implemented by <typeparamref name="CI"/>.
		/// </summary>
		/// <typeparam name="K">The type of the keys of the dictionary.</typeparam>
		/// <typeparam name="E">The type of elements in the source collection and the items stored in the dictionary.</typeparam>
		/// <typeparam name="C">The type of interface for the values collection at each key. Must be derived from <see cref="IReadOnlyBag{E}"/>.</typeparam>
		/// <typeparam name="CI">The type of implementation for the values collection at each key. Must implement <typeparamref name="C"/>.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to specify a dictionary key from an item of the collection.</param>
		/// <returns>
		/// Returns a <see cref="ReadOnlyMultiDictionary{K, E, C, CI}"/> whose multiple values are stored in
		/// in instances of <typeparamref name="C"/> implemented by <typeparamref name="CI"/>.
		/// </returns>
		public static ReadOnlyMultiDictionary<K, E, C, CI> ToReadOnlyMultiDictionary<K, E, C, CI>(
			this IEnumerable<E> sourceCollection,
			Func<E, K> keyMapper)
		where C : IReadOnlyBag<E>
		where CI : C, IInitializable<E>, new()
		{
			return ToReadOnlyMultiDictionary<K, E, E, C, CI>(sourceCollection, keyMapper, e => e);
		}

		/// <summary>
		/// Create a <see cref="ReadOnlyMultiDictionary{K, E, C, CI}"/> from a collection of items of type <typeparamref name="E"/>.
		/// The multiple values are stored in instances of <typeparamref name="C"/> implemented by <typeparamref name="CI"/>.
		/// </summary>
		/// <typeparam name="K">The type of the keys of the dictionary.</typeparam>
		/// <typeparam name="E">The type of elements stored in the dictionary.</typeparam>
		/// <typeparam name="I">The type of items in the source collection.</typeparam>
		/// <typeparam name="C">The type of interface for the values collection at each key. Must be derived from <see cref="IReadOnlyBag{E}"/>.</typeparam>
		/// <typeparam name="CI">The type of implementation for the values collection at each key. Must implement <typeparamref name="C"/>.</typeparam>
		/// <param name="sourceCollection">The source collection.</param>
		/// <param name="keyMapper">Function to specify a dictionary key from an item of the collection.</param>
		/// <param name="valueMapper">Function to specify a value from an item of the collection.</param>
		/// <returns>
		/// Returns a <see cref="ReadOnlyMultiDictionary{K, E, C, CI}"/> whose multiple values are stored in
		/// in instances of <typeparamref name="C"/> implemented by <typeparamref name="CI"/>.
		/// </returns>
		public static ReadOnlyMultiDictionary<K, E, C, CI> ToReadOnlyMultiDictionary<K, E, I, C, CI>(
			this IEnumerable<I> sourceCollection,
			Func<I, K> keyMapper,
			Func<I, E> valueMapper)
		where C : IReadOnlyBag<E>
		where CI : C, IInitializable<E>, new()
		{
			return ReadOnlyMultiDictionary<K, E, C, CI>.Create(sourceCollection, keyMapper, valueMapper);
		}
	}
}
