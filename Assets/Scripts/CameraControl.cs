using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject card;
    bool hitting;
    bool viewingCards;
    Deck d;
    void Start()
    {
        d = FindObjectOfType<Deck>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!viewingCards)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                mainCamera.transform.SetLocalPositionAndRotation(new Vector3(-16.6f, 7.4f, -9.6f), Quaternion.Euler(22.9899979f, 26.4519958f, 338.822021f));
                mainCamera.GetComponent<Camera>().orthographicSize = 2.52f;
                viewingCards = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                mainCamera.transform.SetLocalPositionAndRotation(new Vector3(-4.6f, 7.6f, -12.2f), new Quaternion(0,0,0,0));
                mainCamera.GetComponent<Camera>().orthographicSize = 5.96f;
                viewingCards = false;

            }
        }*/
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast (ray, out hit, 1000))
        {
            if(hit.collider.gameObject.tag == "Card")
            {
                Debug.Log(hit.collider.name);
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
            card.GetComponent<Image>().color = Color.gray;
        }
        else if(hitting == false && card != null)
        {
            card.GetComponent<Image>().color = Color.white;
        }
       
    }

    public void ClickToUse()
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;
        if(Physics.Raycast (ray2, out hit2, 1000))
        {
            Debug.Log(hit2.collider.gameObject.name);
            d.useCard(hit2.collider.gameObject);
        }

    }

    public void ClickToView()
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;
        if(Physics.Raycast (ray2, out hit2, 1000))
        {
            Debug.Log(hit2.collider.gameObject.name);
            d.ViewCard(hit2.collider.gameObject);
        }
    }
}
