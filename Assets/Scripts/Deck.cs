using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public Image image;


    void Start()
    {
        jm = FindObjectOfType<JogoManagement>();
        cc = FindObjectOfType<CameraControl>();
        image.gameObject.SetActive(false);
        
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
            a.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
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
        if (!use)
        {
            GameObject temp = drawed.Find(obj => obj.name == g.name);
            if (temp != null)
            {
                jm.damage = temp.GetComponent<Cards>().damage;
                jm.cust = temp.GetComponent<Cards>().mana;
                hand.Remove(g);
                drawed.Remove(temp);
                Destroy(g);
            }
            draw = false;
            use = true;
            yield return new WaitForSeconds(1f);
            jm.TurnFinished = true;
        
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
            a.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
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

    public void ViewCard(GameObject g)
    {
        image.sprite = g.GetComponent<SpriteRenderer>().sprite;
        image.gameObject.SetActive(true);
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
}