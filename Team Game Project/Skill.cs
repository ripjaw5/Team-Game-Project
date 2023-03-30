using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Game_Project
{
    internal class Skill
    {
        enum SkillType
        {
            Phys,
            Drain,
            Mag
        }
        private SkillType _skillType;
        private int _dmg;
        private string _desc;
        private string _name;
        private int _cost;
        public static Texture2D _skillAnimations;
        public Skill(int skillType, int dmg, string desc, string name, int cost)
        {
            _skillType = (SkillType) skillType;
            _dmg = dmg;
            _desc = desc;
            _name = name;
            _cost = cost;
        }
    }
}
