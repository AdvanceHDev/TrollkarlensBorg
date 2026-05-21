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

            player.FindPossiblePaths();
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
        public const int boardSize = 7;
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
        private Dictionary<string, int> _possibleDirections;

        public Player()
        {
            Name = string.Empty;
            _position = (1, 1);
            _possibleDirections = new Dictionary<string, int>();
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

        public void FindPossiblePaths()
        {
            _possibleDirections.Clear();


            // Ändra 7 om spelplanen inte är 7x7
            if (_position.Item2 < Board.boardSize)
            {
                _possibleDirections.Add("Upp", _possibleDirections.Count + 1);
            }
            if (_position.Item2 > 1)
            {
                _possibleDirections.Add("Ned", _possibleDirections.Count + 1);
            }

            if (_position.Item1 < Board.boardSize)
            {
                _possibleDirections.Add("Höger", _possibleDirections.Count + 1);
            }
            if (_position.Item1 > 1)
            {
                _possibleDirections.Add("Vänster", _possibleDirections.Count + 1);
            }

            Console.WriteLine($"\n{Name} kan gå:");
            foreach (string direction in _possibleDirections.Keys)
            {
                int num = _possibleDirections.GetValueOrDefault(direction);
                Console.WriteLine($"{num}. {direction}");
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
