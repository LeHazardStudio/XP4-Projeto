using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> deckBase;
    public List<GameObject> deck;
    public List<int> get;
    public GameObject deckObject;
    public List<GameObject> cardPlaces;
    void Start()
    {


        while (deck.Count != deckBase.Count)
        {
            foreach (GameObject i in deckBase)
            {
                print("ou");
                int r = Random.Range(0, 5);
                if (!get.Contains(r))
                {
                    deck.Add(deckBase[r]);
                    get.Add(r);

                }
                
            }
        }
        foreach(GameObject i in deck)
        {
            GameObject a = Instantiate(i, cardPlaces[deck.IndexOf(i)].transform.position, cardPlaces[deck.IndexOf(i)].transform.rotation);
            a.transform.parent = deckObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
