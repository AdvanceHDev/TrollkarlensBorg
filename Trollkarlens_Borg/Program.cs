namespace Trollkarlens_Borg
{
    /* Funktioner att lägga till:
     * Sparning
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

                if (int.TryParse(input, out int choice))
                {
                    player = Player.ChooseHero(choice);
                }
            }
            while (player == null);
            
            while (true)
            {
                Console.Write("Välj ett namn för din karaktär: ");
                string? input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    player.Name = input;
                    break;
                }
            }

            player.PresentPlayer();

            // Game starts here?
            Game.ChooseRoomType();
        }
    }

    interface IAttack
    {
        void Attack();
        void TakeDamage();
    }

    static class Game
    {
        public static Room ChooseRoomType()
        {
            Random random = new Random();

            return random.Next(2) switch
            {
                0 => new TrapRoom(),
                1 => new EnemyRoom()
            };
        }
    }

    static class Board
    {
        
    }

    abstract class Room
    {
        
    }

    class TrapRoom : Room
    {

    }

    class EnemyRoom : Room
    {

    }

    abstract class Player : IAttack
    {
        public string Name { get; set; }

        private (int, int) _position;
        private Dictionary<string, int> possiblePaths;

        public Player()
        {
            Name = string.Empty;
            _position = (0, 0);
            possiblePaths = new Dictionary<string, int>();
        }

        public static Player? ChooseHero(int choice)
        {
            return choice switch
            {
                1 => new FireHero(),
                2 => new IceHero(),
                _ => null
            };
        }

        public void PrintPossiblePaths()
        {
            Console.WriteLine($"{Name} kan gå:");

            // Ändra 7 om spelplanen inte är 7x7
            if (_position.Item1 < 7)
            {
                Console.WriteLine("Upp");
                possiblePaths.Add("Upp", possiblePaths.Count + 1); // Fortsätt härifrån
            }
            if (_position.Item1 > 0)
            {
                Console.WriteLine("Ned");
            }

            if (_position.Item2 < 7)
            {
                Console.WriteLine("Höger");
            }
            if (_position.Item2 > 0)
            {
                Console.WriteLine("Vänster");
            }
        }

        public abstract void PresentPlayer();

        public abstract void Attack();
        public abstract void TakeDamage();
    }

    class FireHero : Player
    {
        private int health = 20;

        public override void PresentPlayer()
        {
            Console.WriteLine($"Din karaktär heter {Name} och är en eldhjälte.");
        }

        public override void Attack()
        {
            
        }

        public override void TakeDamage()
        {
            
        }
    }

    class IceHero : Player
    {
        private int health = 20;

        public override void PresentPlayer()
        {
            Console.WriteLine($"Din karaktär heter {Name} och är en ishjälte.");
        }

        public override void Attack()
        {
            
        }

        public override void TakeDamage()
        {
            
        }
    }
}
