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
        public Player(string name, Texture2D t): base(50, 10, 10, 10, 10, name, t, 0)
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
            _res += 10;
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
            _skillList.Add(new Skill(1, (int)(_str * 1.5), "suck", 5));
            _skillList.Add(new Skill(2, (int)(_mag * 1.25), "Fireball", 2));
            _skillList.Add(new Skill(0, (int)(_str*1.25), "Bite", 7));
            _skillList.Add(new Skill(1, (int)(_mag*1.75), "SUCK", 25));
            _skillList.Add(new Skill(2, (int)(_mag*1.5), "Blood Spear", 15));
            _skillList.Add(new Skill(0, (int)(_str * 1.5), "Punch Of Doom", 30));
            _skillList.Add(new Skill(0, _mag + _str, "Enhanced Claws", 35));
            _skillList.Add(new Skill(1, (int)(_mag * 2.5), "Drain", 50));
            _skillList.Add(new Skill(2, (int)(_mag*1.75), "Blood Slash", 30));
            _skillList.Add(new Skill(0, (int)(_mag * 1.85), "Bloody Stab", 45));
            _skillList.Add(new Skill(2, (int)(_mag * 2), "Eye Beams", 50));
            _skillList.Add(new Skill(2, (int) (_mag*2.25), "Blood Rain",65));
            _skillList.Add(new Skill(1, (int)(_str * 3.25), "Drain Punch",60));
            _skillList.Add(new Skill(2, (int)(_mag * 2.5), "Bloodbolt", 100));   
            _skillList.Add(new Skill(0, (int)(_str * 2.75), "Punch Of Nuclear Power", 110));
            _skillList.Add(new Skill(2, _mag * 3, "Eye Blast", 135)); 
            _skillList.Add(new Skill(2, 300, "Vampire Rage", 150)); 
            _skillList.Add(new Skill(1, (int)(_mag * 4.5), "Draining Glare",200));
            _skillList.Add(new Skill(2, (int)(_mag * 5), "Last Resort", _currHP-1));

            // and the funny one
            // _skillList.Add(new Skill(2, ((_mag * 2) * (_str*2)), "Nuke", 35000));
        }
        public void useSkill(Entity e, Skill s)
        {
            _currHP -= s.GetCost();
            int dmg = e.hurt(s.GetDMG(), s.GetSkillType());
            if (s.GetSkillType().Equals("Drain"))
                _currHP += dmg;
            if (e.getCurrHP() <= 0)
                addXP(e._xp);
        }

        public void attack(Entity e)
        {
            e.hurt(_str, "phys");
            if (e.getCurrHP() <= 0)
                addXP(e._xp);
        }
    }
}
