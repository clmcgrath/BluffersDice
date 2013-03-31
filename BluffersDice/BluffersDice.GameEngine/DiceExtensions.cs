using System;
using System.Collections.Generic;
using System.Linq;

namespace BluffersDice.GameEngine
{
    public static class DiceExtensions
    {
        public static Die HoldToggle(this ICollection<Die> dice, int id)
        {
            var d_list = dice.ToList();
            Die dieToHold = d_list.Single(d => d.Id == id);
            if (dieToHold == null)
                throw new ArgumentException("id", String.Format("id {0} was not found", id));
            int index = d_list.IndexOf(dieToHold);
            d_list[index].ToggleHold();

            return d_list[index];

        }
    }
}
