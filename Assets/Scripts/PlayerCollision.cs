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

    private void OnTriggerEnter(Collider collider)
    {
            print("Player acertou");
            print("AAAAAAAAAAAAAAAAAAAAAAAAA");
            jm.enemyCollision = true;
            
    }
}
