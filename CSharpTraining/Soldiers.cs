using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTraining.Soldier_Class;

namespace CSharpTraining
{
    public class Soldiers : Soldier, IUpgrades
    {
        public static List<Soldier> soldiers { get; private set; } = new List<Soldier>();
        public static List<Soldier> enemySoldiers { get; private set; } = new List<Soldier>();
        public static List<Soldier> diedSoldiers { get; private set; } = new List<Soldier>();
        public static List<Soldier> diedEnemySoldiers { get; private set; } = new List<Soldier>();

        public void AddSoldiers(bool enemy, string name, string rank, int exp, int hp, int company, string weapon, int damage)
        {
            this.Name = name;
            this.HP = hp;
            this.Company = company;
            this.Rank = rank;
            this.Exp = exp;

            this.Weapon = weapon;
            this.Damage = damage;

            if(!enemy) soldiers.Add(this);
            else enemySoldiers.Add(this);
        }

        //Вывод всех солдат
        public void Info(bool enemy, int ID)
        {
            if(!enemy)
            {
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Rank: {soldiers[ID].Rank}\nName: {soldiers[ID].Name}:\nExp: {soldiers[ID].Exp}:\nHP: {soldiers[ID].HP}:\nDamage: {soldiers[ID].Damage}:\nWeapon: {soldiers[ID].Weapon}\nCompany: {soldiers[ID].Company}\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Rank: {enemySoldiers[ID].Rank}\nName: {enemySoldiers[ID].Name}:\nExp: {enemySoldiers[ID].Exp}:\nHP: {enemySoldiers[ID].HP}:\nDamage: {enemySoldiers[ID].Damage}:\nWeapon: {enemySoldiers[ID].Weapon}\nCompany: {enemySoldiers[ID].Company}\n");
            }

            
        }

        public static void InfoAll(bool enemy)
        {

            if(!enemy)
            {
                Console.WriteLine("{0, 0} | {1, 18} | {2, 10} | {3, 10} | {4, 10} | {5, 20} | {6, 10} |", "Rank", "Name", "Exp", "HP", "Damage", "Weapon", "Company");

                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Soldier s in soldiers)
                {
                    Console.WriteLine("{0, 0} | {1, 15} | {2, 10} | {3, 10} | {4, 10} | {5, 20} | {6, 11}|", s.Rank, s.Name, s.Exp, s.HP, s.Damage, s.Weapon, s.Company);
                }

                Console.WriteLine($"Soldeirs count {soldiers.Count}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (Soldier s in enemySoldiers)
                {
                    Console.WriteLine("{0, 0} | {1, 15} | {2, 10} | {3, 10} | {4, 10} | {5, 20} | {6, 11}|", s.Rank, s.Name, s.Exp, s.HP, s.Damage, s.Weapon, s.Company);
                }

                Console.WriteLine($"Soldeirs count {enemySoldiers.Count}");
            }
            
            Console.WriteLine();
        }

        public void Upgrade(List<Soldier> list, int ID, int uHP, int uDamage)
        {
            list[ID].HP = uHP;
            list[ID].Damage = uDamage;
        }

        public void Upgrade(List<Soldier> list, int ID, int uHP, int uDamage, int uCompany)
        {
            list[ID].HP = uHP;
            list[ID].Damage = uDamage;
            list[ID].Company = uCompany;
        }

        public void Upgrade(List<Soldier> list, int ID, int uHP, int uDamage, string uWeapon)
        {
            list[ID].HP = uHP;
            list[ID].Damage = uDamage;
            list[ID].Weapon = uWeapon;
        }

        public void Upgrade(List<Soldier> list, int ID, int uHP, int uDamage, string uWeapon, string uRank)
        {
            list[ID].HP = uHP;
            list[ID].Damage = uDamage;
            list[ID].Rank = uRank;
        }
    }
}
