using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public List<GameObject> deckIce;
    public List<GameObject> deckFire;
    public List<GameObject> deckDark;
    public List<GameObject> deck;
    public List<GameObject> drawed;
    public List<GameObject> hand;
    public List<int> get;
    public GameObject deckObject;
    public GameObject deckSelectButton;
    public GameObject gameHud;
    public GameObject center;
    public GameObject selectedCard;
    public GameObject cardHud;
    public GameObject actionHud;
    public GameObject player;
    public GameObject cardEffect;
    public List<GameObject> cardPlaces;
    public List<GameObject> attackAreas;
    public bool decided;
    public bool choosed;
    private int count;
    public bool deckFull;
    public bool draw;
    public bool use;
    public bool viewingCard;
    public bool teleport;
    public int usedCard;
    public bool skip;
    public CameraControl cc;
    public JogoManagement jm;
    public Enemy enm;
    public Image image;
    public Board b;
    

    void Start()
    {
        jm = FindObjectOfType<JogoManagement>();
        cc = FindObjectOfType<CameraControl>();
        enm = FindObjectOfType<Enemy>();
        b = FindObjectOfType<Board>();
        image.gameObject.SetActive(false);
        cardHud.SetActive(false);
        
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            cc.ClickToUse();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            cc.ClickToView();
        }

       /* if(Input.GetKeyDown(KeyCode.R) && drawed.Count < 5)
        {
            DrawCard();
        }*/
        foreach(GameObject a in hand)
        {
            a.transform.position = cardPlaces[hand.IndexOf(a)].transform.position;
        }
        
    }

    
    IEnumerator deckDefine(List<GameObject> deckBase)
    {
        decided = true;
        deckSelectButton.SetActive(false);
        gameHud.SetActive(true);
        while (!deckFull)
        {
            while (deck.Count != deckBase.Count)
            {
                    print("ou");
                    int r = Random.Range(0, 30);
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
                if(drawed.Count == 5)
                {
                    break;
                }
            drawed.Add(i);
            print(drawed.Count);
            GameObject a = Instantiate(i, cardPlaces[drawed.Count - 1].transform.position, cardPlaces[drawed.Count - 1].transform.rotation);
            a.GetComponent<SpriteRenderer>().sprite = i.GetComponent<SpriteRenderer>().sprite;
            a.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            a.name = i.name;  
            a.transform.parent = deckObject.transform;
            hand.Add(a);
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
               if (temp != null && temp.GetComponent<Cards>().mana <= jm.P1_MANA)
               {

                    GameObject demo = Instantiate(temp, center.transform.position, center.transform.rotation);
                    demo.transform.localScale = new Vector3(1, 1, 1);
                    yield return new WaitForSeconds(1.5f);
                    cardEffect = demo.GetComponent<Cards>().effect;
                if (!teleport)
                {
                    if (demo.GetComponent<Cards>().isAttack)
                    {
                        jm.p1damage = demo.GetComponent<Cards>().damage;
                        print(demo.GetComponent<Cards>().attackAreaP1.Count + "contou");
                        
                        for (int i = 0; i < demo.GetComponent<Cards>().attackAreaP1.Count; i++)
                        {
                           
                            print(demo.GetComponent<Cards>().attackAreaP1.Count + "contou");
                            GameObject area = b.EnemyPositions.Find(obj => obj.name == demo.GetComponent<Cards>().attackAreaP1[i].name);
                            if (b.EnemyPositions.Contains(area))
                            {
                                print(demo.GetComponent<Cards>().attackAreaP1.Count + "contou dnv");
                                //area.GetComponent<MeshRenderer>().enabled = true;
                                // area.GetComponent<BoxCollider>().enabled = true;
                                
                                attackAreas.Add(area);
                                
                                /* for (int number = 1; number <= 9; number++)
                                 {
                                     if (b.EnemyPositions[number] != area)
                                     {
                                         if (!attackAreas.Contains(b.EnemyPositions[number]))
                                         {
                                             print("terminou");
                                             b.EnemyPositions[number].GetComponent<MeshRenderer>().enabled = false;
                                             b.EnemyPositions[number].GetComponent<BoxCollider>().enabled = false;
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
                        jm.p1attackBuff = demo.GetComponent<Cards>().attackBuff + jm.p1attackBuff;
                        jm.p1abRounds = demo.GetComponent<Cards>().abRounds;
                        jm.bdActivated = true;
                        jm.p1damage = 0;
                        print("buff");
                    }
                    else if (demo.GetComponent<Cards>().isDefenseBuff)
                    {
                        jm.p1defenseBuff = demo.GetComponent<Cards>().defenseBuff + jm.p1defenseBuff;
                        jm.p1dbRounds = demo.GetComponent<Cards>().dbRounds;
                        jm.bdActivated = true;
                        jm.p1damage = 0;
                        print("buff");
                    }
                    else if (demo.GetComponent<Cards>().isTeleport)
                    {
                        for (int number = 1; number <= 9; number++)
                        {
                            if (b.BoardPositions[number] != g)
                            {
                                b.BoardPositions[number].GetComponent<MeshRenderer>().enabled = true;
                                b.BoardPositions[number].GetComponent<BoxCollider>().enabled = true;

                            }

                        }
                        Destroy(demo);
                        jm.p1damage = 0;
                        print("tp");
                        teleport = true;
                        selectedCard = g;
                        yield break;
                    }
                    else
                    {
                        jm.p1damage = 0;
                        print("mp");
                    }
                    jm.p1cust = demo.GetComponent<Cards>().mana;
                    Destroy(demo);
                    hand.Remove(g);
                    drawed.Remove(temp);
                    //Destroy(g);
                    draw = false;
                    choosed = false;
                    use = true;
                    yield return new WaitForSeconds(1f);
                    enm.ChooseCard();
                }
                else
                {
                    jm.p1cust = demo.GetComponent<Cards>().mana;
                    Destroy(demo);
                    hand.Remove(g);
                    drawed.Remove(temp);
                    //Destroy(g);
                    draw = false;
                    choosed = false;
                    use = true;
                    jm.walk = true;
                    teleport = false;
                    yield return new WaitForSeconds(1f);
                    enm.ChooseCard();
                }
                
               }


            
        }
    }


    public void SelectCard(GameObject g)
    {
        print("clicou em: " + g.name);
        if (!choosed)
        {

            GameObject temp = drawed.Find(obj => obj.name == g.name);
            if (temp != null && temp.GetComponent<Cards>().mana <= jm.P1_MANA)
            {
                selectedCard = g;
                actionHud.gameObject.SetActive(false);
                cardHud.gameObject.SetActive(true);
                image.sprite = g.GetComponent<SpriteRenderer>().sprite;
                image.gameObject.SetActive(true);
                for (int i = 1; i < 9; i++)
                {
                    b.EnemyPositions[i].GetComponent<MeshRenderer>().enabled = false;
                    b.EnemyPositions[i].GetComponent<BoxCollider>().enabled = false;
                }
                if (g.GetComponent<Cards>().isAttack)
                {
                    for (int i = 0; i < g.GetComponent<Cards>().attackAreaP1.Count; i++)
                    {
                        GameObject area = b.EnemyPositions.Find(obj => obj.name == g.GetComponent<Cards>().attackAreaP1[i].name);
                        area.GetComponent<MeshRenderer>().enabled = true;
                        area.GetComponent<BoxCollider>().enabled = true;
                    }
                }
            }
        }
    }

    public void DrawCard()
    {
        if (!draw && deck.Count > 0)
        {
            int r = Random.Range(0, deck.Count);
            GameObject g = deck[r];
            drawed.Add(g);
            print(drawed.Count);
            GameObject a = Instantiate(g, cardPlaces[usedCard].transform.position, cardPlaces[usedCard].transform.rotation);
            a.name = g.name;
            a.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            a.transform.parent = deckObject.transform;
            deck.Remove(g);
            hand.Add(a);
            use = false;
            draw = true;
        }
        else if (deck.Count <= 0)
        {
            use = false;
            draw = true;
        }
    }

    /*public void ViewCard(GameObject g)
    {
        image.sprite = g.GetComponent<SpriteRenderer>().sprite;
        image.gameObject.SetActive(true);
        
    }*/
        
    public void fireDeck()
    {
        if (!decided)
        {
        enm.ChooseDeck();
        StartCoroutine(deckDefine(deckFire));
         
        }
    }

    public void iceDeck()
    {
        if (!decided)
        {
            enm.ChooseDeck();
            StartCoroutine(deckDefine(deckIce));

        }
    }

    public void darkDeck()
    {
        if (!decided)
        {
            enm.ChooseDeck();
            StartCoroutine(deckDefine(deckDark));
        }
    }

    public void skipTurn()
    {
        draw = false;
        use = true;
        jm.p1damage = 0;
        jm.p1cust = 0;
        skip = true;
        selectedCard = null;
        enm.ChooseCard();
        //jm.TurnFinished = true;
    }

    public void SummonCard()
    {
        for (int i = 1; i <= 9; i++)
        {
            b.EnemyPositions[i].GetComponent<MeshRenderer>().enabled = false;
            b.EnemyPositions[i].GetComponent<BoxCollider>().enabled = false;
        }
        StartCoroutine(useCard(selectedCard));
        cardHud.SetActive(false);
        actionHud.SetActive(true);
        
    }

    public void Back()
    {
        cardHud.SetActive(false);
        actionHud.SetActive(true);
        for (int i = 1; i <= 9; i++)
        {
            b.EnemyPositions[i].GetComponent<MeshRenderer>().enabled = false;
            b.EnemyPositions[i].GetComponent<BoxCollider>().enabled = false;
        }
    }

}
