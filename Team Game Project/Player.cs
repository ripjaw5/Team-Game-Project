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
        public int _level;
        private int _xp;
        private int _levelThreshold;
        public Player(string name, Texture2D t): base(50, 10, 10, 10, 10 , name, t, 0)
        {
            _level = 5;
            _xp = 0;
            _levelThreshold = 150 ;
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
                _levelThreshold = (int)Math.Round(_levelThreshold * 1.2);
                _def += 5;
                _res += 5;
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
            _skillList.Add(new Skill(1, (int)(_str * 1.5), "suck", (int) (1 * _level *.5))); //at level 2, costs 1 and does 22 damage
            _skillList.Add(new Skill(2, (int)(_mag * 1.5), "Fireball", (int) (5 * _level * .5)));
            _skillList.Add(new Skill(0, (int)(_str*1.25), "Bite", (int)(3 * _level * .5)));
            _skillList.Add(new Skill(1, (int)(_mag*1.75), "SUCK", (int)(2 * _level * .5)));
            _skillList.Add(new Skill(2, (int)(_mag*1.5), "Blood Spear", (int)(5 * _level * .5)));
            _skillList.Add(new Skill(0, (int)(_str * 1.5), "Punch Of Doom", (int)(5 * _level * .5)));
            _skillList.Add(new Skill(0, _mag + _str, "Enhanced Claws", (int)(10 * _level * .5)));
            _skillList.Add(new Skill(1, (int)(_mag * 2.5), "Drain", (int)(12 * _level * .5)));
            _skillList.Add(new Skill(2, (int)(_mag*1.75), "Blood Slash", (int)(8 * _level * .5)));
            _skillList.Add(new Skill(0, (int)(_str * 1.85), "Bloody Stab", 45));
            _skillList.Add(new Skill(2, (int)(_mag * 2), "Eye Beams", 75));
            _skillList.Add(new Skill(2, (int) (_mag*2.25), "Blood Rain",80));
            _skillList.Add(new Skill(1, (int)(_str * 3.25), "Drain Punch",60));
            _skillList.Add(new Skill(2, (int)(_mag * 2.5), "Bloodbolt", 120));   
            _skillList.Add(new Skill(0, (int)(_str * 2.75), "Nuke Punch", 130));
            _skillList.Add(new Skill(2, _mag * 3, "Eye Blast", 150)); 
            _skillList.Add(new Skill(2, 450, "Vampire Rage", 350)); 
            _skillList.Add(new Skill(1, (int)(_mag * 5.5), "Draining Glare",150));
            _skillList.Add(new Skill(2, (int)(_mag * 5), "Last Resort", _hp-1));

            // and the funny one
             _skillList.Add(new Skill(2, ((_mag * 2) * (_str*2)), "Nuke", 1500));
        }
        public int useSkill(Entity e, Skill s)
        {
            _currHP -= s.GetCost();
            int dmg = e.hurt(s.GetDMG(), s.GetSkillType());
            if (s.GetSkillType().Equals("Drain"))
                _currHP += dmg;
            if (e.getCurrHP() <= 0)
                addXP(e._xp);
            return dmg;
        }

        public int attack(Entity e)
        {
            Random rng = new Random();
            int dmg;
            if (rng.Next(1000) < 1)
            {
                dmg = e.hurt(_str * 3, "phys");
                Game1._actionText = "BASED: ";
            }
            else
                dmg = e.hurt(_str, "phys");
            if (e.getCurrHP() <= 0)
                addXP(e._xp);

            return dmg;
        }
    }
}
