using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Game_Project
{
    internal class Entity
    {
        protected int _hp;
        protected int _str;
        protected int _def;
        protected int _res;
        protected int _mag;
        protected int _currHP;
        protected string _name;
        public Entity(int hp, int str, int def, int mag, int res, string name)
        {
            _hp = hp;
            _str = str;
            _def = def;
            _mag = mag;
            _res = res;
            _name = name;
            _currHP = _hp;
        }

        public void attack(Entity e)
        {
            e.hurt(_str);
        }
        public void hurt(int dmg)
        {
            int dmgtaken;
            dmgtaken = dmg - _def;
            if(dmgtaken <= 0)
            {
                dmgtaken = 1;
            }
            _hp -= dmgtaken;
        }
        public int getCurrHP()
        {
            return _currHP;
        }
    }
}
