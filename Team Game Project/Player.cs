using Microsoft.Xna.Framework.Graphics;
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
        private int _levelThreshold;
        public Player(string name, Texture2D t): base(50, 10, 10, 10, 5, name, t, 0)
        {
            _level = 1;
            _xp = 0;
            _levelThreshold = 100;
            _skillList = new List<Skill>();
        }
        public void addXP(int add)
        {
            _xp += add;
            while (_xp >= _levelThreshold)
                levelUp();
        }
        public void levelUp()
        {
            _level++;
            _xp -= _levelThreshold;
            _levelThreshold = (int) Math.Round(_levelThreshold * 1.2);
            _def += 10;
            _str += 5;
            _mag += 5;
            makeSkillList();
        }
        public int getLevel()
        {
            return _level;
        }
        public void makeSkillList()
        {
            _skillList.Clear();
            _skillList.Add(new Skill(0, (int)(_str*1.25), "Bite", 5));
            _skillList.Add(new Skill(2, (int)(_mag*1.25), "Fireball", 2));
            _skillList.Add(new Skill(1, (int)(_str*.5), "suck", 10));
            _skillList.Add(new Skill(1, (int)(_mag*.75), "SUCK", 12));
            _skillList.Add(new Skill(0, _str, "Strike", 0));
            _skillList.Add(new Skill(2, (int)(_mag*1.5), "Blood Spear", 15));
            _skillList.Add(new Skill(2, (int)(_mag*1.75), "Blood Slash", 20));
            _skillList.Add(new Skill(2, (int) (_mag*2.5), "Blood Rain",50));
            _skillList.Add(new Skill(1, (int) ((_mag*.2) +(_str *.5)), "Drain", 20)); 
            _skillList.Add(new Skill(0, _mag + _str, "Enhanced Claws", 10));
        }
        public void useSkill(Entity e, Skill s)
        {
            _currHP -= s.GetCost();
            int dmg = e.hurt(s.GetDMG(), s.GetSkillType());
            if (s.GetSkillType().Equals("Drain"))
                _currHP += dmg;
        }

        public void attack(Entity e)
        {
            if (_str > _mag)
                e.hurt(_str, "phys");
            else
                e.hurt(_mag, "mag");
        }
    }
}
