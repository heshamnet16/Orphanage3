using System;
using System.Collections.Generic;
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
    }
}
