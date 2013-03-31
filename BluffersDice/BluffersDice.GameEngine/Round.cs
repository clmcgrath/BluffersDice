using System;
using System.Collections.Generic;
using System.Linq;


namespace BluffersDice.GameEngine
{
    public class Round
    {
        public Round()
        {
            Turns = new List<Turn>();
        }

        public List<Turn> Turns { get; set; }
    }
}
