namespace Trollkarlens_Borg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player? player = null;
            do
            {
                player = Player.ChooseHero();
            }
            while (player == null);

            player.ChooseName();

            player.PresentPlayer();

            Game.Start();
            while (Game.IsRunning)
            {
                Board.ShowMap(player);

                player.FindPossibleDirections();
                player.Move();

                Room room = Room.ChooseRandom();
                room.Enter(player);
            }
        }
    }

    interface IAttack
    {
        void Attack();
    }

    interface IHealth
    {
        int Health { get; set; }

        void Heal();
        void TakeDamage();
    }

    static class Game
    {
        public static bool IsRunning
        {
            get { return _isRunning; }
        }

        private static bool _isRunning;

        public static void Start()
        {
            _isRunning = true;
        }

        public static void End()
        {
            Console.WriteLine("Game over.");
            _isRunning = false;
        }
    }

    abstract class Board
    {
        public const int Size = 7;

        public static void ShowMap(Player player)
        {
            Console.WriteLine();

            for (int i = Size; i > 0; i--)
            {
                Console.Write("|");

                for (int j = 1; j <= Size; j++)
                {
                    Console.Write(player.Position == (j, i) ? "¤" : " ");
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }
    }

    abstract class Room
    {
        public static Room ChooseRandom()
        {
            Random random = new Random();

            return random.Next(3) switch
            {
                0 => new TrapRoom(),
                1 => new EnemyRoom(),
                2 => new EmptyRoom()
            };
        }

        public abstract void Enter(Player player);
    }

    class EmptyRoom : Room
    {
        public override void Enter(Player player)
        {
            Console.WriteLine("\nDetta rummet är tomt.");
        }
    }

    class ChestRoom : Room
    {
        public override void Enter(Player player)
        {
            Console.WriteLine("\nDetta rummet har en kista.");
            PromptChestOpen(player);
        }

        private void PromptChestOpen(Player player)
        {
            while (true)
            {
                Console.WriteLine("\nVill du öppna kistan? 8 av 10 gånger är innehållet ofarligt.");
                Console.Write("J/n? ");
                ConsoleKey input = Console.ReadKey().Key;

                if (input == ConsoleKey.J)
                {
                    Random random = new Random();

                    switch (random.Next(1, 11))
                    {
                        case <= 2:
                            Console.WriteLine("\nDu hade otur och fick en giftig dryck.");
                            Console.WriteLine("-5 hälsa");
                            player.Health -= 5;
                            break;
                        case > 2:
                            Console.WriteLine("\nDu fick en helande dryck.");
                            Console.WriteLine("+5 hälsa");
                            player.Health += 5;
                            break;
                    }

                    return;
                }
                else if (input == ConsoleKey.N)
                {
                    return;
                }
            }
        }
    }

    class TrapRoom : Room
    {
        private Enemy _enemy;

        public override void Enter(Player player)
        {

        }
    }

    class EnemyRoom : Room
    {
        public override void Enter(Player player)
        {

        }
    }

    abstract class Player : IAttack, IHealth
    {
        public int Health
        {
            get { return _health; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine($"\n{_name} har dött.");
                    Game.End();
                }
                else
                {
                    _health = value;
                    Console.WriteLine($"\n{_name}s hälsa är nu {_health}");
                }
            }
        }

        public (int, int) Position
        {
            get { return _position; }
        }

        private int _health;
        private string? _name;
        private (int, int) _position;
        private Dictionary<int, string> _possibleDirections;

        public Player(int health)
        {
            _health = health;
            _name = null;
            _position = (1, 1);
            _possibleDirections = new Dictionary<int, string>();
        }

        public static Player? ChooseHero()
        {
            Console.WriteLine("Välj din hjälte:");
            Console.WriteLine("1. Eldhjälte");
            Console.WriteLine("2. Ishjälte");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice)) return null;

            return choice switch
            {
                1 => new FireHero(),
                2 => new IceHero(),
                _ => null
            };
        }

        public void ChooseName()
        {
            do
            {
                Console.Write("\nVälj ett namn för din karaktär: ");
                string? input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    _name = input;
                }
            }
            while (_name == null);
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

            Console.WriteLine($"\n{_name} kan gå:");
            foreach (int num in _possibleDirections.Keys)
            {
                string direction = _possibleDirections[num];
                Console.WriteLine($"{num}. {direction}");
            }
        }

        public void Move()
        {
            string? direction = null;
            do
            {
                Console.WriteLine("\nVart vill du gå?");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    _possibleDirections.TryGetValue(choice, out direction);
                }
            }
            while (direction == null);

            switch (direction)
            {
                case "Upp":
                    _position.Item2++;
                    break;
                case "Ned":
                    _position.Item2--;
                    break;
                case "Höger":
                    _position.Item1++;
                    break;
                case "Vänster":
                    _position.Item1--;
                    break;
            }

            Board.ShowMap(this);
        }

        public virtual void PresentPlayer()
        {
            Console.Write($"Din karaktär heter {_name} och är en ");
        }

        public abstract void Attack();

        public void Dodge()
        {

        }

        public void Heal()
        {

        }

        public void TakeDamage()
        {

        }
    }

    class FireHero : Player
    {
        public FireHero() : base(20) { }

        public override void PresentPlayer()
        {
            base.PresentPlayer();
            Console.WriteLine("eldhjälte");
        }

        public override void Attack()
        {

        }
    }

    class IceHero : Player
    {
        public IceHero() : base(25) { }

        public override void PresentPlayer()
        {
            base.PresentPlayer();
            Console.WriteLine("ishjälte");
        }

        public override void Attack()
        {

        }
    }

    class Enemy : IAttack, IHealth
    {
        public int Health { get; set; }

        public virtual void Attack()
        {

        }

        public void Heal()
        {

        }

        public void TakeDamage()
        {

        }
    }

    class Wizard : Enemy
    {

    }
}
