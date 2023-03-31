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
        public Player(string name, Texture2D t): base(50, 10, 10, 10, 5, name, t)
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
        }
        
        public void makeSkillList()
        {
            _skillList.Add(new Skill(0, (int)(_str*1.25) , "Insert Flavor Text Here", "Bite", 5));
            _skillList.Add(new Skill(2, (int)(_mag*1.25), "Insert Flavor Text Here", "Fireball", 2));
            _skillList.Add(new Skill(1, (int)(_str*.5), "Insert Flavor Text Here", "suck", 10));
            _skillList.Add(new Skill(1, (int)(_mag*.75), "Insert Flavor Text Here", "SUCK", 12));
            _skillList.Add(new Skill(0, _str, "It's just a punch bro", "Strike", 0));
            _skillList.Add(new Skill(2, (int)(_mag*1.5), "Insert Flavor Text Here", "Blood Spear", 15));
            _skillList.Add(new Skill(2, (int)(_mag*1.75), "Insert Flavor Text Here", "Blood Slash", 20));
            _skillList.Add(new Skill(2, (int) (_mag*2.5), "Insert Flavor Text Here", "Blood Rain",50));
            _skillList.Add(new Skill(1, (int) ((_mag*.2) +(_str *.5)), "Insert Flavor Text Here", "Drain", 20)); 
            _skillList.Add(new SKill(0, _mag + _str, "Insert Flavor Text Here", "Enhanced Claws", 10));
        }
    }
}
