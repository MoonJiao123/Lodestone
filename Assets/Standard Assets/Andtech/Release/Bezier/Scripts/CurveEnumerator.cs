using UnityEngine;

namespace Andtech.Bezier {

	public struct CurveEnumerator {
		public float Distance {
			get {
				return distance;
			}
		}

		private readonly Curve curve;
		private readonly float speedInitial;
		private readonly float speedFinal;
		private int index;
		private float distance;

		public CurveEnumerator(Curve curve, float speedInitial, float speedFinal, float counter = 0.0F) {
			this.curve = curve;
			this.speedInitial = speedInitial;
			this.speedFinal = speedFinal;
			index = 0;

			distance = counter;

			float speedAverage = (speedInitial + speedFinal) / 2.0F;
			float distanceTotal = curve.Length;
			float duration = distanceTotal / speedAverage;
		}

		public bool Step(out OrientedPoint orientedPoint) {
			float alpha = Distance / curve.Length;
			float speedLow = speedInitial + alpha * speedFinal;

			float increment = speedLow * Time.deltaTime;
			distance += increment;

			// Recompute index
			while (index + 1 < curve.Count) {
				if (Distance < curve.distances[index + 1])
					break;

				index++;
			}

			// Return appropriately
			if (index + 1 < curve.Count) {
				OrientedPoint a = curve[index];
				OrientedPoint b = curve[index + 1];
				float t = (Distance - curve.distances[index]) / curve.deltas[index + 1];

				// Compue the oriented point
				Vector3 position = Vector3.Lerp(a.position, b.position, t);
				Quaternion rotation = Quaternion.Slerp(a.rotation, b.rotation, t);
				orientedPoint = new OrientedPoint(position, rotation);

				return true;
			}

			orientedPoint = default;
			return false;
		}
	}
}
