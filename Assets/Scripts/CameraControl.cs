using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject card;
    bool hitting;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (mainCamera.transform.rotation.x == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                mainCamera.transform.Rotate(60, 0, 0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                mainCamera.transform.Rotate(-60, 0, 0);
            }
        }*/
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast (ray, out hit, 1000))
        {
            if(hit.collider.gameObject.tag == "Card")
            {
                Debug.Log("Colisão");
                hitting = true;
                card = hit.collider.gameObject;
            }
           
           
            
        }
        else
        {
            hitting = false;
        }

        if (hitting)
        {
            card.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if(hitting == false && card != null)
        {
            card.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Click();
        }
    }

    void Click()
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;
        if(Physics.Raycast (ray2, out hit2, 1000))
        {
            Debug.Log(hit2.collider.gameObject.name);
        }
    }
}
