using System.Collections.Generic;
using UnityEngine;

namespace Andtech {

	/// <summary>
	/// Alpha curve functions which can be used in animations.
	/// </summary>
	public static class AlphaCurve {

		/// <summary>
		/// Returns a progress <see cref="IEnumerable{T}"/> per frame.
		/// </summary>
		/// <param name="duration">The total duration of the curve.</param>
		/// <returns>The enumerator of alpha values per frame.</returns>
		public static IEnumerable<float> Linear(float duration) {
			for (float t = 0.0F; t < duration; t += Time.deltaTime) {
				float alpha = t / duration;

				yield return alpha;
			}

			yield return 1.0F;
		}

		/// <summary>
		/// Returns a progress <see cref="IEnumerable{T}"/> per frame.
		/// </summary>
		/// <param name="duration">The total duration of the curve.</param>
		/// <param name="p">The exponent to use in the expression.</param>
		/// <returns>The enumerator of alpha values per frame.</returns>
		public static IEnumerable<float> Power(float duration, float p) {
			for (float t = 0.0F; t < duration; t += Time.deltaTime) {
				float alpha = t / duration;

				yield return Mathf.Pow(alpha, p);
			}

			yield return 1.0F;
		}

		/// <summary>
		/// Returns a progress <see cref="IEnumerable{T}"/> per frame.
		/// </summary>
		/// <param name="duration">The total duration of the curve.</param>
		/// <param name="p">The exponent to use in the expression.</param>
		/// <returns>The enumerator of alpha values per frame.</returns>
		public static IEnumerable<float> PowerInverse(float duration, float p) {
			for (float t = 0.0F; t < duration; t += Time.deltaTime) {
				float alpha = t / duration;


				yield return 1.0F - Mathf.Pow(1.0F - alpha, p);
			}

			yield return 1.0F;
		}

		/// <summary>
		/// Returns a progress <see cref="IEnumerable{T}"/> per frame by using a sigmoid function.
		/// </summary>
		/// <param name="duration">The total duration of the curve.</param>
		/// <returns>The enumerator of alpha values per frame.</returns>
		public static IEnumerable<float> Sigmoid(float duration) {
			for (float t = 0.0F; t < duration; t += Time.deltaTime) {
				float alpha = t / duration;
				float x = alpha * 12.0F - 6.0F;

				float beta = 1.0F / (1.0F + Mathf.Exp(-x));

				yield return beta;
			}

			yield return 1.0F;
		}
	}
}
