using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Effects : MonoBehaviour
{
    public GameObject fire;
    public GameObject ice;
    public GameObject dark;

    public void criarfire()
    {
        fire.SetActive(true);
    }
    
    public void criarice()
    {
        ice.SetActive(true);
    }
    
    public void criardark()
    {
        dark.SetActive(true);
    }

    public void desligar()
    {
        fire.SetActive(false);
        ice.SetActive(false);
        dark.SetActive(false);

        SceneManager.LoadScene("Game"); 
    }
}
