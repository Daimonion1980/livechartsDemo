using MvvmGen;
using MvvmGen.Events;
using DotNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfApp5
{

    [Inject(typeof(IEventAggregator))]
    [ViewModelGenerateFactory]
    [ViewModel]
    public partial class DataRecord
    {
        [Property] private int id;
        [Property] private string name;
        [PropertyCallMethod(nameof(RowSelected))]
        [Property] private bool isSelected; 
        IList<int> Record = new List<int>();

        void RowSelected()
        {
            EventAggregator.Publish(new RowSelectedEvent((IReadOnlyCollection<int>)Record, Name, Id, IsSelected)); //for testing purposes implicit cast is ok. 
        }

        public async Task GenerateRandomData(CancellationToken cancellationToken = default)
        {
            Name = new Random().NextString("abcdefghijklmnopqrstuvwxyz", 6);
            Random rnd = new();
            await Task.Run(() =>
            {

                for (int i = 0; i < 100; i++) 
                {
                    Record.Add(rnd.Next(0,100));
                }
            }, cancellationToken);
        }
    }
}
