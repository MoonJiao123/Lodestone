using System;
using System.Collections;
using UnityEngine;

namespace Andtech {

	public static class CoroutineUtility {

		/// <summary>
		/// Delays the coroutine.
		/// </summary>
		/// <param name="routine">The procedure to perform.</param>
		/// <param name="delay">How many seconds to wait.</param>
		/// <returns>The aggregated routine.</returns>
		public static IEnumerator Delayed(this IEnumerator routine, float delay) {
			yield return Wait(delay);
			yield return routine;
		}

		/// <summary>
		/// Returns an empty routine that lasts <paramref name="delay"/> seconds.
		/// </summary>
		/// <param name="delay">The duration to wait.</param>
		/// <returns>The wait routine.</returns>
		public static IEnumerator Wait(float delay) {
			yield return new WaitForSeconds(delay);
		}

		/// <summary>
		/// Performs the callback before the routine.
		/// </summary>
		/// <param name="routine">The routine to perform.</param>
		/// <param name="callback">The callback to perform.</param>
		/// <returns>The aggregated routine.</returns>
		public static IEnumerator Prepend(this IEnumerator routine, Action callback) {
			callback();
			yield return routine;
		}

		/// <summary>
		/// Performs the callback after the routine.
		/// </summary>
		/// <param name="routine">The routine to perform.</param>
		/// <param name="callback">The callback to perform.</param>
		/// <returns>The aggregated routine.</returns>
		public static IEnumerator Append(this IEnumerator routine, Action callback) {
			yield return routine;
			callback();
		}

		/// <summary>
		/// Performs two routines sequentially.
		/// </summary>
		/// <param name="first">The first routine to perform.</param>
		/// <param name="second">The second routine to perform.</param>
		/// <returns>The aggregated routine.</returns>
		public static IEnumerator Concat(this IEnumerator first, IEnumerator second) {
			yield return first;
			yield return second;
		}

		/// <summary>
		/// Converts a coroutine to a routine.
		/// </summary>
		/// <param name="coroutine">The coroutine to convert.</param>
		/// <returns>The routine.</returns>
		public static IEnumerator ToEnumerator(this Coroutine coroutine) {
			yield return coroutine;
		}
	}
}
