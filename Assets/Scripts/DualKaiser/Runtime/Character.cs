using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace DualKaiser
{
    public class Character: MonoBehaviour
    {
        public CharStats charstats;

        public string Name;

        [SerializeField]
        [Header("Runtime Stats")]
        public int currentHP;
        public int currentATK;
        public int currentDEF;
        public float currentCritR;
        public float currentCritDmg;

        [Header("Shield")]
        public int currentAMR;

        [Header("Passive")]
        public SkillEffect passiveSkill;

        [Header("Skills")]
        public SkillEffect offensiveSkill;
        public SkillEffect defensiveSkill;
        public SkillEffect ultimate;

        [Header("Active Buffs")]
        public List<BuffEffect> activeBuffs = new List<BuffEffect>();
        private Dictionary<BuffEffect, int> buffDurations = new Dictionary<BuffEffect, int>();

        [Header("Active Debuffs")]
        public List<DebuffEffect> activeDebuffs = new List<DebuffEffect>();
        private Dictionary<DebuffEffect, int> debuffDurations = new Dictionary<DebuffEffect, int>();

        [Header("Active DOTs")]
        public List<ActiveDot> activeDots = new List<ActiveDot>();

        // Original Stats
        [HideInInspector]
        public int originalHP;
        [HideInInspector]
        public int originalATK;
        [HideInInspector]
        public int originalDEF;
        [HideInInspector]
        public int originalAMR;
        [HideInInspector]
        public float originalCritR;
        [HideInInspector]
        public float originalCritDmg;


        private void Awake()
        {
            originalHP = charstats.baseHP;
            originalATK = charstats.baseATK;
            originalDEF = charstats.baseDEF;
            originalAMR = charstats.baseAMR;
            originalCritR = charstats.critR;
            originalCritDmg = charstats.critDmg;
       
            //Initialise Stats
            currentHP = charstats.baseHP;
            currentATK = charstats.baseATK;
            currentDEF = charstats.baseDEF;
            currentAMR = charstats.baseAMR;
            currentCritR = charstats.critR;
            currentCritDmg = charstats.critDmg;
        }

        // Method to Apply Buff Statuses
        public void ApplyBuff(BuffEffect buffEffect)
        {
            buffEffect.ApplyBuff(this);

            if (buffDurations.ContainsKey(buffEffect))
            {
                buffDurations[buffEffect] = buffEffect.Duration;
            }
            else
            {
                activeBuffs.Add(buffEffect);
                buffDurations[buffEffect] = buffEffect.Duration;
            }
        }

        // Method to Apply Debuff Statuses
        public void ApplyDebuff(DebuffEffect debuffEffect)
        {
            debuffEffect.ApplyDebuff(this);

            if (debuffDurations.ContainsKey(debuffEffect))
            {
                debuffDurations[debuffEffect] = debuffEffect.Duration;
            }
            else
            {
                activeDebuffs.Add(debuffEffect);
                debuffDurations[debuffEffect] = debuffEffect.Duration;
            }
        }

        // Method to Apply DOTs
        public void ApplyDot(ActiveDot dotEffect)
        {
            activeDots.Add(dotEffect);
        }


        public void CheckStatuses()
        {
            // Check through Active Buffs
            List<BuffEffect> buffsToRemove = new List<BuffEffect>();

            foreach (var buff in activeBuffs)
            {
                buffDurations[buff]--;

                if (buffDurations[buff] <= 0)
                {
                    buff.RemoveBuff(this);
                    buffsToRemove.Add(buff);
                }
            }
            foreach (var buff in buffsToRemove)
            {
                activeBuffs.Remove(buff);
                buffDurations.Remove(buff);
            }

            // Check through Active Debuffs
            List<DebuffEffect> debuffsToRemove = new List<DebuffEffect>();

            foreach (var debuff in activeDebuffs)
            {
                debuffDurations[debuff]--;

                if (debuffDurations[debuff] <= 0)
                {
                    debuff.RemoveDebuff(this);
                    debuffsToRemove.Add(debuff);
                }
            }
            foreach (var debuff in debuffsToRemove)
            {
                activeDebuffs.Remove(debuff);
                debuffDurations.Remove(debuff);
            }

            // Check through Active DOTs
            List<ActiveDot> dotsToRemove = new List<ActiveDot>();

            foreach (var dot in activeDots)
            {
                dot.remainingDuration--;

                if (dot.remainingDuration <= 0)
                {
                    dotsToRemove.Add(dot);
                }

                dot.ApplyDot(this);
            }
            foreach (var dot in dotsToRemove)
            {
                activeDots.Remove(dot);
            }
        }

        public void ActivateS1(Character target)
        {
            offensiveSkill.Activate(this, target);
        }

        // Public Methods

        public void TakeDamage(int damage)
        {
            // Check if there is Armour
            if (currentAMR > 0)
            {
                int armourAbsorbed = Mathf.Min(damage, currentAMR);
                currentAMR -= armourAbsorbed;
                damage -= armourAbsorbed;

                if (currentAMR < 0)
                {
                    currentAMR = 0;
                }
            }

            // Leftover Damage
            if (damage > 0 && damage > currentDEF)
            {
                currentHP -= damage - currentDEF;
            }
            else
            {
                currentHP -= damage;
            }
        }

        public void TakePierceDmg(int damage)
        {
            // Check if there is Armour
            if (currentAMR > 0)
            {
                int armourAbsorbed = Mathf.Min(damage, currentAMR);
                currentAMR -= armourAbsorbed;
                damage -= armourAbsorbed;

                if (currentAMR < 0)
                {
                    currentAMR = 0;
                }
            }

            // Leftover Damage
            if (damage > 0 && damage > currentDEF)
            {
                currentHP -= damage - currentDEF;
            }
            else
            {
                currentHP -= damage;
            }
        }

        public void Heal(int amount)
        {
            currentHP += amount;
        }

        // Buff Methods
        public void BuffAtk(int BuffAmount)
        {
            currentATK += BuffAmount;
        }

        public void BuffDef(int BuffAmount)
        {
            currentDEF += BuffAmount;
        }

        public void BuffHP(int BuffAmount)
        {
            currentHP += BuffAmount;
        }

        public void BuffAMR(int BuffAmount)
        {
            currentAMR += BuffAmount;
        }

        public void BuffCritR(float BuffAmountF)
        {
            currentCritR += BuffAmountF;
        }

        public void BuffCritDmg(float BuffAmountF)
        {
            currentCritDmg += BuffAmountF;
        }

    }
}
