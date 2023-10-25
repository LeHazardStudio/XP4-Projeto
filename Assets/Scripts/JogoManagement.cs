using JetBrains.Annotations;
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
    public bool walk;

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

    public List<GameObject> effects;
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

            P1_MANA = P1_MANA - p1cust;
            P2_MANA = P2_MANA - p2cust;
            StartCoroutine(turnFinished());
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

    public IEnumerator turnFinished()
    {
        walk = false;
        player.transform.position = lastBoardPlayer.transform.position;
        enemy.transform.position = lastBoardEnemy.transform.position;
        if (!dc.skip)
        {
            if (dc.selectedCard.GetComponent<Cards>().isAttack)
            {
                if (!dc.selectedCard.GetComponent<Cards>().isUltimate)
                {
                    for (int i = 0; i < dc.attackAreas.Count; i++)
                    {
                        dc.attackAreas[i].GetComponent<MeshRenderer>().enabled = true;
                        dc.attackAreas[i].GetComponent<BoxCollider>().enabled = true;
                        GameObject effect = Instantiate(dc.selectedCard.GetComponent<Cards>().effect,
                            dc.attackAreas[i].transform.position, Quaternion.identity);
                        effect.transform.position = new Vector3(effect.transform.position.x,
                            dc.selectedCard.GetComponent<Cards>().particleY, effect.transform.position.z);
                        effect.transform.Rotate(dc.selectedCard.GetComponent<Cards>().rotation, 0.0f, 0.0f, Space.Self);
                        effects.Add(effect);

                    }
                }
                else
                {
                    for (int i = 0; i < dc.attackAreas.Count; i++)
                    {
                        dc.attackAreas[i].GetComponent<MeshRenderer>().enabled = true;
                        dc.attackAreas[i].GetComponent<BoxCollider>().enabled = true;
                        GameObject effect = Instantiate(dc.selectedCard.GetComponent<Cards>().effect,
                            b.EnemyPositions[5].transform.position, Quaternion.identity);
                        effect.transform.position = new Vector3(effect.transform.position.x,
                            dc.selectedCard.GetComponent<Cards>().particleY, effect.transform.position.z);
                        effect.transform.Rotate(dc.selectedCard.GetComponent<Cards>().rotation, 0.0f, 0.0f, Space.Self);
                        effects.Add(effect);

                    }
                }
            }
            else if (dc.selectedCard.GetComponent<Cards>().isTeleport)
            {
                GameObject effect = Instantiate(dc.selectedCard.GetComponent<Cards>().effect2,
                    player.transform.position, Quaternion.identity);
                effect.transform.position = new Vector3(effect.transform.position.x,
                    dc.selectedCard.GetComponent<Cards>().particleY2, effect.transform.position.z);
                effect.transform.Rotate(dc.selectedCard.GetComponent<Cards>().rotation, 0.0f, 0.0f, Space.Self);
                effects.Add(effect);
            }
            else
            {
                GameObject effect = Instantiate(dc.selectedCard.GetComponent<Cards>().effect, player.transform.position,
                    Quaternion.identity);
                effect.transform.position = new Vector3(effect.transform.position.x,
                    dc.selectedCard.GetComponent<Cards>().particleY, effect.transform.position.z);
                effect.transform.Rotate(dc.selectedCard.GetComponent<Cards>().rotation, 0.0f, 0.0f, Space.Self);
                effects.Add(effect);
            }

            yield return new WaitForSeconds(1.0f);
            Destroy(dc.selectedCard);
            for (int i = 0; i < enm.attackAreas.Count; i++)
            {
                enm.attackAreas[i].GetComponent<MeshRenderer>().enabled = true;
                enm.attackAreas[i].GetComponent<BoxCollider>().enabled = true;
            }
            
            player.GetComponent<Animator>().SetInteger("Index", 0);

            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            dc.skip = false;
        }
        if (playerCollision)
        {
            P1_HP = (int)(P1_HP - (p2damage + (p2damage * p2attackBuff)) - (p2damage * p1defenseBuff));
        }
        if (enemyCollision)
        {
            P2_HP = (int)(P2_HP - (p1damage + (p1damage * p1attackBuff)) - (p1damage * p2defenseBuff));
        }
        enemyCollision = false;

        playerCollision = false;
        for (int i = 1; i <= 9; i++)
        {
            b.BoardPositions[i].GetComponent<MeshRenderer>().enabled = false;
            b.BoardPositions[i].GetComponent<BoxCollider>().enabled = false;
            b.EnemyPositions[i].GetComponent<MeshRenderer>().enabled = false;
            b.EnemyPositions[i].GetComponent<BoxCollider>().enabled = false;
        }
        dc.attackAreas.Clear();
        enm.attackAreas.Clear();
        yield return new WaitForSeconds(0.5f);
        foreach(GameObject g in effects)
        {
            Destroy(g);
        }
        effects.Clear();
        
       
    }

    
        
    
}
