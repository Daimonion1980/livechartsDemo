using MvvmGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp5
{
    [Inject(typeof(IVM_PlotFactory))]
    [Inject(typeof(IVM_DataTableFactory))]
    [ViewModel]
    public partial class VM_MainWindow
    {
        [Property] private VM_DataTable dataTable;
        [Property] private VM_Plot plot;

        partial void OnInitialize()
        {
            Plot = VM_PlotFactory.Create();
            DataTable = VM_DataTableFactory.Create();
        }

    }
}
