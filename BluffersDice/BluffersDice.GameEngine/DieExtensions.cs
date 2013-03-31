using System;
using System.Collections.Generic;
using System.Linq;

namespace BluffersDice.GameEngine
{
    public static class DieExtensions
    {
        public static Die ToggleHold(this ICollection<Die> dice, int id)
        {
            var d_list = dice.ToList();
            try
            {
                var index = d_list.IndexOf(dice.Where(d => d.Id == id).Single());
                d_list[index].ToggleHold();
                return d_list[index];
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentException("id", String.Format("Die id {0} does not exist", id));
            }
        }
    }
}
