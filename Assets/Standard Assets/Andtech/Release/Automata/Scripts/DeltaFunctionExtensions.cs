using System;

namespace Andtech.Automata {

	public static class DeltaFunctionExtensions {

		public static void Add<S, A>(this DeltaFunction<S, A> deltaFunction, ValueTuple<S, A, S> tuple) {
			deltaFunction.Define(tuple.Item1, tuple.Item2, tuple.Item3);
		}
	}
}
