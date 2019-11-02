using System;
using System.Collections.Generic;

namespace Andtech.Pooling {

    /// <summary>
    /// An object pool with automatic resizing.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    public class Pool<T> where T : IPoolable {
        /// <summary>
        /// How many items are available in the pool?
        /// </summary>
        public int Count => count;

        private readonly Func<T> objectFactory;
        private readonly LinkedList<T> list;

        private int count;

        public Pool(Func<T> objectFactory) {
            list = new LinkedList<T>();
            this.objectFactory = objectFactory;
        }

        #region VIRTUAL
        /// <summary>
        /// Fills the pool to capacity.
        /// </summary>
        /// <param name="capacity">The maximum number of items.</param>
        public virtual void Fill(int capacity) {
            while (count < capacity) {
                Make();
            }
        }

        /// <summary>
        /// Retrieves an item from the pool.
        /// </summary>
        /// <returns>The next item.</returns>
        public virtual T Poll() {
            if (Count == 0)
                Make();

            LinkedListNode<T> node = list.First;
            T poolable = node.Value;

            list.RemoveFirst();
            --count;

            (poolable as IPoolObserver)?.OnDispatch();

            return poolable;
        }

        /// <summary>
        /// Returns an item to the pool.
        /// </summary>
        /// <param name="poolable">The item to return.</param>
        public virtual void Reclaim(T poolable) {
            list.AddLast(poolable);
            ++count;

            (poolable as IPoolObserver)?.OnReclaim();
        }
        #endregion VIRTUAL

        #region PIPELINE
        private T Make() {
            T poolable = objectFactory();
            list.AddLast(poolable);
            ++count;

			(poolable as IPoolObserver)?.OnReclaim();

            return poolable;
        }
        #endregion PIPELINE
    }
}
