using System;

namespace TrollkarlensBorg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
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

    class Player : IHealth // Make abstract preferably
    {
        private string _name;
        private int _hp;
        private int _position;

        public Player(string name, int hp)
        {
            _name = name;
            _hp = hp;
            _position = 0; // May be changed
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
