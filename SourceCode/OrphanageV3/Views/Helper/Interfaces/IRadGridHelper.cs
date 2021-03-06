﻿using System.Collections.Generic;
using Telerik.WinControls.UI;

namespace OrphanageV3.Views.Helper.Interfaces
{
    public interface IRadGridHelper
    {
        RadGridView GridView { get; set; }

        IDictionary<string, IList<string>> GetSelectedData(IList<GridViewRowInfo> selectedRows);

        GridViewRowInfo GetRowByColumnName(string ColumnName, object SearchValue);

        object GetValueBySelectedRow(string ColumnName);

        void HideRow(string ColumnName, object SearchValue);

        void ShowRow(string ColumnName, object SearchValue);

        void UpdateRowColor(string ColorColumnName, long? ColorValue, string ColumnName, object SearchValue);

        void UpadteCellData(string IdColumnName, int IdValue, string ColumnName, ref object value);

        IEnumerable<int> GetCurrentRows(string IdColumnName);

        void InvalidateCurrentRows();

        void InvalidateRow(string IdColumnName, int IdValue);
    }
}