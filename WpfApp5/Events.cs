using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    public record RowSelectedEvent(IList<int> Data, string Name, int Id, bool IsSelected);
}
