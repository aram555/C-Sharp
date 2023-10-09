using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTraining.Soldier_Class
{
    public class Soldier : ISoldier, IWeapon
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        public int Exp { get; set; }
        public int HP { get; set; }
        public int Company { get; set; }
        public string Weapon { get; set; }
        public int Damage { get; set; }

        public void GetDamage(Soldier _soldier, int _damage, string _weapon)
        {
            if (_soldier.HP > 0)
            {
                _soldier.HP -= _damage;
                if (_soldier.HP <= 0) Die(_soldier);
            }
            else
                Die(_soldier);
        }

        public void Shoot(Soldier Enemy, int randomShot)
        {
            randomShot += Exp / 5;
            if (randomShot >= 50)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{Rank} {Name} Hit the {Enemy.Name} by {Weapon} : {randomShot}");
                GetDamage(Enemy, Damage, Weapon);

                Exp += 10;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Rank} {Name} Dont hit {Enemy.Name} by the {Weapon} : {randomShot}");
            }
        }

        public void Die(Soldier soldier)
        {
            if (Soldiers.soldiers.Contains(soldier))
            {
                Soldiers.soldiers.Remove(soldier);
                Soldiers.diedSoldiers.Add(soldier);
            }
            else if(Soldiers.enemySoldiers.Contains(soldier))
            {
                Soldiers.enemySoldiers.Remove(soldier);
                Soldiers.diedEnemySoldiers.Add(soldier);
            }
        }
    }
}
