using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluffersDice.GameEngine
{
    public class Players
    {
        public Players()
        {
            Player1 = new Player() { Id = 1 };
            Player2 = new Player() { Id = 2 };
        }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
    }
}
