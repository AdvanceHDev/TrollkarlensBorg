namespace Trollkarlens_Borg
{
    /* Funktioner att lägga till:
     * Spara
     * Automatisk sparning om man trycker Ctrl + C (Console.CancelKeyPress)
    */

    internal class Program
    {
        static void Main(string[] args)
        {
            Player? player = null;
            do
            {
                Console.WriteLine("Välj din hjälte:");
                Console.WriteLine("1. Eldhjälte");
                Console.WriteLine("2. Ishjälte");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int choice)) continue;

                player = Player.ChooseHero(choice);
            }
            while (player == null);

            do
            {
                Console.Write("Välj ett namn för din karaktär: ");
                string? input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input)) player.Name = input;
            }
            while (player.Name == string.Empty);

            Console.Write($"Din karaktär är en ");
            Console.Write(player is FireHero ? "eldhjälte" : "ishjälte");
            Console.WriteLine($" och heter {player.Name}.");
        }
    }

    abstract class Player
    {
        public string Name = string.Empty;

        public static Player? ChooseHero(int choice)
        {
            return choice switch
            {
                1 => new FireHero(),
                2 => new IceHero(),
                _ => null
            };
        }
    }

    class FireHero : Player
    {
        private int health = 20;
    }

    class IceHero : Player
    {
        private int health = 20;
    }
}
