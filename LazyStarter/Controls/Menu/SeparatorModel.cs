using System;

namespace LazyStarter
{
    [Serializable]
    public class SeparatorModel : BaseMenuItemModel
    {
        public SeparatorModel() : this(0)
        { }

        public SeparatorModel(int sortId) : base(sortId)
        { }

        public override bool IsSeparator
        {
            get { return true; }
        }
    }
}