using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private int _xp;
        protected Texture2D _texture;
        public Entity(int hp, int str, int def, int mag, int res, string name, Texture2D texture, int xp)
        {
            _hp = hp;
            _str = str;
            _def = def;
            _mag = mag;
            _res = res;
            _name = name;
            _currHP = _hp;
            _texture = texture;
            _xp = xp;
        }

        public void attack(Entity e)
        {
            if (_str > _mag)
                e.hurt(_str, "phys");
            else
                e.hurt(_mag, "mag");
        }
        public int hurt(int dmg, String type)
        {

            int dmgTaken;
            dmgTaken = dmg - _def;
            if (dmgTaken <= 0)
            {
                dmgTaken = 1;
            }
            _currHP -= dmgTaken;
            return dmgTaken;
        }
        public int getCurrHP()
        {
            return _currHP;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Rectangle? src)
        {
            if (src != null)
                spriteBatch.Draw(_texture, pos, src, Color.White);
            else
                spriteBatch.Draw(_texture, pos, Color.White);
        }
        public Entity clone(Player p)
        {
            double multiplier = Math.Max(p.getLevel() *.75 , 1);
            return new Entity(_hp * p.getLevel(), (int) (_str * multiplier), (int)(_def * multiplier), (int)(_mag * multiplier), (int)(_res * multiplier), _name, _texture, (int) (_xp * (.5 * multiplier)));
        }
    }
}
