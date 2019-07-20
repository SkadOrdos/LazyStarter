using System.Collections;
using System.Windows.Controls;

namespace LazyStarter
{
    public interface ICheckedListItem
    {
        bool Checked { get; set; }

        bool IsHighLighted { get; set; }

        bool Selected { get; set; }

        IList Container { get; set; }
    }
}
