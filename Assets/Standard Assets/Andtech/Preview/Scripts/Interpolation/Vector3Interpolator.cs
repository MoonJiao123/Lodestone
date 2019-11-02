using UnityEngine;

namespace Andtech {

    /// <summary>
    /// Interpolates a vector of floats.
    /// </summary>
	public struct Vector3Interpolator {
        public Vector3 current;
        public Vector3 target;
        public Vector3 velocity;
        public float smoothTime;
        public float maxSpeed;
        public bool useAngularInterpolation;
		public bool Approximately {
			get {
				for (int i = 0; i < 3; i++) {
					if (Mathf.Abs(velocity[i]) > EPSILON)
						return false;
				}
				for (int i = 0; i < 3; i++) {
					if (Mathf.Abs(target[i] - current[i]) > EPSILON)
						return false;
				}

				return true;
			}
		}

		public const float EPSILON = 0.001F;
		public static float defaultSmoothTime = 0.25F;

        public Vector3Interpolator(Vector3 value) : this(value, defaultSmoothTime) { }

        public Vector3Interpolator(Vector3 value, float smoothTime) : this(value, smoothTime, Vector3.zero) { }

        public Vector3Interpolator(Vector3 value, float smoothTime, Vector3 velocity) {
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
            if (useAngularInterpolation) {
                current.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, smoothTime, maxSpeed, deltaTime);
                current.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, smoothTime, maxSpeed, deltaTime);
                current.z = Mathf.SmoothDampAngle(current.z, target.z, ref velocity.z, smoothTime, maxSpeed, deltaTime);
            }
            else {
                current.x = Mathf.SmoothDamp(current.x, target.x, ref velocity.x, smoothTime, maxSpeed, deltaTime);
                current.y = Mathf.SmoothDamp(current.y, target.y, ref velocity.y, smoothTime, maxSpeed, deltaTime);
                current.z = Mathf.SmoothDamp(current.z, target.z, ref velocity.z, smoothTime, maxSpeed, deltaTime);
            }
        }

        /// <summary>
        /// Casts the interpolator to the type of the internal value.
        /// </summary>
        /// <param name="interpolator">The interpolator to cast.</param>
        public static implicit operator Vector3(Vector3Interpolator interpolator) {
            return interpolator.current;
        }
    }
}
