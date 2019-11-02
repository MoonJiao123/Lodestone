using System;

namespace Andtech.Core {

	public class DataProcessorEventArgs : EventArgs {
		public readonly Guid Guid;
		public readonly DataProcess Process;

		public DataProcessorEventArgs(Guid guid, DataProcess process) {
			Guid = guid;
			Process = process;
		}
	}
}
