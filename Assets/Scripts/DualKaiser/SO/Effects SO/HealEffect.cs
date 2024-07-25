using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualKaiser
{
    [CreateAssetMenu(fileName = "New Heal Effect", menuName = "DualKaiser/Effects/Create New Heal Effect")]
    public class HealEffect : SkillEffect
    {
        public string effectName;

        public enum EffectType
        {
            DAMAGE, HEAL
        }

        public EffectType effectType;

        // Stat to Scale Off
        public enum ScaleStat
        {
            ATK, DEF, HP, AMR
        }

        public ScaleStat scalingAttribute;

        public bool isFlat = true;

        [Header("Heal Amount")]
        public int HealAmount;

        [Header("Heal Potency")]
        public float Potency;

        public override void Activate(Character user, Character target)
        {
            if (isFlat == true)
            {
                int amount = HealAmount;
                user.Heal(amount);
            }
            else
            {
                float statValue = 0f;

                switch (scalingAttribute)
                {
                    case ScaleStat.ATK:
                        statValue = user.currentATK;
                        break;
                    case ScaleStat.DEF:
                        statValue = user.currentDEF;
                        break;
                    case ScaleStat.HP:
                        statValue = user.currentHP;
                        break;
                    case ScaleStat.AMR:
                        statValue = user.currentAMR;
                        break;
                }

                int amount = (int)(statValue * Potency);
                user.Heal(amount);
            }
            
        }
    }
}
