using System;
using System.Linq;

namespace TrollkarlensBorg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Skriv siffra för att välja hjälte:\n" +
                "0: Eldhjälte\n" +
                "1: Ishjälte");

            if (Input.TryClean(Console.ReadLine(), out string input))
            {
                Input.IsValid(input, new[] { "0", "1" });
            }
        }
    }

    interface IHealth
    {
        void TakeDamage();
        void Attack();
    }

    static class Game // Make non-static?
    {
        private static Player _player;
        private static Board _board;

        public static void Start()
        {
            Board.ShowMap();
        }

        //public static void Run()
        //{

        //}

        //public static void CheckWin()
        //{

        //}
    }

    static class Input
    {
        public static bool TryClean(string input, out string clean)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                clean = string.Empty;
                return false;
            }

            clean = input.Trim();
            return true;
        }

        public static bool IsValid(string input, string[] validInputs)
        {
            return validInputs.Contains<string>(input);
        }
    }

    class Player : IHealth // Make abstract preferably
    {
        private string _name;
        private int _hp = 10; // Not set in stone
        private int _position;

        public Player(string name)
        {
            _name = name;
            _position = 0; // May be changed
        }

        public static Player ChooseHero()
        {

        }

        public void Move()
        {

        }

        public void TakeDamage()
        {
            Console.WriteLine($"{_name} took damage! Health is now {_hp}.");
        }

        public void Attack()
        {

        }
    }

    class FireHero : Player
    {
        public FireHero(string name) : base(name) { }
    }

    class IceHero : Player
    {
        public IceHero(string name) : base(name) { }
    }

    abstract class Board // Could be static or abstract?
    {
        private Room[] _rooms;

        public static void ShowMap()
        {

        }

        //public static void GetRoom()
        //{

        //}
    }

    abstract class Room
    {
        private string _description;

        // Kanske inte behöver implementeras olika för varje rum
        public virtual void Enter() { } // Mark as abstract?
    }

    class TrapRoom : Room
    {
        public override void Enter()
        {

        }
    }

    class EnemyRoom : Room
    {
        public override void Enter()
        {

        }
    }

    class TreasureRoom : Room
    {
        public override void Enter()
        {

        }
    }

    abstract class Enemy : IHealth
    {
        private string _name;
        private int _hp;

        public void TakeDamage() // Make abstract?
        {

        }

        public abstract void Attack();
    }

    class BossEnemy : Enemy
    {
        public override void Attack()
        {

        }
    }
}
