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
    public List<GameObject> cardPlaces;
    public bool decided;
    private int count;
    public bool deckFull;
    public bool draw;
    public bool use;
    public bool viewingCard;
    public int usedCard;
    public CameraControl cc;
    public JogoManagement jm;
    public Sprite backCard;
   


    void Start()
    {
        jm = FindObjectOfType<JogoManagement>();
        cc = FindObjectOfType<CameraControl>();
        

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


    IEnumerator deckDefine(List<GameObject> deckBase)
    {
        decided = true;
        
       
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
        if (!use)
        {
            GameObject temp = drawed.Find(obj => obj.name == g.name);
            if (temp != null)
            {

                GameObject demo = Instantiate(temp, center.transform.position, center.transform.rotation);
                demo.transform.localScale = new Vector3(1, 1, 1);
                yield return new WaitForSeconds(0.5f);
                Destroy(demo);
                jm.p2damage = temp.GetComponent<Cards>().damage;
                jm.p2cust = temp.GetComponent<Cards>().mana;
                jm.p2attackBuff = temp.GetComponent<Cards>().attackBuff;
                if (!jm.bdActivated)
                {
                    jm.p2attackBuff = temp.GetComponent<Cards>().attackBuff;
                    jm.p2dbRounds = temp.GetComponent<Cards>().abRounds;
                    jm.bdActivated = true;
                }
                hand.Remove(g);
                drawed.Remove(temp);
                Destroy(g);
                draw = false;
                use = true;
                yield return new WaitForSeconds(1f);
                jm.TurnFinished = true;
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
            StartCoroutine(deckDefine(deckFire));
        }
    }

    public void iceDeck()
    {
        if (!decided)
        {
            StartCoroutine(deckDefine(deckIce));
        }
    }

    public void darkDeck()
    {
        if (!decided)
        {
            StartCoroutine(deckDefine(deckDark));
        }
    }

    public void ChooseDeck()
    {
        int r = Random.Range(1,4);
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
 
            jm.TurnFinished = true;
        }


    }

  
}
