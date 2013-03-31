using BluffersDice.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace BluffersDice.Interface.CustomControls
{
    public class DieButton : Button
    {
      public Label ImageLabel { get; set; }

        public event EventHandler ValueChanged;
        private const string HELD_LABEL_TEXT = "Held";
        private TextBlock ValueLabel { get; set; }
        private TextBlock HoldLabel { get; set; }
        DependencyProperty P { get; set; }
        

        public DieButton():this(new Die())
        {
            
        }
        
        public DieButton(Die die)
        {



            die.DieValueChanged += DieValue_Changed;
            DataContext = die;

            ImageLabel = new Label();           
            HoldLabel  = new TextBlock()  { MinHeight = 15 };
            ValueLabel = new TextBlock();
            
            Panel = new StackPanel() { Orientation = Orientation.Vertical};

            Panel.Children.Add(ImageLabel);
            Panel.Children.Add(ValueLabel);
            Panel.Children.Add(HoldLabel);

            Content = Panel;
            
            Background = Brushes.Transparent;
            BorderBrush = new SolidColorBrush(Colors.Transparent);
            BorderThickness = new Thickness(6);


            
            
            ((Die)DataContext).IsHeldChanged += DieValue_IsHeldChanged;
            ((Die)DataContext).DieValueChanged += DieValue_Changed;

            ValueDidChange(DataContext, null);
        }

        




        private Image Image { get; set; }
        private StackPanel Panel { get; set; }

        public void UpdateButtonContent(object sender, EventArgs ea)
        {


            
            Dispatcher.Invoke(() => 
            {
                Die d = (Die)DataContext; 
           
            
                if (d.IsHeld)
                {
                    BorderBrush = new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    BorderBrush = new SolidColorBrush(Colors.Transparent);
                }


                HoldLabel.Text = ((Die)DataContext).IsHeld ? HELD_LABEL_TEXT : string.Empty;

                ImageLabel.Content = new Image() { Source = new BitmapImage(new Uri(String.Format("/res/dice/{0}.png", ((Die)DataContext).Value), UriKind.Relative)) };
            
                ValueLabel.Text = ((Die)DataContext).Value.ToString();

                Debug.WriteLine(String.Format("DieButton {0} Updated: {1}",this.Name, ((Die)DataContext).Value,"DieButton"),"DieButton");
           }); 
            
        
        }
        private void DieValue_IsHeldChanged(object sender, EventArgs e)
        {
            UpdateButtonContent(sender,e);
        }


        public void ValueDidChange(object sender, EventArgs ea)
        {
            if (ValueChanged != null)
                ValueChanged(this, null);
        }

        private void DieValue_Changed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() => UpdateButtonContent(sender, e));

        }

        

        
    }
}
