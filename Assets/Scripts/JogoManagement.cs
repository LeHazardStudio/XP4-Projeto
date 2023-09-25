using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
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
    public int p1abRounds;
    public int p1dbRounds;
    public float p1attackBuff;
    public float p1defenseBuff;
    public int p2damage;
    public int p2cust;
    public int p2dbRounds;
    public int p2abRounds;
    public float p2attackBuff;
    public float p2defenseBuff;

    public TMP_Text P1HPtext;
    public TMP_Text P1MPtext;
    public TMP_Text P2HPtext;
    public TMP_Text P2MPtext;
    public TMP_Text P1Bufftext;

    public bool TurnStart;
    public bool TurnFinished;
    public bool bdActivated;
    public bool canPlay;
    public bool playerCollision;
    public bool enemyCollision;

    public Deck dc;
    public Enemy enm;
    public Board b;
   
    public GameObject skipTurn;
    public GameObject player;
    public GameObject enemy;

    public GameObject playerPosition;
    public GameObject enemyPosition;

    public GameObject lastBoardPlayer;
    public GameObject lastBoardEnemy;

    void Start()
    {
        dc = FindObjectOfType<Deck>();
        enm = FindObjectOfType<Enemy>();
        b = FindObjectOfType<Board>();
    }

    
    void Update()
    {
        P1HPtext.text = P1_HP + "/100";
        P1MPtext.text = P1_MANA + "/100";
        P2HPtext.text = P2_HP + "/100";
        P2MPtext.text = P2_MANA + "/100";
        P1Bufftext.text = "Attack: " + p1attackBuff + " / " + "Defense: " + p1defenseBuff;
        if (TurnStart)
        {
            playerPosition.transform.position = lastBoardPlayer.transform.position;
            enemyPosition.transform.position = lastBoardEnemy.transform.position;
            P1_MANA = P1_MANA + 5;
            P2_MANA = P2_MANA + 5;
            if (dc.drawed.Count < 5)
            {
                dc.DrawCard();
                TurnStart = false;
            }

            else
            {
                dc.draw = true;
                dc.use = false;
                TurnStart = false;
            }

            if (enm.drawed.Count < 5)
            {
                enm.DrawCard();
            }
            /*foreach (GameObject g in dc.drawed)
            {
                if(g.GetComponent<Cards>().mana < P1_MANA)
                {
                    canPlay = true;
                }

                else
                {
                    canPlay = false;
                }
            }
            if(canPlay == false)
            {
                skipTurn.SetActive(true);
            }
            else
            {
                skipTurn.SetActive(false); 
            }*/

            TurnStart = false;
        }

        if (TurnFinished)
        {
            player.transform.position = lastBoardPlayer.transform.position ;
            enemy.transform.position =  lastBoardEnemy.transform.position;
            P1_MANA = P1_MANA - p1cust;
            P2_MANA = P2_MANA - p2cust;
            P1_HP = (int)(P1_HP - (p2damage + (p2damage * p2attackBuff)) - (p2damage * p1defenseBuff));
            P2_HP = (int)(P2_HP - (p1damage + (p2damage * p1attackBuff)) - (p1damage * p2defenseBuff));
            enemyCollision = false;
            
            playerCollision = false;
            for(int i = 1; i <= 9; i++)
            {
                b.BoardPositions[i].GetComponent<MeshRenderer>().enabled = false;
                b.BoardPositions[i].GetComponent<BoxCollider>().enabled = false;
            }
            TurnFinished = false;
            TurnStart = true;
        }

        /*if(P1_HP <= 0 || P2_HP <= 0)
        {
            SceneManager.LoadScene("Menu");
        }*/

        if (P1_HP <= 0)
        {
            SceneManager.LoadScene("Perdeu");
        }

        if (P2_HP <= 0)
        {
            SceneManager.LoadScene("Venceu");
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
