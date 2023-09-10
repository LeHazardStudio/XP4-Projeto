using System;
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

    public int p1damage;
    public int p1cust;
    public int p1bdRounds;
    public float p1buffDebuff;
    public int p2damage;
    public int p2cust;
    public int p2bdRounds;
    public float p2buffDebuff;

    public TMP_Text P1HPtext;
    public TMP_Text P1MPtext;
    public TMP_Text P2HPtext;
    public TMP_Text P2MPtext;

    public bool TurnStart;
    public bool TurnFinished;
    public bool bdActivated;
    public bool canPlay;

    public Deck dc;
    public Enemy enm;

    public GameObject skipTurn;



    void Start()
    {
        dc = FindObjectOfType<Deck>();
        enm = FindObjectOfType<Enemy>();
    }

    
    void Update()
    {
        P1HPtext.text = P1_HP + "/100";
        P1MPtext.text = P1_MANA + "/100";
        P2HPtext.text = P2_HP + "/100";
        P2MPtext.text = P2_MANA + "/100";
        if (TurnStart)
        {
            P1_MANA = P1_MANA + 5;
            P2_MANA = P2_MANA + 5;
            if (dc.drawed.Count < 5)
            {
                dc.DrawCard();
                TurnStart = false;
            }
            if (enm.drawed.Count < 5)
            {
                enm.DrawCard();
            }
            foreach (GameObject g in dc.drawed)
            {
                if(g.GetComponent<Cards>().mana < P1_MANA)
                {
                    canPlay = true;
                }
            }
            if(canPlay = false)
            {
                skipTurn.SetActive(true);
            }
            else
            {
                skipTurn.SetActive(false); 
            }

            TurnStart = false;
        }

        if (TurnFinished)
        {

            P1_MANA = P1_MANA - p1cust;
            P2_MANA = P2_MANA - p2cust;
         
            P2_HP = P2_HP - p1damage;
         
            P1_HP = P1_HP - p2damage;
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
        if (P1_MANA > 100)
        {
            P1_MANA = 100;
        }
        if (P2_MANA > 100)
        {
            P2_MANA = 100;
        }


    }

    
        
    
}
