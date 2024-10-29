using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
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

        // [EDITED] INITIALIZE THE XAXES AND YAXES WITH AT LEAST ONE ELEMENT
        [Property] private IList<ICartesianAxis> xAxes = [new Axis()];
        [Property] private IList<ICartesianAxis> yAxes = [new Axis()];

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

                    // [EDITED] LETS HANDLE THIS ON THE SYNCAXES METHOD
                    //YAxes.RemoveAt(YAxes.Count - 1);
                }

                // [EDITED] WHY IS THIS NECESSARY?
                //if (PlotData.Count == 0)
                //{
                //    XAxes.Clear();
                //}
            }
            else
            {
                if (PlotData.Count == 0)
                {
                    // [EDITED] INSTEAD OF ADDING, LETS REPLACE THE INSTANCE
                    XAxes = [
                        new Axis
                        {
                            LabelsRotation = 90,
                            Name = "Num of elements"
                        }
                    ];
                }

                // [EDITED] lets better handle this on the SynxAxes method
                //YAxes.Add(new Axis
                //{
                //    Name = eventData.Name,
                //    NameTextSize = 12
                //});

                //Füge das Element hinzu
                ISeries elmnt = new LineSeries<int>
                {
                    Values = eventData.Data,
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Name = eventData.Name,
                    // [EDITED] 
                    //ScalesYAt = YAxes.Count - 1 this will be also handled on the SyncAxes method
                };
                elmnt.SeriesId = eventData.Id;
                PlotData.Add(elmnt);
            }

            SyncAxes();
        }

        // THIS METHOD ENSURES THAT EACH SERIES HAS ITS OWN YAXIS
        private void SyncAxes()
        {
            YAxes = PlotData.Select(x => new Axis
            {
                Name = x.Name,
                NameTextSize = 12,
            }).ToArray();

            // ENSURE AT LEAST ONE ELEMENT IS PRESENT IN THE COLLECTION.
            if (YAxes.Count == 0) YAxes = [new Axis()];

            for (int i = 0; i < PlotData.Count; i++)
            {
                // THIS CAST SEEMS CONFUSING, I THINK AN EXAMPLE BASED ON THIS 
                // CASE IS NECESSARY IN THE DOCUMENTATION, I THINK THIS SHOULD BE EASIER
                // OR AT LEAST DOCUMENTED
                var cartesianSeries = (ICartesianSeries<SkiaSharpDrawingContext>)PlotData[i];
                cartesianSeries.ScalesYAt = i;
            }
        }
    }
}
