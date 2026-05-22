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

            // Loop starts here?
            player.FindPossibleDirections();

            while (true)
            {
                Console.WriteLine("\nVart vill du gå?");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int choice)) continue;

                if (player.PossibleDirections.ContainsKey(choice))
                {
                    player.Move(choice);
                    break;
                }
            }

            Room room = Room.ChooseRandom();
            room.Enter();
        }
    }

    interface IAttack
    {
        void Attack();
        void Dodge();
    }

    interface IHealth
    {
        int Health { get; set; }

        void Heal();
        void TakeDamage();
    }

    static class Game
    {

    }

    abstract class Board
    {
        public const int Size = 7;
    }

    abstract class Room
    {
        private string _description;

        public Room(string description)
        {

        }

        public static Room ChooseRandom()
        {
            Random random = new Random();

            return random.Next(2) switch
            {
                0 => new TrapRoom(),
                1 => new EnemyRoom()
            };
        }

        public void Enter()
        {

        }
    }

    class TrapRoom : Room
    {

    }

    class EnemyRoom : Room
    {

    }

    abstract class Player : IAttack, IHealth
    {
        public string Name { get; set; }

        public Dictionary<int, string> PossibleDirections
        {
            get { return _possibleDirections; }
        }

        private (int, int) _position;
        private Dictionary<int, string> _possibleDirections;

        public Player()
        {
            Name = string.Empty;
            _position = (1, 1);
            _possibleDirections = new Dictionary<int, string>();
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

        public void FindPossibleDirections()
        {
            _possibleDirections.Clear();

            if (_position.Item2 < Board.Size)
            {
                _possibleDirections.Add(_possibleDirections.Count + 1, "Upp");
            }
            if (_position.Item2 > 1)
            {
                _possibleDirections.Add(_possibleDirections.Count + 1, "Ned");
            }

            if (_position.Item1 < Board.Size)
            {
                _possibleDirections.Add(_possibleDirections.Count + 1, "Höger");
            }
            if (_position.Item1 > 1)
            {
                _possibleDirections.Add(_possibleDirections.Count + 1, "Vänster");
            }

            Console.WriteLine($"\n{Name} kan gå:");
            foreach (int num in _possibleDirections.Keys)
            {
                string direction = _possibleDirections[num];
                Console.WriteLine($"{num}. {direction}");
            }
        }

        public void Move(int direction)
        {

        }

        public abstract void PresentPlayer();

        public abstract void Attack();
        public abstract void Dodge();

        public abstract void TakeDamage();
    }

    class FireHero : Player
    {
        private int _health = 20;

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
        private int _health = 20;

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

    class Enemy
    {

    }
}
