﻿using Microsoft.Xna.Framework;
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
        private string _name;
        private int _cost;
        public static Texture2D _skillAnimations;
        public Skill(int skillType, int dmg, string name, int cost)
        {
            _skillType = (SkillType)skillType;
            _dmg = dmg;
            _name = name;
            _cost = cost;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 pos, SpriteFont font)
        {
            string dType;
            if (_skillType == SkillType.Drain)
                dType = "Drain";
            if (_skillType == SkillType.Phys)
                dType = "Phys";
            if (_skillType == SkillType.Mag)
                dType = "Mag";
            else
                dType = "";
            spriteBatch.DrawString(font, _name + ": " + _dmg + " " + dType, pos, Color.White);
        }
    }
}
