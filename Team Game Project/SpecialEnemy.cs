using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Game_Project
{
    internal class SpecialEnemy: Entity
    {
        public static List<Skill> SkillList = new List<Skill>();
        public SpecialEnemy(int hp, int str, int def, int mag, int res, int spd, string name): base(hp, str, def, mag, res, name)
        {

        }
    }
}
