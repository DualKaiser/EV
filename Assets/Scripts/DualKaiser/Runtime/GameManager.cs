using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DualKaiser
{
    public enum BattleState { START, C1ACTION, C1ATTACK, C2ACTION, C2ATTACK, C1WIN, C2WIN }

    public class GameManager : MonoBehaviour
    {
        public BattleState state;

        [Header("Game Objects")]

        [SerializeField]
        private GameObject char1;

        [SerializeField]
        private GameObject char2;

        [Header("Spawn Positions")]
        public Transform char1spawn;
        public Transform char2spawn;

        // Script managers 
        Character m_char1;
        Character m_char2;

        //CharStats char1Stats;
        //CharStats char2Stats;

        [Header("UI Elements")]

        public TMP_Text char1HUD;
        public TMP_Text char2HUD;

        public TMP_Text char1HPValue;
        public TMP_Text char2HPValue;

        public TMP_Text char1DmgNo;
        public TMP_Text char2DmgNo;

        public Slider hpslider1;
        public Slider hpslider2;
        public Slider amrslider1;
        public Slider amrslider2;

    

        void Start()
        {
            state = BattleState.START;
            SpawnChars();
        }

        void SpawnChars()
        {
            // Spawn instances
            GameObject char1GO = Instantiate(char1, char1spawn);
            GameObject char2GO = Instantiate(char2, char2spawn);

            // Pull script references
            m_char1 = char1GO.GetComponent<Character>();
            m_char2 = char2GO.GetComponent<Character>();

            // Set UI
            char1HUD.text = m_char1.Name;
            char2HUD.text = m_char2.Name;

            char1HPValue.text = m_char1.currentHP.ToString();
            char2HPValue.text = m_char2.currentHP.ToString();

            //Get list of skills

            /*Debug.Log(char1Stats.charName + " " + "Loaded");
              Debug.Log(char2Stats.charName + " " + "Loaded");

              mainText.text = char1Stats.name + "'s Turn";*/

            Debug.Log(m_char1.currentHP);

            state = BattleState.C1ACTION;

            SetMaxHealth();

            Char1Turn();
        }

        public void SetMaxHealth()
        {
            // Set max value for health bar sliders
            hpslider1.maxValue = m_char1.currentHP;
            hpslider2.maxValue = m_char2.currentHP;

            // Set health bar sliders to current value
            hpslider1.value = m_char1.currentHP;
            hpslider2.value = m_char2.currentHP;

            // Set armour bar sliders to current value
            amrslider1.value = m_char1.currentAMR;
            amrslider2.value = m_char2.currentAMR;
        }

        public void UpdateHealthBar()
        {
            // To be called whenever there is a change in HP/AMR values
            hpslider1.value = m_char1.currentHP;
            hpslider2.value = m_char2.currentHP;

            amrslider1.value = m_char1.currentAMR;
            amrslider2.value = m_char2.currentAMR;

            // Set health values to text component
            char1HPValue.text = m_char1.currentHP.ToString();
            char2HPValue.text = m_char2.currentHP.ToString();
        }

        public void Char1Turn()
        {
            
        }

        public void C1S1AttackButton()
        {
            if (state != BattleState.C1ACTION)
                return;

            m_char1.ActivateS1(m_char2);

            UpdateHealthBar();
        }

        public void C1S2AtackButton()
        {
            m_char2.CheckStatuses();
        }
    }
}
