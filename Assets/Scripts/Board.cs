using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<GameObject> BoardPositions;
    public List<GameObject> EnemyPositions;
    public List<int> positionsIndex;
    public GameObject player;
    public bool pressed;
    public Deck d;
    // Start is called before the first frame update
    void Start()
    {
        d = FindObjectOfType<Deck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walk()
    {
        if (!pressed)
        {
            for (int number = 1; number <= 9; number++)
            {
                float t = Vector3.Distance(player.transform.position, BoardPositions[number].transform.position);
                if (t <= 6.5 && t >= 1.5)
                {
                    BoardPositions[number].GetComponent<MeshRenderer>().enabled = true;
                }
                else
                {
                    BoardPositions[number].GetComponent<MeshRenderer>().enabled = false;
                }
                pressed = true;
            }
        }
        else
        {
            for (int number = 1; number <= 9; number++)
            {
                BoardPositions[number].GetComponent<MeshRenderer>().enabled = false;
            }
            pressed = false;
        }
    }

    public void movePlayer(GameObject g)
    {
        
        if (g.tag == "BoardPositions" && g.GetComponent<MeshRenderer>().enabled)
        {
            player.transform.position = g.transform.position;
            walk();
        }

        
    }

    public void cardAction(GameObject g, GameObject card)
    {
        if (g.tag == "EnemyPositions" && g.GetComponent<MeshRenderer>().enabled)
        {
            StartCoroutine(d.useCard(card));
            d.choosed = true;
            for (int number = 1; number <= 9; number++)
            {
                if (EnemyPositions[number] != g)
                {
                    EnemyPositions[number].GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }
}
