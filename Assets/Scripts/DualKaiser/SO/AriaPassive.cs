using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualKaiser
{
    [CreateAssetMenu(fileName = "Aria Passive", menuName = "DualKaiser/Passives/Aria")]
    public class AriaPassive: SkillEffect
    {
        public string passiveSkillName;

        [TextArea(3, 6)]
        public string skillDescription;

        public DotEffect monitoredDot;

        public List<SkillEffect> skillEffects;

        public override void Activate(Character user, Character target)
        {
            bool hasBurn = false;

            foreach (var activeDot in user.activeDots)
            {
                if (activeDot.dotEffect == monitoredDot)
                {
                    hasBurn = true;
                    break;
                }
            }

            if (hasBurn)
            {
                foreach (var effect in skillEffects)
                {
                    effect.Activate(user, target);
                }
            }
        }
    }
}

