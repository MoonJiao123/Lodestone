using UnityEngine;

namespace Andtech {

    /// <summary>
    /// Interpolates a collection of floats, using uniform interpolation parameters.
    /// </summary>
	public struct ListInterpolator {
		public float smoothTime;
        public float maxSpeed;
        public bool useAngularInterpolation;
		public bool Approximately {
			get {
				for (int i = 0; i < Length; i++) {
					if (Mathf.Abs(velocities[i]) > EPSILON)
						return false;
				}
				for (int i = 0; i < Length; i++) {
					if (Mathf.Abs(targets[i] - currents[i]) > EPSILON)
						return false;
				}

				return true;
			}
		}
		public float this[int index] {
			get {
				return currents[index];
			}
			set {
				targets[index] = value;
			}
		}
		public int Length {
			get {
				return currents.Length;
			}
        }

		public const float EPSILON = 0.001F;
        public static float defaultSmoothTime = 0.25F;

        private readonly float[] currents;
		private readonly float[] targets;
		private readonly float[] velocities;

        public ListInterpolator(int capacity) : this(capacity, defaultSmoothTime) { }

		public ListInterpolator(int capacity, float smoothTime) {
            currents = new float[capacity];
            targets = new float[capacity];
            velocities = new float[capacity];
            this.smoothTime = smoothTime;

            maxSpeed = Mathf.Infinity;
            useAngularInterpolation = false;
        }

        /// <summary>
        /// Advances the interpolator.
        /// </summary>
        public void Update() {
            Update(Time.deltaTime);
        }

        /// <summary>
        /// Advances the interpolator.
        /// </summary>
        /// <param name="deltaTime">The amount of time since the last call to <see cref="Update"/></param>
        public void Update(float deltaTime) {
            for (int i = 0; i < Length; i++) {
                if (useAngularInterpolation)
				    currents[i] = Mathf.SmoothDampAngle(currents[i], targets[i], ref velocities[i], smoothTime, maxSpeed, deltaTime);
                else
                    currents[i] = Mathf.SmoothDamp(currents[i], targets[i], ref velocities[i], smoothTime, maxSpeed, deltaTime);
			}
		}
	}
}
