using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JogoManagement : MonoBehaviour
{
    public int P1_HP = 100;
    public int P1_MANA = 100;
    public int P2_HP = 100;
    public int P2_MANA = 100;

    public int damage;
    public int cust;

    public TMP_Text P1HPtext;
    public TMP_Text P1MPtext;
    public TMP_Text P2HPtext;
    public TMP_Text P2MPtext;

    public bool TurnStart;
    public bool TurnFinished;

    public Deck dc;



    void Start()
    {
        dc = FindObjectOfType<Deck>();
    }

    
    void Update()
    {
        P1HPtext.text = P1_HP + "/100";
        P1MPtext.text = P1_MANA + "/100";
        P2HPtext.text = P2_HP + "/100";
        P2MPtext.text = P2_MANA + "/100";

        if (TurnStart && dc.drawed.Count < 5)
        {
            dc.DrawCard();
            TurnStart = false;
        }

        if (TurnFinished)
        {
            P1_MANA = P1_MANA - cust;
            P2_HP = P2_HP - damage;
            TurnFinished = false;
            TurnStart = true;
        }

        if(P1_HP <= 0 || P2_HP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (P1_MANA <= 0)
        {
            P1_MANA = 0;
        }
        if (P2_MANA <= 0)
        {
            P2_MANA = 0;
        }
    }

    
        
    
}
