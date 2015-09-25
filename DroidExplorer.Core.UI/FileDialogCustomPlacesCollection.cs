using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.UI {
  public class FileDialogCustomPlacesCollection : IList<FileDialogCustomPlace> {
    public event EventHandler ItemAdded;
    public event EventHandler ItemRemoved;
    public event EventHandler ItemsSorted;

    public FileDialogCustomPlacesCollection ( ) {
      this.Items = new List<FileDialogCustomPlace> ( );
    }

    protected void OnItemAdded ( EventArgs e ) {
      if ( this.ItemAdded != null ) {
        this.ItemAdded ( this, e );
      }
    }

    protected void OnItemsSorted ( EventArgs e ) {
      if ( this.ItemsSorted != null ) {
        this.ItemsSorted ( this, e );
      }
    }

    protected void OnItemRemoved ( EventArgs e ) {
      if ( this.ItemRemoved != null ) {
        this.ItemRemoved ( this, e );
      }
    }

    private List<FileDialogCustomPlace> Items { get; set; }
    #region IList<FileDialogCustomPlace> Members

    public int IndexOf ( FileDialogCustomPlace item ) {
      return this.Items.IndexOf ( item );
    }

    public void Insert ( int index, FileDialogCustomPlace item ) {
      this.Items.Insert ( index, item );
      this.OnItemAdded ( EventArgs.Empty );
    }

    public void InsertRange ( int index, IEnumerable<FileDialogCustomPlace> collection ) {
      this.Items.InsertRange ( index, collection );
      this.OnItemAdded ( EventArgs.Empty );
    }

    public void RemoveAt ( int index ) {
      this.Items.RemoveAt ( index );
    }

    public FileDialogCustomPlace this[ int index ] {
      get {
        return this.Items[ index ];
      }
      set {
        this.Items[ index ] = value;
      }
    }

    #endregion

    #region ICollection<FileDialogCustomPlace> Members

    public void Add ( FileDialogCustomPlace item ) {
      this.Items.Add ( item );
      this.OnItemAdded ( EventArgs.Empty );
    }

    public void AddRange ( IEnumerable<FileDialogCustomPlace> collection ) {
      this.Items.AddRange ( collection );
      this.OnItemAdded ( EventArgs.Empty );
    }

    public void Clear ( ) {
      this.Items.Clear ( );
      this.OnItemRemoved( EventArgs.Empty );
    }

    public bool Contains ( FileDialogCustomPlace item ) {
      return this.Items.Contains ( item );
    }

    public void CopyTo ( FileDialogCustomPlace[ ] array, int arrayIndex ) {
      this.Items.CopyTo ( array, arrayIndex );
    }

    public void CopyTo ( FileDialogCustomPlace[ ] array ) {
      this.Items.CopyTo ( array );
    }

    public void CopyTo ( int index, FileDialogCustomPlace[ ] array, int arrayIndex, int count ) {
      this.Items.CopyTo ( index, array, arrayIndex, count );
    }

    public int Count {
      get { return this.Items.Count; }
    }

    public bool IsReadOnly {
      get { return false; }
    }

    public bool Remove ( FileDialogCustomPlace item ) {
      return this.Items.Remove ( item );
      this.OnItemRemoved ( EventArgs.Empty );


    }

    public int RemoveAll ( Predicate<FileDialogCustomPlace> match ) {
      return this.Items.RemoveAll ( match );
      this.OnItemRemoved ( EventArgs.Empty );

    }

    public void RemoveRange ( int index, int count ) {
      this.Items.RemoveRange ( index, count );
      this.OnItemRemoved ( EventArgs.Empty );

    }

    public void Reverse ( ) {
      this.Items.Reverse ( );
      this.OnItemsSorted ( EventArgs.Empty );
    }

    public void Reverse ( int index, int count ) {
      this.Items.Reverse ( index, count );
      this.OnItemsSorted ( EventArgs.Empty );
    }

    public void Sort ( ) {
      this.Items.Sort ( );
      this.OnItemsSorted ( EventArgs.Empty );
    }

    public void Sort ( Comparison<FileDialogCustomPlace> comparison ) {
      this.Items.Sort ( comparison );
      this.OnItemsSorted ( EventArgs.Empty );
    }

    public void Sort ( IComparer<FileDialogCustomPlace> comparer ) {
      this.Items.Sort ( comparer );
      this.OnItemsSorted ( EventArgs.Empty );
    }

    public void Sort ( int index, int count, IComparer<FileDialogCustomPlace> comparer ) {
      this.Items.Sort ( index, count, comparer );
      this.OnItemsSorted ( EventArgs.Empty );
    }

    public bool TrueForAll ( Predicate<FileDialogCustomPlace> match ) {
      return this.Items.TrueForAll ( match );
    }

    public void TrimExcess ( ) {
      int icount = this.Count;
      this.Items.TrimExcess ( );
      if ( this.Count < icount ) {
        this.OnItemRemoved ( EventArgs.Empty );
      }

    }

    public System.Collections.ObjectModel.ReadOnlyCollection<FileDialogCustomPlace> AsReadOnly ( ) {
      return this.Items.AsReadOnly ( );
    }

    public int BinarySearch ( FileDialogCustomPlace item ) {
      return this.Items.BinarySearch ( item );
    }

    public int BinarySearch ( FileDialogCustomPlace item, IComparer<FileDialogCustomPlace> comparer ) {
      return this.Items.BinarySearch ( item, comparer );
    }

    public int BinarySearch ( int index, int count, FileDialogCustomPlace item, IComparer<FileDialogCustomPlace> comparer ) {
      return this.Items.BinarySearch ( index, count, item, comparer );
    }

    public int Capacity {
      get { return this.Items.Capacity; }
      set { this.Items.Capacity = value; }
    }

    public bool Exists ( Predicate<FileDialogCustomPlace> match ) {
      return this.Items.Exists ( match );
    }

    public FileDialogCustomPlace Find ( Predicate<FileDialogCustomPlace> match ) {
      return this.Items.Find ( match );
    }

    public List<FileDialogCustomPlace> FindAll ( Predicate<FileDialogCustomPlace> match ) {
      return this.Items.FindAll ( match );
    }

    public int FindIndex ( Predicate<FileDialogCustomPlace> match ) {
      return this.Items.FindIndex ( match );
    }

    public FileDialogCustomPlace FindLast ( Predicate<FileDialogCustomPlace> match ) {
      return this.Items.FindLast ( match );
    }

    public int FindLastIndex ( Predicate<FileDialogCustomPlace> match ) {
      return this.Items.FindLastIndex ( match );
    }

    public void ForEach ( Action<FileDialogCustomPlace> action ) {
      this.Items.ForEach ( action );
    }

    public List<FileDialogCustomPlace> GetRange ( int index, int count ) {
      return this.Items.GetRange ( index, count  );
    }

    public int LastIndexOf ( FileDialogCustomPlace item ) {
      return this.Items.LastIndexOf ( item );
    }

    public int LastIndexOf ( FileDialogCustomPlace item, int index ) {
      return this.Items.LastIndexOf ( item , index);
    }

    public int LastIndexOf ( FileDialogCustomPlace item, int index, int count ) {
      return this.Items.LastIndexOf ( item, index, count );
    }
    #endregion

    #region IEnumerable<FileDialogCustomPlace> Members

    public IEnumerator<FileDialogCustomPlace> GetEnumerator ( ) {
      return this.Items.GetEnumerator ( );
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ( ) {
      return this.GetEnumerator ( );
    }

    #endregion
  }
}
