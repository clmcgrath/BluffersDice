using BluffersDice.GameEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BluffersDice.Interface.CustomControls
{
    /// <summary>
    /// Interaction logic for DieButton2.xaml
    /// </summary>
    public partial class DieButton2 : UserControl
    {

        public Image DieImage { get; set; }
        public Die DieContext
        {
            get { return (Die)DataContext; }
            set
            {
                if (DataContext == value)
                    return;
                DataContext = value;
            }
        }
        public DieButton2()
        {
            InitializeComponent();
            DataContextChanged += DieButton2_DataContextChanged;
            DataContext = new Die();

        }

        private void DieButton2_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }


        
    }
}
