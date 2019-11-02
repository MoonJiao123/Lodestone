using System;

namespace Andtech.Automata {

	public class MultipleDefinitionException : Exception {

		public MultipleDefinitionException(string message) : base(message) {
		}
		public MultipleDefinitionException(object state, object letter) :
			base(string.Format("The transition for state {0} and letter {1} is already defined", state, letter)) {
		}
	}

	public class UndefinedTransitionException : Exception {

		public UndefinedTransitionException(string message) : base(message) { }

		public UndefinedTransitionException(object state, object letter) :
			base(string.Format("Undefined transition for state {0} and letter {1}", state, letter)) {
		}
	}
}