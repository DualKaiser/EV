using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace DualKaiser
{
    [CreateAssetMenu(fileName = "New DOT Effect", menuName = "DualKaiser/Statuses/Create New DOT")]
    public class DotEffect : SkillEffect
    {
        public string statusName;

        public enum StatusType
        {
            BUFF, DEBUFF, DOT
        }

        public StatusType statusType;

        public enum DotTarget
        {
            ENEMY, SELF
        }

        public DotTarget dotTarget;

        /* Stat to Scale Off
        public enum ScaleStat
        {
            ATK, DEF, HP
        }

        public ScaleStat scalingAttribute;
        */

        [Header("DOT Duration")]
        public int Duration;

        [Header("DOT Percentage")]
        public float Potency;

        public override void Activate(Character user, Character target)
        {
            switch (dotTarget)
            {
                case DotTarget.ENEMY:
                    target.ApplyDot(new ActiveDot(this, Duration));
                    break;

                case DotTarget.SELF:
                    user.ApplyDot(new ActiveDot(this, Duration));
                    break;
            }
        }

        public void ApplyDot(Character target)
        {

        }

        public void RemoveDot(Character user)
        {

        }

        public void DotDamage(Character user)
        {
            int dotDamage = 0;

            dotDamage = (int)(user.originalHP * Potency);

            user.TakeDamage(dotDamage);
        }

    }

    [System.Serializable]
    public class ActiveDot
    {
        public DotEffect dotEffect;
        public int remainingDuration;
        public float potency;

        public ActiveDot(DotEffect dotEffect, int duration)
        {
            this.dotEffect = dotEffect;
            this.remainingDuration = duration;
            this.potency = dotEffect.Potency;
        }

        public void ApplyDot(Character user)
        {
            dotEffect.DotDamage(user);
        }
    }
}
