using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> deckIce;
    public List<GameObject> deckFire;
    public List<GameObject> deckDark;
    public List<GameObject> deck;
    public List<GameObject> drawed;
    public List<int> get;
    public GameObject deckObject;
    public List<GameObject> cardPlaces;
    public bool decided;
    private int count;
    public bool deckFull;
    public int usedCard;
    public CameraControl cc;
    void Start()
    {

        cc = FindObjectOfType<CameraControl>();
        
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
            cc.Click();
        }

        if(Input.GetKeyDown(KeyCode.R) && drawed.Count < 5)
        {
            DrawCard();
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
                a.name = i.name;
                a.transform.parent = deckObject.transform;
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
        
        GameObject temp = drawed[drawed.IndexOf(g)];
        if (temp != null)
        {
            usedCard = drawed.IndexOf(temp);
            Destroy(g);
            drawed.RemoveAt(usedCard);
        }
    }

    public void DrawCard()
    {
        int r = Random.Range(0, deck.Count);
        GameObject g = deck[r];
        drawed.Add(g);
        print(drawed.Count);
        GameObject a = Instantiate(g, cardPlaces[usedCard].transform.position, cardPlaces[usedCard].transform.rotation);
        a.name = g.name;
        a.transform.parent = deckObject.transform;
        deck.Remove(g);
    }
    
}
