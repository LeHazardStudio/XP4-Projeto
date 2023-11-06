using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderControl : MonoBehaviour
{
    public GameObject cardView;
    void Start()
    {
        cardView.GetComponent<Image>().material.SetFloat("_Forca", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void activateShader()
    {
        for (float i = 0; i < 20; i++)
        {
            cardView.GetComponent<Image>().material.SetFloat("_Forca", i);
        }
        
    }

   private void twirl()
    {
        cardView.GetComponent<Animator>().SetInteger("AnimInd", 0);
        cardView.GetComponent<Image>().material.SetFloat("_Forca", 0);
    }
}
