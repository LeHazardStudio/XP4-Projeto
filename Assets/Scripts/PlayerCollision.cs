using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public JogoManagement jm;
    // Start is called before the first frame update
    void Start()
    {
        jm = FindObjectOfType<JogoManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter(Collider collider)
    {
        if(this.gameObject.GetComponent<MeshRenderer>().enabled)
        {
            print("trigger");
            if(collider.gameObject.tag == "Player")
            {
                jm.playerCollision = true;
            }
            if(collider.gameObject.tag == "Enemy")
            {
                jm.enemyCollision = true;
            }
        }
    }*/
}
