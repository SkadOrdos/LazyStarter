using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter
{
    /// <summary>
    /// Интерфейс для доступа к элементам подменю
    /// </summary>
    /// <typeparam name="T">Тип элементов коллекции</typeparam>
    public interface ISubMenu<T> : IMenuItem
    {
        bool HasChildren { get; }

        ObservableCollection<T> Items { get; set; }
    }
}
