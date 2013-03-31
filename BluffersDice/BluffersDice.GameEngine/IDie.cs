using System;
using System.Collections.Generic;
using System.Linq;


namespace BluffersDice.GameEngine
{
    public interface IDie
    {
        int Id { get; set; }
        bool IsHeld { get; set; }
        int Value { get; }

        void Roll();
    }
}
