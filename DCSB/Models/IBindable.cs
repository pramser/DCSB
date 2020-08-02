using ps.Utils;
using System.Collections.ObjectModel;

namespace ps.Models
{
    public interface IBindable
    {
        ObservableCollection<VKey> Keys { get; set; }
    }
}
