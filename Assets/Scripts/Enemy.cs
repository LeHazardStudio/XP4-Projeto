using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public List<GameObject> deckIce;
    public List<GameObject> deckFire;
    public List<GameObject> deckDark;
    public List<GameObject> deck;
    public List<GameObject> drawed;
    public List<GameObject> hand;
    public List<int> get;
    public GameObject deckObject;
    public GameObject center;
    public GameObject selectedCard;
    public GameObject enemy;
    public GameObject nextPosition;
    public GameObject boardCenter;
    public List<GameObject> cardPlaces;
    public List<GameObject> attackAreas;
    public List<GameObject> positions;
    public bool decided;
    public bool choosed;
    private int count;
    public bool deckFull;
    public bool draw;
    public bool use;
    public bool viewingCard;
    public bool teleport;
    public int usedCard;
    public CameraControl cc;
    public JogoManagement jm;
    public Sprite backCard;
    public Board b;
   
   


    void Start()
    {
        jm = FindObjectOfType<JogoManagement>();
        cc = FindObjectOfType<CameraControl>();
        b = FindObjectOfType<Board>();
        teleport = false;
        jm.lastBoardEnemy = boardCenter;

    }

    // Update is called once per frame
    void Update()
    {
        /* if (!decided)
         {
             if (Input.GetKeyDown(KeyCode.Alpha1))
             {
                 StartCoroutine(deckDefine(deckIce));
             }
             else if (Input.GetKeyDown(KeyCode.Alpha2))
             {
                 StartCoroutine(deckDefine(deckFire));
             }
             else if (Input.GetKeyDown(KeyCode.Alpha3))
             {
                 StartCoroutine(deckDefine(deckDark));
             }
         }*/

        /* if(Input.GetKeyDown(KeyCode.R) && drawed.Count < 5)
         {
             DrawCard();
         }*/
        foreach (GameObject a in hand)
        {
            a.transform.position = cardPlaces[hand.IndexOf(a)].transform.position;
        }

    }


    IEnumerator deckDefineEn(List<GameObject> deckBase)
    {
        decided = true;
        
       
        while (!deckFull)
        {
            while (deck.Count != deckBase.Count)
            {
                print("ou");
                int r = UnityEngine.Random.Range(0, 30);
                if (!get.Contains(r))
                {


                    deck.Add(deckBase[r]);
                    get.Add(r);

                }
            }
            deckFull = true;
        }
        foreach (GameObject i in deck)
        {
            if (drawed.Count == 5)
            {
                break;
            }
            drawed.Add(i);
            print(drawed.Count);
            hand.Add(i);
            yield return new WaitForSeconds(0.25f);
        }


        while (deck.Count > 25)
        {
            foreach (GameObject i in drawed)
            {
                print("abuble");
                if (deck.Contains(i))
                {

                    deck.Remove(i);
                }
            }

        }
    }

    public IEnumerator useCard(GameObject g)
    {
        print("ta funcionando");
        if (!use)
        {

            GameObject temp = drawed.Find(obj => obj.name == g.name);
            if (temp != null && temp.GetComponent<Cards>().mana <= jm.P2_MANA)
            {

                GameObject demo = Instantiate(temp, center.transform.position, center.transform.rotation);
                demo.transform.localScale = new Vector3(1, 1, 1);
                yield return new WaitForSeconds(0.5f);
                //if (!teleport)
                //{
                    if (demo.GetComponent<Cards>().isAttack)
                    {
                        jm.p2damage = demo.GetComponent<Cards>().damage;
                        print(demo.GetComponent<Cards>().attackAreaP2.Count + "contou");
                        print("Carta inimigo: " + demo.name);
                        for (int i = 0; i < demo.GetComponent<Cards>().attackAreaP2.Count; i++)
                        {
                            print(demo.GetComponent<Cards>().attackAreaP2.Count + "contou");
                            GameObject area = b.BoardPositions.Find(obj => obj.name == demo.GetComponent<Cards>().attackAreaP2[i].name);
                            print("Esse ataque pega em: " + demo.GetComponent<Cards>().attackAreaP2[i].name);
                            if (b.BoardPositions.Contains(area))
                            {
                                print(demo.GetComponent<Cards>().attackAreaP2.Count + "contou dnv inimigo");
                                //area.GetComponent<MeshRenderer>().enabled = true;
                               // area.GetComponent<BoxCollider>().enabled = true;
                                print(area.name + " " + i);
                                attackAreas.Add(area);
                                /*for (int number = 1; number <= 9; number++)
                                {
                                    if (b.BoardPositions[number] != area)
                                    {
                                        if (!attackAreas.Contains(b.BoardPositions[number]))
                                        {
                                            print("terminou");
                                            b.BoardPositions[number].GetComponent<MeshRenderer>().enabled = false;
                                            b.BoardPositions[number].GetComponent<BoxCollider>().enabled = false;
                                        }
                                    }
                                }*/



                            }
                            else
                            {
                                print("error");
                            }

                        }
                        print("attack");
                    }
                    else if (demo.GetComponent<Cards>().isAttackBuff)
                    {
                        jm.p2attackBuff = demo.GetComponent<Cards>().attackBuff + jm.p2attackBuff;
                        jm.p2abRounds = demo.GetComponent<Cards>().abRounds;
                        jm.bdActivated = true;
                        jm.p2damage = 0;
                        print("buff");
                    }
                    else if (demo.GetComponent<Cards>().isDefenseBuff)
                    {
                        jm.p2defenseBuff = demo.GetComponent<Cards>().defenseBuff + jm.p2defenseBuff;
                        jm.p2dbRounds = demo.GetComponent<Cards>().dbRounds;
                        jm.bdActivated = true;
                        jm.p2damage = 0;
                        print("buff");
                    }
                    else if (demo.GetComponent<Cards>().isTeleport)
                    {
                            for (int number = 1; number <= 9; number++)
                            {
                                float t = Vector3.Distance(enemy.transform.position, b.EnemyPositions[number].transform.position);
                                if (t <= 6.5 && t >= 1.5)
                                {
                                    positions.Add(b.EnemyPositions[number]);
                                }

                            }
                            int rb = UnityEngine.Random.Range(0, positions.Count);
                            nextPosition = positions[rb];
                            jm.lastBoardEnemy = nextPosition;
                            positions.Clear();
                       /* Destroy(demo);
                        jm.p2damage = 0;
                        print("tp");
                        selectedCard = g;
                        jm.walk = true;
                        jm.TurnFinished = true;
                        yield break;*/
                    }
                    else
                    {
                        jm.p2damage = 0;
                        print("mp");
                    }
                    jm.p2cust = demo.GetComponent<Cards>().mana;
                    Destroy(demo);
                    hand.Remove(g);
                    drawed.Remove(temp);
                    draw = false;
                    choosed = false;
                    use = true;
                    yield return new WaitForSeconds(1f);
                    jm.TurnFinished = true;
                    
            }
               /* else
                {
                    jm.p2cust = demo.GetComponent<Cards>().mana;
                    Destroy(demo);
                    hand.Remove(g);
                    drawed.Remove(temp);
                    draw = false;
                    choosed = false;
                    use = true;
                    teleport = false;
                    yield return new WaitForSeconds(1f);
                }*/

            




        }
    }

    public void DrawCard()
    {
        if (!draw && deck.Count > 0)
        {
            int r = UnityEngine.Random.Range(0, deck.Count);
            GameObject g = deck[r];
            drawed.Add(g);
            print(drawed.Count);
            deck.Remove(g);
            hand.Add(g);
            use = false;
            draw = true;
        }
        else if (deck.Count <= 0)
        {
            use = false;
            draw = true;
        }
    }


    public void fireDeck()
    {
        if (!decided)
        {
            StartCoroutine(deckDefineEn(deckFire));
        }
    }

    public void iceDeck()
    {
        if (!decided)
        {
            StartCoroutine(deckDefineEn(deckIce));
        }
    }

    public void darkDeck()
    {
        if (!decided)
        {
            StartCoroutine(deckDefineEn(deckDark));
        }
    }

    public void ChooseDeck()
    {
        int r = UnityEngine.Random.Range(1,4);
        if(r == 1)
        {
            fireDeck();
        }
        else if(r == 2)
        {
            iceDeck();
        }
        else
        {
            darkDeck();
        }
    }
 

    public void ChooseCard()
    {
        GameObject choosed = null;
        
        int r = UnityEngine.Random.Range(1, 3);
        if(r == 1)
        {
           for(int number = 1; number <= 9; number++)
            {
                float t = Vector3.Distance(enemy.transform.position, b.EnemyPositions[number].transform.position);
                if(t <= 6.5 && t>= 1.5)
                {
                    positions.Add(b.EnemyPositions[number]);
                }
                
            }
            int rb = UnityEngine.Random.Range(0, positions.Count);
            nextPosition = positions[rb];
            print(nextPosition.name);
            jm.lastBoardEnemy = nextPosition;
            jm.walk = true;
            positions.Clear();
        }
        else
        {
            print("dont walk");
        }
            foreach (GameObject g in drawed)
            { 
                if (g.GetComponent<Cards>().mana < jm.P2_MANA)
                {
                    choosed = g;
                }

            }
        if (choosed != null)
        {
          StartCoroutine(useCard(hand[drawed.IndexOf(choosed)]));
        }
        else
        {
            draw = false;
            use = true;
            jm.P2_MANA = jm.P2_MANA + 5;
            jm.TurnFinished = true;
        }


    }

  
}
