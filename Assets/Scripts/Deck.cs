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
    public List<GameObject> cardPlaces;
    public bool decided;
    private int count;
    public bool deckFull;
    public bool draw;
    public bool use;
    public bool viewingCard;
    public int usedCard;
    public CameraControl cc;
    public Image image;
    void Start()
    {

        cc = FindObjectOfType<CameraControl>();
        image.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!decided)
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
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            cc.ClickToUse();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            cc.ClickToView();
        }

        if(Input.GetKeyDown(KeyCode.R) && drawed.Count < 5)
        {
            DrawCard();
        }
        foreach(GameObject a in hand)
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
                if(drawed.Count == 5)
                {
                    break;
                }
            drawed.Add(i);
            print(drawed.Count);
            GameObject a = Instantiate(i, cardPlaces[drawed.Count - 1].transform.position, cardPlaces[drawed.Count - 1].transform.rotation);
            a.GetComponent<Image>().sprite = a.GetComponent<SpriteRenderer>().sprite;
            a.transform.localScale = new Vector3(10.2f, 10.2f, 10.2f);
            a.name = i.name;  
            a.transform.parent = deckObject.transform;
            hand.Add(a);
            yield return new WaitForSeconds(0.5f);
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

    public void useCard(GameObject g)
    {
        if (!use)
        {
            GameObject temp = drawed.Find(obj => obj.name == g.name);
            if (temp != null)
            {
                hand.Remove(g);
                drawed.Remove(temp);
                Destroy(g);
            }
            draw = false;
            use = true;
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
            a.transform.parent = deckObject.transform;
            deck.Remove(g);
            hand.Add(a);
            use = false;
            draw = true;
        }
        else if (deck.Count <= 0)
        {
            use = false;
        }
    }

    public void ViewCard(GameObject g)
    {
        image.sprite = g.GetComponent<SpriteRenderer>().sprite;
        image.gameObject.SetActive(true);
    }
        
    
}
