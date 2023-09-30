using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
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

    private void OnTriggerStay(Collider collider)
    {
        print("Inimigo acertou");
        if (collider.gameObject.tag == "Player")
        {
            jm.playerCollision = true;
        }
    }
}
