using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LazyStarter
{
    /// <summary>
    /// Обертка для сериализации простого биндинга
    /// </summary>
    [Serializable]
    public class SimpleBinding
    {
        public String Path { get; set; }

        public BindingMode Mode { get; set; }

        public String ElementName { get; set; }

        public String StringFormat { get; set; }

        public Boolean IsAsync { get; set; }


        public SimpleBinding() { }

        public SimpleBinding(String path, BindingMode mode = BindingMode.Default)
        {
            Path = path;
            Mode = mode;
        }
        
        public Binding ToBinding()
        {
            Binding binding = null;
            if (!String.IsNullOrEmpty(Path))
                binding = new Binding(Path)
                {
                    Mode = this.Mode,
                    ElementName = this.ElementName,
                    IsAsync = this.IsAsync,
                    StringFormat = this.StringFormat
                };
            return binding;
        }
    }
}