using BluffersDice.GameEngine;
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
using System.Windows.Shapes;
using BluffersDice.Interface.CustomControls;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BluffersDice.Interface
{
    class DiePanelItem : StackPanel
    {
        public DiePanelItem(Roll roll)
        {

            MaxHeight = 65;

            var l_dice = roll.Dice.OrderBy(o => o.Value).ToList();

            if (roll.Dice.Count != 5)
            {
                throw new ArgumentException("dice count must be 5");
            }

            Orientation = Orientation.Horizontal;



            Die1 = new Image()
            {
                Source = new BitmapImage(new Uri(String.Format("/res/dice/{0}.png", l_dice[0].Value), UriKind.Relative)),
                Margin = new Thickness(5)
            };




            Die2 = new Image()
            {
                Source = new BitmapImage(new Uri(String.Format("/res/dice/{0}.png", l_dice[1].Value), UriKind.Relative)),
                Margin = new Thickness(5)
            };

            Die3 = new Image()
            {
                Source = new BitmapImage(new Uri(String.Format("/res/dice/{0}.png", l_dice[2].Value), UriKind.Relative)),
                Margin = new Thickness(5)
            };

            Die4 = new Image()
            {
                Source = new BitmapImage(new Uri(String.Format("/res/dice/{0}.png", l_dice[3].Value), UriKind.Relative)),
                Margin = new Thickness(5)
            };

            Die5 = new Image()
            {
                Source = new BitmapImage(new Uri(String.Format("/res/dice/{0}.png", l_dice[4].Value), UriKind.Relative)),
                Margin = new Thickness(5)
            };

            Children.Add(Die1);
            Children.Add(Die2);
            Children.Add(Die3);
            Children.Add(Die4);
            Children.Add(Die5);





        }


        private Image Die1 { get; set; }
        private Image Die2 { get; set; }
        private Image Die3 { get; set; }
        private Image Die4 { get; set; }
        public Image Die5 { get; set; }

    }
}
