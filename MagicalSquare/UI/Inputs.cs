using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MagicalSquare.UI.Inputs
{
    public class EInput : TextBox
    {
        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register("PlaceHolder", typeof(string), typeof(EInput), new PropertyMetadata(""));
        public static readonly DependencyProperty PlaceHolderColorProperty = DependencyProperty.Register("PlaceHolderColor", typeof(SolidColorBrush), typeof(EInput), 
            new PropertyMetadata(new SolidColorBrush() { Color = Colors.Gray }));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(EInput), new PropertyMetadata(""));
        public static readonly DependencyProperty DescriptionColorProperty = DependencyProperty.Register("DescriptionColor", typeof(SolidColorBrush), typeof(EInput),
             new PropertyMetadata(new SolidColorBrush() { Color = Colors.Gray }));
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public SolidColorBrush PlaceHolderColor
        {
            get { return (SolidColorBrush)GetValue(PlaceHolderColorProperty); }
            set { SetValue(PlaceHolderColorProperty, value); }
        }
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public SolidColorBrush DescriptionColor
        {
            get { return (SolidColorBrush)GetValue(DescriptionColorProperty); }
            set { SetValue(DescriptionColorProperty, value); }
        }


        public EInput() { }
    }
}
