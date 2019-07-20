using System.Windows.Controls;

namespace LazyStarter
{
    /// <summary>
    /// Базовый интерфейс для элементов меню/тулбаров
    /// </summary>
    public interface IMenuItem : IVisibility
    {
        /// <summary>
        /// Необходимо для автосортировки меню
        /// </summary>
        int SortId { get; }
        
        bool IsSeparator { get; }

        bool Enabled { get; set; }

        string Text { get; set; }

        Image Icon { get; set; }

        object Tag { get; set; }
}
}
