using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using MvvmGen;
using MvvmGen.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    [ViewModelGenerateFactory]
    [ViewModel]
    public partial class VM_Plot : IEventSubscriber<RowSelectedEvent>
    {
        [Property] private ObservableCollection<ISeries> plotData = [];
        [Property] private IList<ICartesianAxis> xAxes = [];
        [Property] private IList<ICartesianAxis> yAxes = [];

        public void OnEvent(RowSelectedEvent eventData)
        {
            //remove data from Plot
            if (!eventData.IsSelected)
            {
                var elmnt = PlotData.FirstOrDefault(p => p.SeriesId == eventData.Id);
                if (elmnt is not null)
                {
                    PlotData.Remove(elmnt);
                    //Workaround. Only Remove from the End of the list.
                    YAxes.RemoveAt(YAxes.Count - 1);
                }
            }
            else
            {
                if (PlotData.Count == 0)
                {
                    XAxes.Add(new Axis
                    {
                        LabelsRotation = 90,
                        Name = "Num of elements"
                    });
                }

                YAxes.Add(new Axis
                {
                    Name = eventData.Name,
                    NameTextSize = 12
                });

                //Füge das Element hinzu
                ISeries elmnt = new LineSeries<int>
                {
                    Values = eventData.Data,
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Name = eventData.Name,
                    ScalesYAt = YAxes.Count - 1
                };
                elmnt.SeriesId = eventData.Id;
                PlotData.Add(elmnt);
            }
        }
    }
}
