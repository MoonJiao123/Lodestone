
namespace Andtech.Core {

	public struct DataOp {
		public readonly CrudOperation CrudOperation;
		public readonly IDiff Diff;

		public DataOp(CrudOperation crudOperation, IDiff diff = default) {
			CrudOperation = crudOperation;
			Diff = diff;
		}
	}
}
