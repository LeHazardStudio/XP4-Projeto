using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
   public void Menu() //Se o botão do menu for apertado, ele carrega a cena do jogo
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit() //Se o botão do exit for apertado, ele fecha o jogo
    {
        Application.Quit();
    }

    public void Credits() //Se o botão do credits for apertado, ele carrega a cena do credits
    {
        SceneManager.LoadScene("Credits");
    }

    public void Play() //Se o botão do play/playagain for apertado, ele carrega a cena do jogo
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitToMenu() //Se o botão do exittomenu for apertado, ele carrega a cena do menu
    {
        SceneManager.LoadScene("Menu");
    }

}
