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
using BluffersDice.Interface.Properties;

namespace BluffersDice.Interface
{
    /// <summary>
    /// Interaction logic for UserTurn.xaml
    /// </summary>
    /// 
    public partial class PlayerTurn : Window
    {
        public Turn CurrentTurn 
        {
            get { return (Turn)DataContext; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException();
                DataContext = value; 
            }
        }
        public static int MaxAllowedRolls 
        {
            get { return Settings.Default.MaxRollsPerTurn; } 
        }

        public Roll CurrentRoll { get; set; }
        private ObservableCollection<Roll> Rolls { get; set; }
        
        
        public bool BeforeFirstRoll { get; set; }

        public PlayerTurn(ref Turn turn)
        {
            
            BeforeFirstRoll = true;

            CurrentTurn = turn;

            CurrentRoll = turn.Roll;

            Rolls = new ObservableCollection<Roll>(new List<Roll>());            
            Rolls.CollectionChanged += Rolls_CollectionChanged;
            
            InitializeComponent();

            //Icon = new BitmapImage(new Uri(String.Format("/res/dice/{0}.png", CurrentTurn.CurrentPlayer.Id), UriKind.RelativeOrAbsolute));

            lbl_PlayerTitle.Text = string.Format(lbl_PlayerTitle.Text, GameState.Caller.Id);
            lbl_RollValue.Text = " ";
            InitDiceButtons();
            HideDiceButtons();
            DisableDiceButtons();
            Closing += PlayerTurn_Closing;
            btn_AcceptRoll.IsEnabled = false;
            if (CurrentTurn.Challenger.LastTurn != null)
            {
                vbox_RollToBeat.Visibility = System.Windows.Visibility.Visible;
                pnl_RollToBeat.Children.Add(new DiePanelItem(CurrentTurn.Challenger.LastTurn.Claim));
            }
            else
            {
                vbox_RollToBeat.Visibility = System.Windows.Visibility.Hidden;
            }
            pnl_BluffButtons.Visibility = System.Windows.Visibility.Hidden;
            btn_Bluff.IsEnabled = false;
        }

        void PlayerTurn_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            
        }



        public void InitDiceButtons()
        {
            SetupDieButton(btn_Die1, CurrentRoll.Dice[0]);
            SetupDieButton(btn_Die2, CurrentRoll.Dice[1]);
            SetupDieButton(btn_Die3, CurrentRoll.Dice[2]);
            SetupDieButton(btn_Die4, CurrentRoll.Dice[3]);
            SetupDieButton(btn_Die5, CurrentRoll.Dice[4]);
        }
        private void Rolls_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<Roll> dice = e.NewItems.Cast<Roll>().ToList();
           
              dice.ForEach(i=>stk_RollsDisplay.Children.Add(new DiePanelItem(i)));

              lbl_RollValue.Text = CurrentRoll.ToString();
            
        }

        public void DieClick(object sender, EventArgs ea)
        {
            ((Die)((DieButton)sender).DataContext).ToggleHold();
        }
        public void SetupDieButton(DieButton btn, Die die)
        {
            btn.DataContext = die;

            
            //add die handlers 
            die.DieValueChanged += btn.UpdateButtonContent;
            die.PropertyChanged += btn.UpdateButtonContent;

            //add button handlers
            btn.Click += DieClick;
            btn.Click += btn.UpdateButtonContent;
            
        }

        private void RollDice_btnClick(object sender, RoutedEventArgs e)
        {
            
            var roll = CurrentRoll.RollDice();
            var roll_clone = CurrentRoll.Clone();
            
            Rolls.Add(CurrentRoll.Clone());
            BeforeFirstRoll = false;

            if (BeforeFirstRoll)
                return;

            EnableDiceButtons();
            UnhideDiceButtons();

            CurrentTurn.Actual = roll_clone;
            btn_AcceptRoll.IsEnabled = RollIsBetterThanChallenger;
            if (Rolls.Count < MaxAllowedRolls)
                return;
            

            btn_RollDice.IsEnabled = false;
            btn_Bluff.IsEnabled = true;
            DisableDiceButtons();
        
        }


        public void HideDiceButtons()
        {
            btn_Die1.Visibility = Visibility.Hidden;
            btn_Die2.Visibility = Visibility.Hidden;
            btn_Die3.Visibility = Visibility.Hidden;
            btn_Die4.Visibility = Visibility.Hidden;
            btn_Die5.Visibility = Visibility.Hidden;
        }

        public void UnhideDiceButtons()
        {
            btn_Die1.Visibility = Visibility.Visible;
            btn_Die2.Visibility = Visibility.Visible;
            btn_Die3.Visibility = Visibility.Visible;
            btn_Die4.Visibility = Visibility.Visible;
            btn_Die5.Visibility = Visibility.Visible;
        }
        public void EnableDiceButtons()
        {
            btn_Die1.IsEnabled = true;
            btn_Die2.IsEnabled = true;
            btn_Die3.IsEnabled = true;
            btn_Die4.IsEnabled = true;
            btn_Die5.IsEnabled = true;
        }

        public void DisableDiceButtons()
        {
            btn_Die1.IsEnabled = false;
            btn_Die2.IsEnabled = false;
            btn_Die3.IsEnabled = false;
            btn_Die4.IsEnabled = false;
            btn_Die5.IsEnabled = false;
        }

        private void btn_AcceptRoll_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTurn.Challenger.LastTurn != null)
            {
              //check roll is valid 
            }
            if (CurrentTurn.Claim == null)
            {
                CurrentTurn.Claim = CurrentTurn.Actual;
            }

            CurrentTurn.CurrentPlayer.LastTurn = CurrentTurn;
            Closing -= PlayerTurn_Closing;
            Close();
        }

        

        private void btn_Bluff_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTurn.Claim == null)
            {
                CurrentTurn.Claim = new Roll();
                BluffButtonsSetup();
                pnl_BluffButtons.Visibility = System.Windows.Visibility.Visible;
                lbl_BluffButtonText.Content = "Cancel Bluff";
                btn_AcceptRoll.IsEnabled = RollIsBetterThanChallenger;
            }
            else
            {
                lbl_BluffButtonText.Content = "Bluff";
                CurrentTurn.Claim = null;
                pnl_BluffButtons.Visibility = System.Windows.Visibility.Hidden;
                btn_AcceptRoll.IsEnabled = RollIsBetterThanChallenger;
            }
        }

        public bool RollIsBetterThanChallenger
        {
            get
            {
                if (BeforeFirstRoll)
                    return false;

                Roll roll = CurrentTurn.Claim == null ? CurrentTurn.Actual : CurrentTurn.Claim;


                if (CurrentTurn.Challenger.LastTurn == null)
                    return true;
                if (roll.BestHand.Type < CurrentTurn.Challenger.LastTurn.ClaimedHandValue.Type)
                    return false;
                if (roll.BestHand.Type == CurrentTurn.Challenger.LastTurn.ClaimedHandValue.Type 
                      &&  roll.DiceSum <= CurrentTurn.Challenger.LastTurn.Claim.DiceSum)
                    return false;

                return true;
            }
        }


        public void BluffButtonsSetup()
        {
            BluffButtonSetup(btn_BluffDie1, CurrentTurn.Claim.Dice[0]);
            BluffButtonSetup(btn_BluffDie2, CurrentTurn.Claim.Dice[1]);
            BluffButtonSetup(btn_BluffDie3, CurrentTurn.Claim.Dice[2]);
            BluffButtonSetup(btn_BluffDie4, CurrentTurn.Claim.Dice[3]);
            BluffButtonSetup(btn_BluffDie5, CurrentTurn.Claim.Dice[4]);
            lbl_BluffValue.Text = CurrentTurn.Claim.ToString();
        }
        public void BluffButtonSetup(DieButton btn, Die die)
        {
            btn.DataContext = die;
            btn.Click += btn_BluffDieButton_Click;
            btn.UpdateButtonContent(null, null);

        }

        void btn_BluffDieButton_Click(object sender, RoutedEventArgs e)
        {
            
            var btn = ((DieButton)sender);
            var diesrc = ((Die)btn.DataContext);
            diesrc++;
            btn.UpdateButtonContent(this, e);
            btn_AcceptRoll.IsEnabled = RollIsBetterThanChallenger;
            lbl_BluffValue.Text = CurrentTurn.Claim.ToString();
        
        }




        //private void CurrentTurnCompleted(object sender, EventArgs ea)
        //{
            
        //}
        
        
    }
}
