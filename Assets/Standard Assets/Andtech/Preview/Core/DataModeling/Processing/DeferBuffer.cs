using System;
using System.Collections.Generic;

namespace Andtech {

	public class DeferBuffer<T> {
		public int Count => queue.Count;

		private readonly Queue<T> queue;

		public DeferBuffer(int capacity) {
			queue = new Queue<T>(capacity);
		}

		public void Push(T element) {
			queue.Enqueue(element);
		}

		public void Clear() {
			queue.Clear();
		}

		public void Flush() {
			OnFlush(queue);

			Clear();
		}

		#region EVENT
		public event Action<IEnumerable<T>> Flushed;

		protected virtual void OnFlush(IEnumerable<T> ops) => Flushed?.Invoke(ops);
		#endregion EVENT
	}
}
