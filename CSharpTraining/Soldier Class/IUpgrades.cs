using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTraining.Soldier_Class
{
    interface IUpgrades
    {
        void Upgrade(List<Soldier> list, int ID, int uHP, int uDamage);
        void Upgrade(List<Soldier> list, int ID, int uHP, int uDamage, int uCompany);
        void Upgrade(List<Soldier> list, int ID, int uHP, int uDamage, string uWeapon);
        void Upgrade(List<Soldier> list, int ID, int uHP, int uDamage, string uWeapon, string uRank);
    }
}
