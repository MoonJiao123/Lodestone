using Andtech.Extensions;
using System;
using UnityEngine;

namespace Andtech {

	/// <summary>
	/// Represents a discrete rotation around a single axis.
	/// </summary>
	/// <remarks>Uses LHR rotations.</remarks>
	[Serializable]
	public struct Rotation {
		/// <summary>
		/// The (clockwise) quadrant that the rotation lies in. (Read Only)
		/// </summary>
		/// <remarks>Quadrant values are on the interval [0, 4).</remarks>
		public int Quadrant => 4 * turns / MAX;
		/// <summary>
		/// Returns the actual angle (in degrees) represented by the rotation.
		/// </summary>
		public float EulerAngle => TURNS2DEG * Turns;

		/// <summary>
		/// Number of clockwise 90 degree turns (internal).
		/// </summary>
		[SerializeField]
		private int turns;
		/// <summary>
		/// The number of rotation increments.
		/// </summary>
		public int Turns {
			get => turns;
			set => turns = value.Repeat(0, MAX - 1);
		}

		/// <summary>
		/// The maximum number of rotation fixtures.
		/// </summary>
		public const int MAX = 8;
		/// <summary>
		/// The number of turns per 1/2 revolution.
		/// </summary>
		public const int ONEHALFREV = MAX / 2;
		/// <summary>
		/// The number of turns per 1/4 revolution.
		/// </summary>
		public const int ONEFOURTHREV = MAX / 4;
		/// <summary>
		/// Turns-to-degrees conversion constant (Read Only).
		/// </summary>
		public const float TURNS2DEG = 360.0F / MAX;
		/// <summary>
		/// Turns-to-radians conversion constant (Read Only).
		/// </summary>
		public const float TURNS2RAD = 2.0F * Mathf.PI / MAX;

		public static readonly Rotation Up = new Rotation(ONEFOURTHREV * 0);
		public static readonly Rotation Right = new Rotation(ONEFOURTHREV * 1);
		public static readonly Rotation Down = new Rotation(ONEFOURTHREV * 2);
		public static readonly Rotation Left = new Rotation(ONEFOURTHREV * 3);

		/// <summary>
		/// Constructs a rotation with <paramref name="turns"/> clockwise 90 degree turns.
		/// </summary>
		/// <param name="turns"></param>
		public Rotation(int turns) {
			this.turns = turns.Repeat(0, MAX - 1);
		}

		#region OVERRIDE
		/// <summary>
		/// Determines whether the object is equal to this rotation.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns>The object is equal to this rotation.</returns>
		public override bool Equals(object obj) {
			if (!base.Equals(obj))
				return false;

			return ((Rotation)obj).Turns == Turns;
		}

		public override int GetHashCode() {
			return Turns.GetHashCode();
		}

		public override string ToString() {
			return string.Format("{0} ({1} / {2})", EulerAngle, Turns, MAX);
		}
		#endregion OVERRIDE

		#region OPERATOR
		/// <summary>
		/// Adds the two rotations together.
		/// </summary>
		/// <param name="rotationA">The first rotation.</param>
		/// <param name="rotationB">The second rotation.</param>
		/// <returns>The resultant rotation.</returns>
		public static Rotation operator +(Rotation rotationA, Rotation rotationB) {
			return new Rotation(rotationA.turns + rotationB.turns);
		}

		/// <summary>
		/// Adds <paramref name="turns"/> to the rotation.
		/// </summary>
		/// <param name="rotation">The rotation to add to.</param>
		/// <param name="turns">The number of clockwise turns to add./</param>
		/// <returns>The resultant rotation.</returns>
		public static Rotation operator +(Rotation rotation, int turns) {
			return new Rotation(rotation.Turns + turns);
		}

		/// <summary>
		/// Adds <paramref name="turns"/> to the rotation.
		/// </summary>
		/// <param name="turns">The number of clockwise turns to add./</param>
		/// <param name="rotation">The rotation to add to.</param>
		/// <returns>The resultant rotation.</returns>
		public static Rotation operator +(int turns, Rotation rotation) {
			return rotation + turns;
		}

		/// <summary>
		/// Subtracts <paramref name="turns"/> from the rotation.
		/// </summary>
		/// <param name="rotation">The rotation to subtract from.</param>
		/// <param name="turns">The number of clockwise turns to subtract./</param>
		/// <returns>The resultant rotation.</returns>
		public static Rotation operator -(Rotation rotation, int turns) {
			return rotation + -turns;
		}

		/// <summary>
		/// Reverses the direction of the rotation.
		/// </summary>
		/// <param name="rotation">The rotation to reverse.</param>
		/// <returns>The reversed rotation.</returns>
		public static Rotation operator ~(Rotation rotation) {
			return rotation + (MAX >> 1);
		}

		/// <summary>
		/// Determines whether two rotation are equal.
		/// </summary>
		/// <param name="rotationA">The first rotation.</param>
		/// <param name="rotationB">The second rotation.</param>
		/// <returns>The two rotations are equal.</returns>
		public static bool operator ==(Rotation rotationA, Rotation rotationB) {
			return rotationA.Turns == rotationB.Turns;
		}

		/// <summary>
		/// Determines whether two rotation are different.
		/// </summary>
		/// <param name="rotationA">The first rotation.</param>
		/// <param name="rotationB">The second rotation.</param>
		/// <returns>The two rotations are different.</returns>
		public static bool operator !=(Rotation rotationA, Rotation rotationB) {
			return rotationA.Turns != rotationB.Turns;
		}

		/// <summary>
		/// Rotates the rotation by 1 clockwise 90 degree turn.
		/// </summary>
		/// <param name="rotation"></param>
		/// <returns></returns>
		public static Rotation operator ++(Rotation rotation) {
			return rotation + (MAX >> 2);
		}

		/// <summary>
		/// Rotates the rotation by 1 counter clockwise 90 degree turn.
		/// </summary>
		/// <param name="rotation"></param>
		/// <returns></returns>
		public static Rotation operator --(Rotation rotation) {
			return rotation - (MAX >> 2);
		}

		/// <summary>
		/// Unboxing operator for rotations to ints.
		/// </summary>
		/// <param name="rotation">The rotation to unbox.</param>
		public static implicit operator int(Rotation rotation) {
			return rotation.turns;
		}

		/// <summary>
		/// Autoboxing operator for ints to rotations.
		/// </summary>
		/// <param name="turns">The int to autobox.</param>
		public static implicit operator Rotation(int turns) {
			return new Rotation(turns);
		}
		#endregion OPERATOR
	}
}
