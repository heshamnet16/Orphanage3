using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace OrphanageV3.Views.Helper.Interfaces
{
    public interface IRadGridHelper
    {        
        RadGridView GridView { get; set; }

        GridViewRowInfo GetRowByColumnName(string ColumnName, object SearchValue);

        object GetValueBySelectedRow(string ColumnName);

        void HideRow(string ColumnName,object SearchValue);

        void ShowRow(string ColumnName, object SearchValue);

        void UpdateRowColor(string ColorColumnName, long? ColorValue, string ColumnName, object SearchValue);

        void UpadteCellData(string IdColumnName,int IdValue, string ColumnName, object value);

        IEnumerable<int> GetCurrentRows(string IdColumnName);

        void InvalidateCurrentRows();

        void InvalidateRow(string IdColumnName, int IdValue);
    }
}
