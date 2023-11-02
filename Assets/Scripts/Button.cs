using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
   public void Menu() //Se o botão do menu for apertado, ele carrega a cena do jogo
    {
        SceneManager.LoadScene("SampleScene");
    }
}
