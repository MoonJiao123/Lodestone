using Andtech.Core;

namespace Andtech.Tasking {

	/// <summary>
	/// Database task which supports standard data processsing.
	/// </summary>
	/// <typeparam name="T">The type of the data.</typeparam>
	public sealed class DataProcessTask<T> : DatabaseTask<T> where T : Data {
		private readonly DataProcessor processor;

		private DataProcessTask(Database<T> database, T data, T dataPreserved, CrudOperation operation, DataProcessor processor) : base(database, data, dataPreserved, operation) {
			this.processor = processor;
		}

		/// <summary>
		/// Automatically creates a CREATE data process task.
		/// </summary>
		/// <param name="database">The destination database.</param>
		/// <param name="data">The element to add to the database.</param>
		/// <param name="procesor">The processor used by the task.</param>
		/// <returns>The database task.</returns>
		public static DataProcessTask<T> NewCreateTask(Database<T> database, T data, DataProcessor processor) {
			return new DataProcessTask<T>(database, data, default, CrudOperation.Create, processor);
		}

		/// <summary>
		/// Automatically creates an UPDATE data process task.
		/// </summary>
		/// <param name="database">The destination database.</param>
		/// <param name="data">The element which will replace the database.</param>
		/// <param name="procesor">The processor used by the task.</param>
		/// <returns>The database task.</returns>
		public static DataProcessTask<T> NewUpdateTask(Database<T> database, T data, DataProcessor processor) {
			T dataPreserved = database.Get((data as IGuidIndexable).Guid);

			return new DataProcessTask<T>(database, data, dataPreserved, CrudOperation.Create, processor);
		}

		/// <summary>
		/// Automatically creates a DELETE data process task.
		/// </summary>
		/// <param name="database">The destination database.</param>
		/// <param name="data">The element to be deleted from the database.</param>
		/// <param name="procesor">The processor used by the task.</param>
		/// <returns>The database task.</returns>
		public static DataProcessTask<T> NewDeleteTask(Database<T> database, T data, DataProcessor processor) {
			return new DataProcessTask<T>(database, data, default, CrudOperation.Delete, processor);
		}

		#region OVERRIDE
		protected override void Create() {
			// Acquire data process
			DataProcess process;
			if (processor.ContainsProcess(Guid))
				process = processor.GetProcess(Guid);
			else
				process = processor.Initiate(Guid);

			base.Create();

			// Update data process
			process.OnCreate();
		}

		protected override void Update() {
			base.Update();

			// Update data process
			DataProcess process = processor.GetProcess(Guid);
			process.OnUpdate();
		}

		protected override void Revert() {
			base.Revert();

			// Update data process
			DataProcess process = processor.GetProcess(Guid);
			process.OnUpdate();
		}

		protected override void Delete() {
			base.Delete();

			// Update data process
			DataProcess process = processor.GetProcess(Guid);
			process.OnDelete();
		}

		public override void OnPostExecute() {
			DataProcess process = processor.GetProcess(Guid);
		}

		public override void OnPostUndo() {
			DataProcess process = processor.GetProcess(Guid);
		}

		public override void OnPostRedo() {
			DataProcess process = processor.GetProcess(Guid);
		}

		public override void OnRelease() {
		}
		#endregion OVERRIDE
	}
}
