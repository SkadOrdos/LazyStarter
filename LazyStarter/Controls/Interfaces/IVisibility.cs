using System.Windows;

namespace LazyStarter
{
    /// <summary>
    /// Доступ к видимости
    /// </summary>
    public interface IVisibility
    {
        /// <summary>
        /// Видимость элемента
        /// </summary>
        Visibility Visibility { get; set; }
    }
}
