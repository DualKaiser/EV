using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualKaiser
{
    [CreateAssetMenu(fileName = "New Debuff Effect", menuName = "DualKaiser/Statuses/Create New Debuff")]
    public class DebuffEffect : SkillEffect
    {
        public string statusName;

        public enum StatusType
        {
            BUFF, DEBUFF, DOT
        }

        public StatusType statusType;

        public enum BuffStat
        {
            ATK, DEF, HP, AMR, CRITR, CRITDMG
        }

        public BuffStat statToDebuff;

        [Header("Debuff Duration")]
        public int Duration;

        [Header(" ")]
        public bool isFlat = true;

        [Header("Flat Debuff")]
        public int DebuffAmount;

        [Header("Debuff (For Crit Stats")]
        public float DebuffAmountF;

        [Header("Debuff Percentage")]
        public float Potency;

        public override void Activate(Character user, Character target)
        {
            target.ApplyDebuff(this);
        }

        public void ApplyDebuff(Character target)
        {
            if (isFlat == true)
            {
                ApplyFlatDebuff(target);
            }
            else
            {
                ApplyPercentageDebuff(target);
            }
        }

        public void RemoveDebuff(Character user)
        {
            if (isFlat == true)
            {
                RemoveFlatDebuff(user);
            }
            else
            {
                RemovePercentageDebuff(user);
            }
        }

        private void ApplyFlatDebuff(Character target)
        {
            switch (statToDebuff)
            {
                case BuffStat.ATK:
                    target.BuffAtk(-DebuffAmount);
                    break;

                case BuffStat.DEF:
                    target.BuffDef(-DebuffAmount);
                    break;

                case BuffStat.HP:
                    target.BuffHP(-DebuffAmount);
                    break;

                // *Armour starts at 0
                case BuffStat.AMR:
                    target.BuffAMR(-DebuffAmount);
                    break;

                case BuffStat.CRITR:
                    target.BuffCritR(-DebuffAmountF);
                    break;

                case BuffStat.CRITDMG:
                    target.BuffCritDmg(-DebuffAmountF);
                    break;
            }
        }

        private void ApplyPercentageDebuff(Character target)
        {
            switch (statToDebuff)
            {
                case BuffStat.ATK:
                    target.BuffAtk(-(int)(target.currentATK * Potency));
                    break;

                case BuffStat.DEF:
                    target.BuffDef(-(int)(target.currentDEF * Potency));
                    break;

                case BuffStat.HP:
                    target.BuffHP(-(int)(target.currentHP * Potency));
                    break;

                // *Armour starts at 0
                case BuffStat.AMR:
                    target.BuffAMR(-(int)(target.currentAMR * Potency));
                    break;
            }
        }

        private void RemoveFlatDebuff(Character user)
        {
            switch (statToDebuff)
            {
                case BuffStat.ATK:
                    user.BuffAtk(DebuffAmount);
                    break;

                case BuffStat.DEF:
                    user.BuffDef(DebuffAmount);
                    break;

                case BuffStat.HP:
                    user.BuffHP(DebuffAmount);
                    break;

                case BuffStat.AMR:
                    user.BuffAMR(DebuffAmount);
                    break;

                case BuffStat.CRITR:
                    user.BuffCritR(DebuffAmountF);
                    break;

                case BuffStat.CRITDMG:
                    user.BuffCritDmg(DebuffAmountF);
                    break;
            }
        }

        private void RemovePercentageDebuff(Character user)
        {
            switch (statToDebuff)
            {
                case BuffStat.ATK:
                    user.BuffAtk((int)(user.originalATK * Potency));
                    break;

                case BuffStat.DEF:
                    user.BuffDef((int)(user.originalDEF * Potency));
                    break;

                case BuffStat.HP:
                    user.BuffHP((int)(user.originalHP * Potency));
                    break;

                case BuffStat.AMR:
                    user.BuffAMR((int)(user.originalAMR * Potency));
                    break;
            }
        }
    }
}
