using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Game_Project
{
    internal class Player: Entity
    {
        private int _hp;
        public static List<Skill> _skillList;
        private int _level;
        private int _xp;
        private int _str;
        private int _def;

        public Player()
        {
            _hp = 50;
            _level = 1;
            _xp = 0;
            _str = 5;
            _def = 5;
        }
        public void addXP(int add)
        {
            _xp += add;
        }
    }
}
