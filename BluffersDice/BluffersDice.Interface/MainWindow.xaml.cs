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
using BluffersDice.GameEngine;
using System.Collections.ObjectModel;
using BluffersDice.Interface.Properties;
namespace BluffersDice.Interface
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        const string ButtonTextFormat = "Start Turn";
        public Round CurrentRound { get; set; }
        public ObservableCollection<Round> CompletedRounds { get; set; }
        public static bool ClaimIsActual 
        {
            get 
            {
                return GameState.Challenger.LastTurn.Claim == GameState.Challenger.LastTurn.Actual;
        
            } 
        }


        public MainWindow()
        {
            InitializeComponent();

            
            GameState.Players = new Players();
            GameState.Players.Player1.PointsChanged += PlayerPointsChanged;
            GameState.Players.Player2.PointsChanged += PlayerPointsChanged;
            GameState.Players.Player1.PointsReachedZero += Player_PointsReachedZero;
            GameState.Players.Player2.PointsReachedZero  += Player_PointsReachedZero;
            GameState.Players.Player1.Points = Settings.Default.PlayerStartPoints;
            GameState.Players.Player2.Points = Settings.Default.PlayerStartPoints;
            
            GameState.ActivePlayerChanged += GameState_ActivePlayerChanged;
            GameState.RoundChanged += GameState_RoundChanged;
            GameState.StartNewRound();
            CurrentRound = GameState.CurrentRound;
            //GameState_ActivePlayerChanged(null, null);
            btn_lbl_TakeTurnButtonText.Content = string.Format("Player {0}'s Turn", GameState.Caller.Id);
            lbl_ClaimValue.Text = " ";
            vbox_ClaimControl.Visibility = System.Windows.Visibility.Hidden;
            vbox_GameOver.Visibility = System.Windows.Visibility.Hidden;   
        }

        private void Player_PointsReachedZero(object sender, EventArgs e)
        {
            vbox_ClaimControl.Visibility = System.Windows.Visibility.Hidden;
            var player = (Player)sender;
            Player winner = player.Id == 1 ? GameState.Players.Player2 : GameState.Players.Player1;
            lbl_WinnerText.Text = winner.ToString(); 
            vbox_GameOver.Visibility = System.Windows.Visibility.Visible;
            btn_StartTurn.IsEnabled = false;

        }

        void GameState_RoundChanged(object sender, EventArgs e)
        {
            lbl_RoundNumber.Text = string.Format("Round {0}", GameState.RoundNumber);
        }

        void GameState_ActivePlayerChanged(object sender, EventArgs e)
        {
            btn_lbl_TakeTurnButtonText.Content = string.Format("Player {0}'s Turn", GameState.Caller.Id);
        }

        private void PlayerPointsChanged(object sender, EventArgs e)
        {
            var p = sender as Player;

            if (p.Id == 1)
            {
                lbl_Player1Score.Text = p.Points.ToString();

            }
            else if (p.Id == 2)
            {
                lbl_Player2Score.Text = p.Points.ToString();
            }

            
        }
        
        private void btn_StartTurn_Click(object sender, RoutedEventArgs e)
        {
            Turn turn = new Turn();
            PlayerTurn ui = new PlayerTurn(ref turn);
            
            ui.ShowDialog();

            GameState.Caller.LastTurn = turn;
            CurrentRound.Turns.Add(turn);

            GameState.ChangeActivePlayer();
            pnl_ClaimDice.Children.Clear();
            pnl_ClaimDice.Children.Add( new DiePanelItem(turn.Claim));
            lbl_ClaimValue.Text = turn.Claim.ToString();
            vbox_ClaimControl.Visibility = System.Windows.Visibility.Visible;
            GameState_RoundChanged(null, null);
        }

        private void btn_ChallengeClaim_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimIsActual)
            {
                MessageBox.Show("Challenge Failed");
                GameState.Caller.Points--;
                
            }
            else
            {
                MessageBox.Show("Challenge Passed");
                GameState.Challenger.Points--;
                
            }

            GameState.StartNewRound();
            vbox_ClaimControl.Visibility = System.Windows.Visibility.Hidden;
        }


        








    }
}
