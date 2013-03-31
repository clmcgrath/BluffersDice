using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace BluffersDice.GameEngine
{
    [System.Diagnostics.DebuggerDisplay("Player {Id}")]
    public class Player 
    {
        private int points;
        

        public Player()
        {
            PointsChanged += PointsChangedEventHandler;
        }

        public event EventHandler PointsChanged;
        public event EventHandler PointsReachedZero;

        public int Id { get; set; }
        public int Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
                PointsChanged(this, new PropertyChangedEventArgs("Points"));
            }
        }

        private void PointsChangedEventHandler(object sender, EventArgs e)
        {
            if ((sender as Player).Points < 1 && PointsReachedZero != null)
            {
                PointsReachedZero(this, new EventArgs());
            }
        }
        public Turn LastTurn { get; set; }
        public override string ToString()
        {
            return string.Format("Player {0}", Id);
        }

        public Player Clone()
        {
            return new Player() { Id = this.Id, Points = this.Points };
        }
    }
}
