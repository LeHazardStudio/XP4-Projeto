using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Board : MonoBehaviour
{
    public List<GameObject> BoardPositions;
    public List<GameObject> EnemyPositions;
    public List<int> positionsIndex;
    public GameObject player;
    public bool pressed;
    public bool pressedStone;
    public Deck d;
    public JogoManagement jm;
    public Enemy enm;
    // Start is called before the first frame update
    void Start()
    {
        d = FindObjectOfType<Deck>();
        jm = FindObjectOfType<JogoManagement>();
        enm = FindObjectOfType<Enemy>();
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
                    BoardPositions[number].GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    BoardPositions[number].GetComponent<MeshRenderer>().enabled = false;
                    BoardPositions[number].GetComponent<BoxCollider>().enabled = false;
                }
                pressed = true;
            }
        }
        else
        {
            for (int number = 1; number <= 9; number++)
            {
                BoardPositions[number].GetComponent<MeshRenderer>().enabled = false;
                BoardPositions[number].GetComponent<BoxCollider>().enabled = false;
            }
            pressed = false;
        }
    }

    public void movePlayer(GameObject g)
    {
        
        if (g.tag == "BoardPositions" && g.GetComponent<MeshRenderer>().enabled)
        {
            jm.lastBoardPlayer = g;
            jm.walk = true;
            walk();
        }

        
    }

    public void throwStone()
    {
        if (!pressedStone)
        {
            for (int number = 1; number <= 9; number++)
            {
               
                
                EnemyPositions[number].GetComponent<MeshRenderer>().enabled = true;
                EnemyPositions[number].GetComponent<BoxCollider>().enabled = true;
                pressedStone = true;
            }
        }
        else
        {
            for (int number = 1; number <= 9; number++)
            {
                EnemyPositions[number].GetComponent<MeshRenderer>().enabled = false;
                EnemyPositions[number].GetComponent<BoxCollider>().enabled = false;
            }
            pressedStone = false;
        }
    }

    public IEnumerator useStone(GameObject g)
    {
        if (g.tag == "EnemyPositions" && g.GetComponent<MeshRenderer>().enabled)
        {
            d.attackAreas.Clear();
            d.attackAreas.Add(g);
            d.draw = false;
            d.choosed = false;
            d.use = true;
            throwStone();
            yield return new WaitForSeconds(1f);
            enm.ChooseCard();
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
                    BoardPositions[number].GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
}
