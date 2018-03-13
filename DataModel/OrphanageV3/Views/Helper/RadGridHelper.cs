using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls;
using Telerik.WinControls.UI;
namespace OrphanageV3.Views.Helper
{
    public class RadGridHelper : IRadGridHelper
    {
        public object LockObject;

        public RadGridHelper()
        {
            LockObject = new object();
        }

        public RadGridView GridView { get; set; }

        public GridViewRowInfo GetRowByColumnName(string ColumnName, object SearchValue)
        {
            lock (LockObject)
            {
                return GridView.Rows.FirstOrDefault(r => r.Cells[ColumnName].Value.ToString() == SearchValue.ToString());
            }
        }

        public object GetValueBySelectedRow(string ColumnName)
        {
            GridViewRowInfo row = null;
            object id = null;
            lock (LockObject)
            {
                if (GridView.SelectedRows != null && GridView.SelectedRows.Count > 0)
                {
                    row = GridView.SelectedRows[0];
                    id = row.Cells[ColumnName].Value;
                }
            }
            return id;
        }

        public void HideRow(string ColumnName, object SearchValue)
        {
            var row = GetRowByColumnName(ColumnName, SearchValue);
            lock (LockObject)
            {
                row.IsVisible = false;
                GridView.GridNavigator.SelectNextRow(1);
            }
        }

        public void ShowRow(string ColumnName, object SearchValue)
        {
            var row = GetRowByColumnName(ColumnName, SearchValue);
            lock (LockObject)
            {
                row.IsVisible = true;
                GridView.GridNavigator.SelectRow(row);
            }
        }

        public void UpdateRowColor(string ColorColumnName, long? ColorValue, string ColumnName, object SearchValue)
        {
            var row = GetRowByColumnName(ColumnName, SearchValue);
            lock (LockObject)
            {
                row.Cells[ColorColumnName].Value = ColorValue;
            }
        }

        public IEnumerable<int> GetCurrentRows(string columnName)
        {
            List<int> retList = new List<int>();
            lock (LockObject)
            {

                foreach (var row in GridView.ChildRows)
                    if (row.Cells[columnName].Value != null)
                        retList.Add((int)row.Cells[columnName].Value);
                return retList;
            }
        }

        public void UpadteCellData(string IdColumnName, int IdValue, string ColumnName, ref object value)
        {
            var row = GetRowByColumnName(IdColumnName, IdValue);
            lock (LockObject)
            {
                row.Cells[ColumnName].Value = value;
            }
        }

        public void InvalidateCurrentRows()
        {
            lock (LockObject)
            {
                foreach (var row in GridView.ChildRows)
                    row.InvalidateRow();
            }

        }

        public void InvalidateRow(string IdColumnName, int IdValue)
        {
            var row = GetRowByColumnName(IdColumnName, IdValue);
            lock (LockObject)
            {
                row.InvalidateRow();
            }
        }
    }
}
