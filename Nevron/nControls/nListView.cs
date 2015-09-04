namespace nControls
{
    //using SqlTools;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Nevron.UI.WinForm.Controls;

    [ToolboxBitmap(typeof(resfinder), "nControls.icon.nListView.bmp")]
    public class nListView : NListView, IEnumerable
    {
        //private SqlArrayClass array;
        private bool dbIsSet;
        public readonly string name;
        public IEnumerator rowEnumerator;

        public nListView()
        {
            base.View = View.Details;
            this.rowEnumerator = base.Items.GetEnumerator();
        }

        public nListView(view viewMode)
        {
            this.setView(viewMode);
            this.rowEnumerator = base.Items.GetEnumerator();
        }

        public nListView(string viewMode)
        {
            this.setView(viewMode);
            this.rowEnumerator = base.Items.GetEnumerator();
        }

        public nListView(View viewMode)
        {
            this.setView(viewMode);
            this.rowEnumerator = base.Items.GetEnumerator();
        }

        public void addCol()
        {
            base.Columns.Add("");
        }

        public void addCol(string key)
        {
            base.Columns.Add(key);
            base.Columns[base.Columns.Count - 1].Name = key;
        }

        public void addCol(string key, int width)
        {
            base.Columns.Add(key, width);
            base.Columns[base.Columns.Count - 1].Name = key;
        }

        public void addCol(string key, int width, align alignment)
        {
            base.Columns.Add(key, width, this.getAlign(alignment));
            base.Columns[base.Columns.Count - 1].Name = key;
        }

        public void addRow()
        {
            base.Items.Add(new ListViewItem());
        }

        public void addRow(params string[] values)
        {
            int count;
            ListViewItem.ListViewSubItem[] itemArray;
            if (values.Length > base.Columns.Count)
            {
                count = base.Columns.Count;
                itemArray = new ListViewItem.ListViewSubItem[count];
            }
            else
            {
                count = values.Length;
                itemArray = new ListViewItem.ListViewSubItem[count];
            }
            for (int i = 0; i < count; i++)
            {
                itemArray[i] = new ListViewItem.ListViewSubItem();
                itemArray[i].Name = base.Columns[i].Name;
                itemArray[i].Text = values[i];
            }
            ListViewItem item = new ListViewItem(itemArray, 0);
            item.Name = values[0];
            item.Text = values[0];
            base.Items.Add(item);
        }

        /*
        private void addSetToView()
        {
            if (this.array.get_readiness())
            {
                base.Clear();
                foreach (string str in this.array.get_colList())
                {
                    this.addCol(str);
                }
            }
            else
            {
                Console.WriteLine("failed to extract dataset");
            }
        }
        */

        public void bob()
        {
        }

        private void boo()
        {
        }

        public HorizontalAlignment getAlign(align alignment)
        {
            switch (alignment)
            {
                case align.left:
                    return HorizontalAlignment.Left;

                case align.center:
                    return HorizontalAlignment.Center;

                case align.right:
                    return HorizontalAlignment.Right;
            }
            return HorizontalAlignment.Center;
        }

        public string getAllColumns()
        {
            string str = "";
            for (int i = 0; i < base.Columns.Count; i++)
            {
                str = ((str + i.ToString() + ": ") + base.Columns[i].Name + " -> ") + base.Columns[i].Text + "\n";
            }
            return str;
        }

        public int getColumnIndex(string key)
        {
            try
            {
                return base.Columns[key].Index;
            }
            catch
            {
                return -1;
            }
        }

        public string getColumnKey(int index)
        {
            try
            {
                return base.Columns[index].Name;
            }
            catch
            {
                return "not found";
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.rowEnumerator;
        }

        public int getRowIndex(string key)
        {
            try
            {
                return base.Items[key].Index;
            }
            catch
            {
                return -1;
            }
        }

        public string getRowKey(int index)
        {
            try
            {
                return base.Items[index].Name;
            }
            catch
            {
                return "not found";
            }
        }

        public string getSelectedValueAt(int colIndex)
        {
            if (base.SelectedItems.Count > 0)
            {
                return base.SelectedItems[0].SubItems[colIndex].Text;
            }
            return "Nothing Selected";
        }

        public string getSelectedValueAt(string colKey)
        {
            if (base.SelectedItems.Count > 0)
            {
                return base.SelectedItems[0].SubItems[colKey].Text;
            }
            return "Nothing Selected";
        }

        public string getValue(int rowID, int columnID)
        {
            if (this.getRowKey(rowID).Equals("not found") || this.getColumnKey(columnID).Equals("not found"))
            {
                return "you overstepped the bounds of the nListView";
            }
            if (columnID.Equals(0))
            {
                return base.Items[rowID].Text;
            }
            return base.Items[rowID].SubItems[this.getColumnKey(columnID)].Text;
        }

        public string getValue(int rowID, string columnKey)
        {
            if (this.getRowKey(rowID).Equals("not found") || this.getColumnIndex(columnKey).Equals(-1))
            {
                return "you overstepped the bounds of the nListView";
            }
            if (this.getColumnIndex(columnKey).Equals(0))
            {
                return base.Items[rowID].Text;
            }
            return base.Items[rowID].SubItems[columnKey].Text;
        }

        public string getValue(string rowKey, int columnID)
        {
            if (this.getRowIndex(rowKey).Equals(-1) || this.getColumnKey(columnID).Equals("not found"))
            {
                return "you overstepped the bounds of the nListView";
            }
            if (columnID.Equals(0))
            {
                return base.Items[rowKey].Text;
            }
            return base.Items[rowKey].SubItems[this.getColumnKey(columnID)].Text;
        }

        public string getValue(string rowKey, string columnKey)
        {
            if (this.getRowIndex(rowKey).Equals(-1) || this.getColumnIndex(columnKey).Equals(-1))
            {
                return "you overstepped the bounds of the nListView";
            }
            if (this.getColumnIndex(columnKey).Equals(0))
            {
                return base.Items[rowKey].Text;
            }
            return base.Items[rowKey].SubItems[columnKey].Text;
        }

        /*
        public bool setDataSet(MySqlClass mysql)
        {
            this.dbIsSet = false;
            try
            {
                this.array = mysql.get_table();
                this.addSetToView();
                this.dbIsSet = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message + "\n\r-> error attempting to set mysql data set");
                return false;
            }
            return true;
        }
         * */

        public void setSelectedValueAt(int colIndex, string value)
        {
            if (base.SelectedItems.Count > 0)
            {
                base.SelectedItems[0].SubItems[colIndex].Text = value;
            }
        }

        public void setSize(int width, int height)
        {
            if (width >= 0)
            {
                base.Width = width;
            }
            if (height >= 0)
            {
                base.Height = height;
            }
        }

        public void setSlectedValueAt(string colKey, string value)
        {
            if (base.SelectedItems.Count > 0)
            {
                base.SelectedItems[0].SubItems[colKey].Text = value;
            }
        }

        public void setView(view v)
        {
            switch (v)
            {
                case view.details:
                    base.View = View.Details;
                    return;

                case view.largeIcons:
                    base.View = View.LargeIcon;
                    return;

                case view.smallIcons:
                    base.View = View.SmallIcon;
                    return;

                case view.list:
                    base.View = View.List;
                    return;

                case view.tile:
                    base.View = View.Tile;
                    return;
            }
        }

        public void setView(string v)
        {
            switch (v)
            {
                case "details":
                case "Details":
                    base.View = View.Details;
                    return;

                case "largeIcons":
                case "LargeIcons":
                    base.View = View.LargeIcon;
                    return;

                case "list":
                case "List":
                    base.View = View.List;
                    return;

                case "smallIcons":
                case "SmallIcons":
                    base.View = View.SmallIcon;
                    return;

                case "tile":
                case "Tile":
                    base.View = View.Tile;
                    return;
            }
        }

        public void setView(View v)
        {
            base.View = v;
        }

        public ListViewItem this[string rowKey]
        {
            get
            {
                return base.Items[rowKey];
            }
            set
            {
                base.Items[base.Items[rowKey].Index] = value;
            }
        }

        public ListViewItem this[int rowIndex]
        {
            get
            {
                return base.Items[rowIndex];
            }
            set
            {
                base.Items[rowIndex] = value;
            }
        }

        public ListViewItem.ListViewSubItem this[string rowKey, string colKey]
        {
            get
            {
                return base.Items[rowKey].SubItems[colKey];
            }
            set
            {
                base.Items[base.Items[rowKey].Index].SubItems[base.Columns.IndexOfKey(colKey)] = value;
            }
        }

        public ListViewItem.ListViewSubItem this[int rowIndex, int colIndex]
        {
            get
            {
                return base.Items[rowIndex].SubItems[colIndex];
            }
            set
            {
                base.Items[rowIndex].SubItems[colIndex] = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Column
        {
            public string key;
            public int index;
            public int width;
            public align alignment;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Row
        {
            public string key;
            public int index;
            public List<Element> values;
        }
    }
}

