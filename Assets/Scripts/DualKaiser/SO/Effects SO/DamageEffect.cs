using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualKaiser
{
    [CreateAssetMenu(fileName = "New Damage Effect", menuName = "DualKaiser/Effects/Create New Damage Effect")]
    public class DamageEffect : SkillEffect
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

        [Header("Damage Potency")]
        public float Potency;

        public override void Activate(Character user, Character target)
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

            // No Crit
            if (Random.Range(0f, 1f) >= user.currentCritR)
            {
                int damage = (int)(statValue * Potency);
                target.TakeDamage(damage);
            }
            // With Crit
            else
            {
                int damage = (int)(statValue * Potency * user.currentCritDmg);
                target.TakeDamage(damage);
            }
        }
    }
}
