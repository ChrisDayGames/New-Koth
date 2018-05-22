using System;

namespace CustomCollections {

	public interface IHeapItem<T> : IComparable<T> {
		int HeapIndex {
			get;
			set;
		}
	}

}