using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTraining.Soldier_Class
{
    public interface IWeapon
    {
        string Weapon { get; set; }
        int Damage { get; set; }

        void GetDamage(Soldier _soldier, int _damage, string _weapon);
    }
}
