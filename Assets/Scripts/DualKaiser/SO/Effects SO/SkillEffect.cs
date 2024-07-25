using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualKaiser
{
    public abstract class SkillEffect : ScriptableObject
    {
        public abstract void Activate(Character user, Character target);
    }
}
