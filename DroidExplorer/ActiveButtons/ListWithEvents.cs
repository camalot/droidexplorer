/*=============================================================================
*
*	(C) Copyright 2011, Michael Carlisle (mike.carlisle@thecodeking.co.uk)
*
*   http://www.TheCodeKing.co.uk
*  
*	All rights reserved.
*	The code and information is provided "as-is" without waranty of any kind,
*	either expresed or implied.
*
*-----------------------------------------------------------------------------
*	History:
*		01/09/2007	Michael Carlisle				Version 1.0
*=============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace DroidExplorer.ActiveButtons {
	internal class ListModificationEventArgs : ListRangeEventArgs {
		private readonly ListModification modification;

		public ListModificationEventArgs(ListModification modification, int startIndex, int count)
			: base(startIndex, count) {
			this.modification = modification;
		}

		public ListModification Modification {
			get { return modification; }
		}
	}

	internal class ListItemEventArgs : EventArgs {
		private readonly int itemIndex;

		public ListItemEventArgs(int itemIndex) {
			this.itemIndex = itemIndex;
		}

		public int ItemIndex {
			get { return itemIndex; }
		}
	}

	internal class ListRangeEventArgs : EventArgs {
		private readonly int count;
		private readonly int startIndex;

		public ListRangeEventArgs(int startIndex, int count) {
			this.startIndex = startIndex;
			this.count = count;
		}

		public int StartIndex {
			get { return startIndex; }
		}

		public int Count {
			get { return count; }
		}
	}

	internal enum ListModification {
		/// <summary>
		/// 	The list has been cleared.
		/// </summary>
		Cleared = 0,
		/// <summary>
		/// 	A new item has been added.
		/// </summary>
		ItemAdded,
		/// <summary>
		/// 	An item has been modified.
		/// </summary>
		ItemModified,
		/// <summary>
		/// 	An item has been removed.
		/// </summary>
		ItemRemoved,
		/// <summary>
		/// 	A range of items has been added.
		/// </summary>
		RangeAdded,
		/// <summary>
		/// 	A range of items has been removed.
		/// </summary>
		RangeRemoved
	}

	[Serializable]
	internal class ListWithEvents<T> : List<T>, IList<T>, IList {
		private readonly object syncRoot = new object();
		private bool suppressEvents;

		public ListWithEvents() {
		}

		public ListWithEvents(IEnumerable<T> collection)
			: base(collection) {
		}

		public ListWithEvents(int capacity)
			: base(capacity) {
		}

		protected bool EventsSuppressed {
			get { return suppressEvents; }
		}

		#region IList Members

		public object SyncRoot {
			get { return syncRoot; }
		}

		int IList.Add(object value) {
			if(value is T) {
				Add((T)value);
				return Count - 1;
			}
			return -1;
		}

		#endregion

		#region IList<T> Members

		public new virtual T this[int index] {
			get { return base[index]; }
			set {
				lock(syncRoot) {
					bool equal = false;
					if(base[index] != null) {
						equal = base[index].Equals(value);
					} else if(base[index] == null && value == null) {
						equal = true;
					}

					if(!equal) {
						base[index] = value;
						OnItemModified(new ListItemEventArgs(index));
					}
				}
			}
		}

		public new virtual void Add(T item) {
			int count;
			lock(syncRoot) {
				base.Add(item);
				count = base.Count - 1;
			}
			OnItemAdded(new ListItemEventArgs(count));
		}

		public new virtual void Clear() {
			lock(syncRoot) {
				base.Clear();
			}
			OnCleared(EventArgs.Empty);
		}

		public new virtual void Insert(int index, T item) {
			lock(syncRoot) {
				base.Insert(index, item);
			}
			OnItemAdded(new ListItemEventArgs(index));
		}

		public new virtual bool Remove(T item) {
			bool result;

			lock(syncRoot) {
				result = base.Remove(item);
			}

			// raise the event only if the removal was successful
			if(result) {
				OnItemRemoved(EventArgs.Empty);
			}

			return result;
		}

		public new virtual void RemoveAt(int index) {
			lock(syncRoot) {
				base.RemoveAt(index);
			}
			OnItemRemoved(EventArgs.Empty);
		}

		#endregion

		public event EventHandler<ListModificationEventArgs> CollectionModified;

		public event EventHandler Cleared;

		public event EventHandler<EventArgs> ItemAdded;

		public event EventHandler<EventArgs> ItemModified;

		public event EventHandler ItemRemoved;

		public event EventHandler<ListRangeEventArgs> RangeAdded;

		public event EventHandler RangeRemoved;

		public new virtual void AddRange(IEnumerable<T> collection) {
			lock(syncRoot) {
				InsertRange(base.Count, collection);
			}
		}

		public new virtual void InsertRange(int index, IEnumerable<T> collection) {
			int count;
			lock(syncRoot) {
				base.InsertRange(index, collection);
				count = base.Count - index;
			}
			OnRangeAdded(new ListRangeEventArgs(index, count));
		}

		public new virtual int RemoveAll(Predicate<T> match) {
			int count;

			lock(syncRoot) {
				count = base.RemoveAll(match);
			}

			// raise the event only if the removal was successful
			if(count > 0) {
				OnRangeRemoved(EventArgs.Empty);
			}

			return count;
		}

		/// <summary>
		/// 	Overloads <see cref = "List{T}.RemoveRange" />.
		/// </summary>
		/// <remarks>
		/// 	This operation is thread-safe.
		/// </remarks>
		public new virtual void RemoveRange(int index, int count) {
			int listCountOld, listCountNew;
			lock(syncRoot) {
				listCountOld = base.Count;
				base.RemoveRange(index, count);
				listCountNew = base.Count;
			}

			// raise the event only if the removal was successful
			if(listCountOld != listCountNew) {
				OnRangeRemoved(EventArgs.Empty);
			}
		}

		public virtual void RemoveRange(List<T> collection) {
			for(int i = 0; i < collection.Count; i++) {
				Remove(collection[i]);
			}
		}

		public void SuppressEvents() {
			suppressEvents = true;
		}

		public void ResumeEvents() {
			suppressEvents = false;
		}

		protected virtual void OnCleared(EventArgs e) {
			if(suppressEvents) {
				return;
			}

			if(Cleared != null) {
				Cleared(this, e);
			}

			OnCollectionModified(new ListModificationEventArgs(ListModification.Cleared, -1, -1));
		}

		protected virtual void OnCollectionModified(ListModificationEventArgs e) {
			if(suppressEvents) {
				return;
			}

			if(CollectionModified != null) {
				CollectionModified(this, e);
			}
		}

		protected virtual void OnItemAdded(ListItemEventArgs e) {
			if(suppressEvents) {
				return;
			}

			if(ItemAdded != null) {
				ItemAdded(this, e);
			}

			OnCollectionModified(new ListModificationEventArgs(ListModification.ItemAdded, e.ItemIndex, 1));
		}

		protected virtual void OnItemModified(ListItemEventArgs e) {
			if(suppressEvents) {
				return;
			}

			if(ItemModified != null) {
				ItemModified(this, e);
			}

			OnCollectionModified(new ListModificationEventArgs(ListModification.ItemModified, e.ItemIndex, 1));
		}

		protected virtual void OnItemRemoved(EventArgs e) {
			if(suppressEvents) {
				return;
			}

			if(ItemRemoved != null) {
				ItemRemoved(this, e);
			}

			OnCollectionModified(new ListModificationEventArgs(ListModification.ItemRemoved, -1, 1));
		}

		protected virtual void OnRangeAdded(ListRangeEventArgs e) {
			if(suppressEvents) {
				return;
			}

			if(RangeAdded != null) {
				RangeAdded(this, e);
			}

			OnCollectionModified(new ListModificationEventArgs(ListModification.RangeAdded, e.StartIndex, e.Count));
		}

		protected virtual void OnRangeRemoved(EventArgs e) {
			if(suppressEvents) {
				return;
			}

			if(RangeRemoved != null) {
				RangeRemoved(this, e);
			}

			OnCollectionModified(new ListModificationEventArgs(ListModification.RangeRemoved, -1, -1));
		}
	}
}