using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter
{
    public class VcUtils
    {
        public static MenuItemModel FindMenuItem(string commandId, object menu)
        {
            return FindMenuItem(commandId, (ICollection<MenuItemModel>)menu);
        }

        public static MenuItemModel FindMenuItem(string commandId, ICollection<MenuItemModel> menu)
        {
            return menu.FirstOrDefault(mi => mi.CommandId == commandId);
        }

        public static void LocalizeMenu(ICollection<MenuItemModel> menu, string locale)
        {
            if (menu == null) return;

            foreach (var item in menu)
            {
                item.Text = LocalePlugin.GetString(item.CommandId, locale);
                if (item.Items != null && item.Items.Any())
                    LocalizeMenu((ICollection<MenuItemModel>)item.Items, locale);
            }
        }
    }
}
