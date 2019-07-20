using System;
using System.Windows;
using System.Windows.Input;

namespace LazyStarter
{
    public class MouseOverItemArgs : MouseEventArgs
    {
        public readonly FrameworkElement Element;
        public readonly Object ElementContext;

        public MouseOverItemArgs(FrameworkElement item, object context, MouseDevice mouse, int timestamp, StylusDevice stylusDevice) : base(mouse, timestamp, stylusDevice)
        {
            Element = item;
            ElementContext = context;
        }
    }
}
