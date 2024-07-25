using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualKaiser
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "DualKaiser/Skills/Create New Skill")]
    public class Skill : SkillEffect
    {
        public string skillName;

        //public int skillPoints;

        public enum skillType
        {
            OFFENSIVE, DEFENSIVE, ULTIMATE
        }

        public skillType SkillType;

        [TextArea(3, 6)]
        public string skillDescription;

        public List<SkillEffect> skillEffects;

        public override void Activate(Character user, Character target)
        {
            foreach (var effect in skillEffects)
            {
                effect.Activate(user, target);
            }
        }
    }
}
