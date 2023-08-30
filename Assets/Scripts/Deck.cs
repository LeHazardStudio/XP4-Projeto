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
    void Start()
    {
        

        
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
    }

    
    IEnumerator deckDefine(List<GameObject> deckBase)
    {
        decided = true;
        while (!deckFull)
        {
            while (deck.Count != deckBase.Count)
            {
                foreach (GameObject i in deckBase)
                {
                    print("ou");
                    int r = Random.Range(0, 20);
                    if (!get.Contains(r))
                    {
                        deck.Add(deckBase[r]);
                        get.Add(r);

                    }

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
                GameObject a = Instantiate(i, cardPlaces[drawed.IndexOf(i)].transform.position, cardPlaces[drawed.IndexOf(i)].transform.rotation);
                a.transform.parent = deckObject.transform;
                yield return new WaitForSeconds(0.5f);
         }
        

        while (deck.Count > 15)
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

    
}
