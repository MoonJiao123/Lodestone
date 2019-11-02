#define NEGATIVE
#define ZERO
#define POSITIVE

using UnityEngine;

namespace Andtech {

	public struct AxisButton {
		public string axisName;
		public float threshold;

#if NEGATIVE
		/// <summary>
		/// Did the player press down in the negative direction?
		/// </summary>
		public bool DownNegative {
			get;
			private set;
		}
		/// <summary>
		/// Is player pressing in the negative direction?
		/// </summary>
		public bool Negative {
			get;
			private set;
		}
		/// <summary>
		/// Did the player release from the negative direction?
		/// </summary>
		public bool UpNegative {
			get;
			private set;
		}
#endif

#if ZERO
		/// <summary>
		/// Is the player not pressing?
		/// </summary>
		public bool Zero {
			get;
			private set;
		}
#endif

#if POSITIVE
		/// <summary>
		/// Did the player press down in the positive direction?
		/// </summary>
		public bool DownPositive {
			get;
			private set;
		}
		/// <summary>
		/// Is player pressing in the positive direction?
		/// </summary>
		public bool Positive {
			get;
			private set;
		}
		/// <summary>
		/// Did the player release from the positive direction?
		/// </summary>
		public bool UpPositive {
			get;
			private set;
		}
#endif

		public AxisButton(string axisName, float threshold) {
			this.axisName = axisName;
			this.threshold = threshold;
#if NEGATIVE
			DownNegative = false;
			Negative = false;
			UpNegative = false;
#endif

#if ZERO
			Zero = true;
#endif

#if POSITIVE
			DownPositive = false;
			Positive = false;
			UpPositive = false;
#endif
		}

		/// <summary>
		/// Manually update the state of the instance.
		/// </summary>
		public void Update() {			
			float value = Input.GetAxis(axisName);
#if NEGATIVE
			if (value <= -threshold) {
				// Press down in negative direction
				DownNegative = !Negative;
				UpNegative = false;
				Negative = true;

#if ZERO
				Zero = false;
#endif

#if POSITIVE
				DownPositive = false;
				UpPositive = Positive;
				Positive = false;
#endif
				return;
			}
#endif

#if POSITIVE
			if (value >= threshold) {
				// Press down in positive direction
#if NEGATIVE
				DownNegative = false;
				UpNegative = Negative;
				Negative = false;
#endif

#if ZERO
				Zero = false;
#endif

				DownPositive = !Positive;
				UpPositive = false;
				Positive = true;

				return;
			}
#endif

			// No press detected
#if NEGATIVE
			DownNegative = false;
			UpNegative = Negative;
			Negative = false;
#endif

#if ZERO
			Zero = true;
#endif

#if POSITIVE
			DownPositive = false;
			UpPositive = Positive;
			Positive = false;
#endif
		}
	}
}
