using System;

namespace Andtech.Core {

	[Flags]
	public enum CrudOperation {
		None			= 0,
		Create			= 1 << 0,
		Read			= 1 << 1,
		Update			= 1 << 2,
		Delete			= 1 << 3,

		CreateOrUpdate	= Create | Update
	}
}
