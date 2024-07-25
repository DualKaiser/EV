using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualKaiser
{
    public class BattleStateMachine : MonoBehaviour
    {
        public enum PerformAction
        {
            WAIT, CHOOSEACTION, PERFORMACTION
        }

        public PerformAction battleState;

        // Start is called before the first frame update
        void Start()
        {
            battleState = PerformAction.WAIT;
        }

        // Update is called once per frame
        void Update()
        {
            switch (battleState)
            {
                case (PerformAction.WAIT):

                    break;

                case (PerformAction.CHOOSEACTION):

                    break;

                case (PerformAction.PERFORMACTION):

                    break;
            }
        }
    }
}
