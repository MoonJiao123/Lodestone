
namespace Andtech.Automata {

	public struct Transition<S, A> {
		public S PresentState;
		public A Letter;
		public S NextState;
	}
}
