using MahApps.Metro.Controls;
using System.Collections.Generic;

namespace DonePadClient.View
{
    public class ViewBase
    {
        public class ViewInfo
        {
            public MetroWindow MetroWindow { get; set; }
            public string Key { get; set; }
        }

        public static List<ViewInfo> ViewList { get; set; } = new List<ViewInfo>();

        public static void RegisterView(string key, MetroWindow window)
        {
            ViewList.Add(new ViewInfo
            {
                Key = key,
                MetroWindow = window
            });
        }

        public static void ShowView(string key)
        {
            foreach (var view in ViewList)
            {
                if (view.Key == key)
                    view.MetroWindow.ShowDialog();
            }
        }

        public static void CloseView(string key)
        {
            foreach (var view in ViewList)
            {
                if (view.Key == key)
                    view.MetroWindow.Close();
            }
        }
    }
}