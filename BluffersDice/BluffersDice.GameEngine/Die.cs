using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BluffersDice.GameEngine
{
    [System.Diagnostics.DebuggerDisplay("Value: {Value} IsHeld: {IsHeld}")]
    public sealed class Die : IDie, INotifyPropertyChanged
    {
        private bool _IsHeld;
        private static readonly int _r_seed = new Random(((int)DateTime.Now.Millisecond) + new Random().Next(1, 100000)).Next();
        private static readonly Random _rng = new Random(_r_seed);
        private int _Value;

        public Die()
        {
            Value = 1;
            IsHeld = false;
        }

        public event EventHandler DieValueChanged;
        public event EventHandler IsHeldChanged;

        public int Id { get; set; }
        public bool IsHeld
        {
            get
            {
                return _IsHeld;
            }
            set
            {
                if (_IsHeld == value)
                {
                    return;
                }
                _IsHeld = value;
                if (IsHeldChanged == null)
                    return;

                IsHeldChanged(this, new EventArgs());
                if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("IsHeld"));
            }
        }
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {

                _Value = value;
                if (DieValueChanged == null)
                    return;
                
                DieValueChanged(this, new EventArgs());
                
                if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public void Roll()
        {
            var rolls = new List<int>();
            if (!IsHeld)
            {
                var d = 0;
                for (var i = 0; i < 50; i++)
                {
                    d = _rng.Next(1, 7);
                    Value = d;
                }
               

            }
        }
        public void ToggleHold()
        {
            IsHeld = !IsHeld;
        }

        public static Die operator ++(Die x) 
        {
            ++x.Value;
            if (x.Value > 6)
                x.Value = 1;
            return x;
        }
        public static Die operator --(Die x)
        {
            --x.Value;
            if (x.Value < 1)
                x.Value = 6;
            return x;
        }
        public static Die operator +(Die x, int i)
        {
            x.Value += i;
            return x;
        }
        public static Die operator -(Die x, int i)
        {
            x.Value -= i;
            return x;
        }



        public Die Clone()
        {

            int id = this.Id;
            int d_value = this.Value;
            bool isheld = this.IsHeld;

            Die clone = new Die() 
            { 
                Id = id, 
                IsHeld = isheld, 
                Value = d_value 
            };
            return clone;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


}
