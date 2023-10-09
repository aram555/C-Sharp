using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTraining.Soldier_Class
{
    public interface ISoldier
    {
        string Name { get; set; }
        string Rank { get; set; }
        int Exp { get; set; }
        int HP { get; set; }
        int Company { get; set; }

        void Shoot(Soldier Enemy, int randomShot);
        void Die(Soldier soldier);
    }
}
