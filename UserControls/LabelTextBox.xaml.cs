using AmoSim2.ViewModel;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmoSim2.UserControls
{
    /// <summary>
    /// Interaction logic for LabelTextBox.xaml
    /// </summary>
    public partial class LabelTextBox : UserControl
    {
        public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(
            name: nameof(Label),
            propertyType: typeof(string),
            ownerType: typeof(LabelTextBox));

        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            name: nameof(Text),
            propertyType: typeof(string),
            ownerType: typeof(LabelTextBox),
            new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public static readonly DependencyProperty MyImageSourceProperty =
    DependencyProperty.Register("ImgSource",
        typeof(string), typeof(LabelTextBox));

        public string ImgSource
        {
            get { return (string)GetValue(MyImageSourceProperty); }
            set { SetValue(MyImageSourceProperty, value); }
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public LabelTextBox()
        {
            InitializeComponent();
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            BindingExpression be = tb.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }
    }
}