using System;

namespace MyGameSnake.AuxiliaryClasses
{
    public static class Handlers
    {
        public static void PlayerWon(object sender, EventArgs args)
        {
            Console.WriteLine("Player won this game");
        }

        public static void PlayerHookedMyself(object sender, EventArgs args)
        {
            Console.WriteLine("You lost because you hooked yourself");
        }
        
        public static void PlayerHookedWall(object sender, EventArgs args)
        {
            Console.WriteLine("You lost because you hooked wall");
        }
    }
}