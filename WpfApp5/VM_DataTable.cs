using MvvmGen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    [Inject(typeof(IDataRecordFactory))]
    [ViewModelGenerateFactory]
    [ViewModel]
    public partial class VM_DataTable
    {
        [Property] private ObservableCollection<DataRecord> dataRecordCollection = [];

        partial void OnInitialize()
        {
            var record = DataRecordFactory.Create();
            record.Id = 0;
            _ = record.GenerateRandomData();
            DataRecordCollection.Add(record);

            record = DataRecordFactory.Create();
            record.Id = 1;
            _ = record.GenerateRandomData();
            DataRecordCollection.Add(record);

            record = DataRecordFactory.Create();
            record.Id = 2;
            _ = record.GenerateRandomData();
            DataRecordCollection.Add(record);
        }
    }
}
