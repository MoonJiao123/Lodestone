using System;
using UnityEngine;

namespace Andtech.Pooling {

	public static class PoolExtensions {

		/// <summary>
		/// Creates a pool with automatic reclamation handling.
		/// </summary>
		/// <typeparam name="T">The type of items in the pool.</typeparam>
		/// <param name="prefab">The item prefab.</param>
		/// <param name="folder">The destination transform of the items.</param>
		/// <returns>The created pool.</returns>
		public static Pool<T> Factory<T>(T prefab, Transform folder = default) where T : UnityEngine.Object, IPoolable {
			Pool<T> pool = default;

			pool = new Pool<T>(() => {
				T poolable = UnityEngine.Object.Instantiate(prefab as UnityEngine.Object, folder) as T;
				poolable.RequestedReclaim += (object sender, EventArgs e) => {
					pool.Reclaim(poolable);
				};

				return poolable;
			});

			return pool;
		}
	}
}
