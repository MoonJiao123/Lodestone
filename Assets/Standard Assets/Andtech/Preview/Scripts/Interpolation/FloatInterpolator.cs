using UnityEngine;

namespace Andtech {

    /// <summary>
    /// Interpolates a single float.
    /// </summary>
	public struct FloatInterpolator {
		public float current;
		public float target;
		public float velocity;
		public float smoothTime;
        public float maxSpeed;
        public bool useAngularInterpolation;
		public bool Approximately {
			get {
				if (Mathf.Abs(velocity) > EPSILON)
					return false;

				if (Mathf.Abs(target - current) > EPSILON)
					return false;

				return true;
			}
		}

		public const float EPSILON = 0.001F;
		public static float defaultSmoothTime = 0.25F;

		public FloatInterpolator(float value) : this(value, defaultSmoothTime) { }

		public FloatInterpolator(float value, float smoothTime) : this(value, smoothTime, 0.0F) { }

        public FloatInterpolator(float value, float smoothTime, float velocity) {
            current = value;
            this.velocity = velocity;
            this.smoothTime = smoothTime;

            target = value;
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
            if (useAngularInterpolation)
                current = Mathf.SmoothDampAngle(current, target, ref velocity, smoothTime, maxSpeed, deltaTime);
            else
                current = Mathf.SmoothDamp(current, target, ref velocity, smoothTime, maxSpeed, deltaTime);
		}

        /// <summary>
        /// Casts the interpolator to the type of the internal value.
        /// </summary>
        /// <param name="interpolator">The interpolator to cast.</param>
		public static implicit operator float(FloatInterpolator interpolator) {
			return interpolator.current;
		}
	}
}
