using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Game_Project
{
    internal class Player: Entity
    {
        public static List<Skill> _skillList;
        private int _level;
        private int _xp;
        private int _currHP;

        public Player(string name): base(50, 5, 5, 5, 5, 5, name)
        {
            _level = 1;
            _xp = 0;
            _currHP = 50;
        }
        public void addXP(int add)
        {
            _xp += add;
        }
    }
}
