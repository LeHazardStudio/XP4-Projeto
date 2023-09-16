using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<GameObject> BoardPositions;
    public List<int> positionsIndex;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (positionsIndex.Contains(GetPressedNumber())){
            int i = GetPressedNumber();
            float t = Vector3.Distance(player.transform.position, BoardPositions[i].transform.position);
            print(t);
            if(t <= 5.3)
            {
                player.transform.position = BoardPositions[i].transform.position;
            }
        }
    }

    public int GetPressedNumber()
    {
        for (int number = 0; number <= 9; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
                return number;
        }

        return -1;
    }
}
