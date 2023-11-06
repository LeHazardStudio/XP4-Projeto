using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
   public void Menu() //Se o bot�o do menu for apertado, ele carrega a cena do jogo
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit() //Se o bot�o do exit for apertado, ele fecha o jogo
    {
        Application.Quit();
    }

    public void Credits() //Se o bot�o do credits for apertado, ele carrega a cena do credits
    {
        SceneManager.LoadScene("Credits");
    }

    public void Play() //Se o bot�o do play/playagain for apertado, ele carrega a cena do jogo
    {
        SceneManager.LoadScene("DeckSelection");
    }

    public void ExitToMenu() //Se o bot�o do exittomenu for apertado, ele carrega a cena do menu
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void Tutorial() //Se o bot�o do exittomenu for apertado, ele carrega a cena do menu
    {
        SceneManager.LoadScene("Tutorial");
    }

}
