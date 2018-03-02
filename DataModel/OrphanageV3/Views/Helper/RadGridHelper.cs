﻿using OrphanageV3.Views.Helper.Interfaces;
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

        public RadGridView GridView { get ; set; }

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
            var row = GetRowByColumnName(ColumnName,SearchValue);
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
    }
}
