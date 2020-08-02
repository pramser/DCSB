using System.Windows;
using System.Windows.Controls;

namespace ps.Controls
{
    public class RadioButtonList : ListBox
    {
        static RadioButtonList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioButtonList), new FrameworkPropertyMetadata(typeof(RadioButtonList)));
        }
    }
}
