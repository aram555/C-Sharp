using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTraining
{
    public class Character
    {
        public int Health { get; private set; } = 100;

        public bool GetDamage(int damage, out string result)
        {
            if(damage < Health)
            {
                Health -= damage;
                result = $"Character is damaged, now you HP = {Health}";
                
                return true;
            }
            else
            {
                Health = 0;
                result = "Your Character died";
                return false;
            }
        }
    }
}
