﻿using UnityEngine;

namespace Andtech {

	/// <summary>
	/// A 3D subspace.
	/// </summary>
	public class Basis {
		public Vector3 Origin => Globalization.GetColumn(3);
		public Quaternion Rotation => Globalization.rotation;
		public Vector3 Scale => Globalization.lossyScale;
		public Vector3 this[int index] {
			get {
				switch (index) {
					case 0:
						return Globalization.MultiplyVector(Vector3.right);
					case 1:
						return Globalization.MultiplyVector(Vector3.up);
					case 2:
						return Globalization.MultiplyVector(Vector3.forward);
					default:
						throw new System.IndexOutOfRangeException();
				}
			}
		}

		private readonly Matrix4x4 Localization;
		private readonly Matrix4x4 Globalization;

		public Basis(Vector3 basis0, Vector3 basis1, Vector3 basis2) : this(basis0, basis1, basis2, Vector3.zero) { }

		public Basis(Vector3 basis0, Vector3 basis1, Vector3 basis2, Vector3 origin) {
			Localization = MatrixUtility.GetLocalizationMatrix(basis0, basis1, basis2, origin);
			Globalization = Localization.inverse;
		}

		/// <summary>
		/// Converts the vector to this coordinate system.
		/// </summary>
		/// <param name="vector">The position (worldspace) to convert.</param>
		/// <returns>The equivalent position in basis localspace coordinates.</returns>
		public Vector3 Localize(Vector3 vector) => Localization.MultiplyPoint3x4(vector);

		/// <summary>
		/// Converts the vector to global coordinates.
		/// </summary>
		/// <param name="vector">The position (local) to convert.</param>
		/// <returns>The equivalent position in worldspace coordinates</returns>
		public Vector3 Globalize(Vector3 vector) => Globalization.MultiplyPoint3x4(vector);
	}
}
