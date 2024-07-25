using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualKaiser
{
    [CreateAssetMenu(fileName = "New Buff Effect", menuName = "DualKaiser/Statuses/Create New Buff")]
    public class BuffEffect : SkillEffect
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

        public BuffStat statToBuff;

        [Header("Buff Duration")]
        public int Duration;

        [Header(" ")]
        public bool isFlat = true;

        [Header("Flat Buff")]
        public int BuffAmount;

        [Header("Buff (For Crit Stats)")]
        public float BuffAmountF;

        [Header("Buff Percentage")]
        public float Potency;

        public override void Activate(Character user, Character target)
        {
            user.ApplyBuff(this);
        }

        public void ApplyBuff(Character user)
        {
            if (isFlat == true)
            {
                ApplyFlatBuff(user);
            }
            else
            {
                ApplyPercentageBuff(user);
            }
        }

        public void RemoveBuff(Character user)
        {
            if (isFlat == true)
            {
                RemoveFlatBuff(user);
            }
            else
            {
                RemovePercentageBuff(user);
            }
        }

        private void ApplyFlatBuff(Character user)
        {
            switch (statToBuff)
            {
                case BuffStat.ATK:
                    user.BuffAtk(BuffAmount);
                    break;

                case BuffStat.DEF:
                    user.BuffDef(BuffAmount);
                    break;

                case BuffStat.HP:
                    user.BuffHP(BuffAmount);
                    break;

                // *Armour starts at 0
                case BuffStat.AMR:
                    user.BuffAMR(BuffAmount);
                    break;

                case BuffStat.CRITR:
                    user.BuffCritR(BuffAmountF);
                    break;

                case BuffStat.CRITDMG:
                    user.BuffCritDmg(BuffAmountF);
                    break;
            }
        }

        private void ApplyPercentageBuff(Character user)
        {
            switch (statToBuff)
            {
                case BuffStat.ATK:
                    user.BuffAtk((int)(user.currentATK * Potency));
                    break;

                case BuffStat.DEF:
                    user.BuffDef((int)(user.currentDEF * Potency));
                    break;

                case BuffStat.HP:
                    user.BuffHP((int)(user.currentHP * Potency));
                    break;

                // *Armour starts at 0
                case BuffStat.AMR:
                    user.BuffAMR((int)(user.currentAMR * Potency));
                    break;
            }
        }

        private void RemoveFlatBuff(Character user)
        {
            switch (statToBuff)
            {
                case BuffStat.ATK:
                    user.BuffAtk(-BuffAmount);
                    break;

                case BuffStat.DEF:
                    user.BuffDef(-BuffAmount);
                    break;

                case BuffStat.HP:
                    user.BuffHP(-BuffAmount);
                    break;

                case BuffStat.AMR:
                    user.BuffAMR(-BuffAmount);
                    break;

                case BuffStat.CRITR:
                    user.BuffCritR(-BuffAmountF);
                    break;

                case BuffStat.CRITDMG:
                    user.BuffCritDmg(-BuffAmountF);
                    break;
            }
        }

        private void RemovePercentageBuff(Character user)
        {
            switch (statToBuff)
            {
                case BuffStat.ATK:
                    user.BuffAtk(-(int)(user.originalATK * Potency));
                    break;

                case BuffStat.DEF:
                    user.BuffDef(-(int)(user.originalDEF * Potency));
                    break;

                case BuffStat.HP:
                    user.BuffHP(-(int)(user.originalHP * Potency));
                    break;

                case BuffStat.AMR:
                    user.BuffAMR(-(int)(user.originalAMR * Potency));
                    break;
            }
        }
    }
}
