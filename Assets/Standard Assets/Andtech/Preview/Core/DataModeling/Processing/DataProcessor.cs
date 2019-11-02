using System;
using System.Collections.Generic;

namespace Andtech.Core {
	
	/// <summary>
	/// Process management for data.
	/// </summary>
	public class DataProcessor {
		private readonly Dictionary<Guid, DataProcess> processes;

		public DataProcessor() {
			processes = new Dictionary<Guid, DataProcess>();
		}

		/// <summary>
		/// Returns whether a process with the given GUID exists.
		/// </summary>
		/// <param name="guid">The GUID of the process.</param>
		/// <returns>Does the process exists?</returns>
		public bool ContainsProcess(Guid guid) {
			return processes.ContainsKey(guid);
		}

		/// <summary>
		/// Retrieves a data process.
		/// </summary>
		/// <param name="guid">The GUID of the process.</param>
		/// <returns>The data process.</returns>
		public DataProcess GetProcess(Guid guid) {
			return processes[guid];
		}

		#region VIRTUAL
		/// <summary>
		/// Initiates a new process given the GUID.
		/// </summary>
		/// <param name="guid">The GUID of the process.</param>
		public virtual DataProcess Initiate(Guid guid) => Initiate(guid, () => new DataProcess(guid));

		/// <summary>
		/// Initiates a new process given the GUID.
		/// </summary>
		/// <param name="guid">The GUID of the process.</param>
		/// <param name="processFactory">How processes will be created.</param>
		public virtual DataProcess Initiate(Guid guid, Func<DataProcess> processFactory) {
			// Initiate a new proces
			DataProcess process = processFactory();
			process.Released += ProcessReleased;
			processes.Add(guid, process);

			// Fire events
			OnInitiateProcess(new DataProcessorEventArgs(guid, process));

			return process;

			#region LOCAL_FUNCTIONS
			void ProcessReleased(Guid id) {
				process.Released -= ProcessReleased;

				Terminate(guid);
			}
			#endregion LOCAL_FUNCTIONS
		}

		public virtual bool Terminate(Guid guid) {
			if (!ContainsProcess(guid))
				return false;

			DataProcess process = processes[guid];
			processes.Remove(guid);

			OnTerminateProcess(new DataProcessorEventArgs(guid, process));

			return true;
		}
		#endregion VIRTUAL

		#region EVENT
		/// <summary>
		/// Event fired when a data process begins.
		/// </summary>
		public event Action<DataProcessorEventArgs> InitiatedProcess;
		/// <summary>
		/// Event fired when a data process ended.
		/// </summary>
		public event Action<DataProcessorEventArgs> TerminatedProcess;

		protected virtual void OnInitiateProcess(DataProcessorEventArgs e) => InitiatedProcess?.Invoke(e);
		
		protected virtual void OnTerminateProcess(DataProcessorEventArgs e) => TerminatedProcess?.Invoke(e);
		#endregion EVENT
	}
}
