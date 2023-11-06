using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ControllerRegras : MonoBehaviour
{
    public List<Sprite> tutorial;
    public GameObject imagem;
    public int iterador = 0;

    public void trocaImag()
    {
        iterador++;
        if (iterador>tutorial.Count)
        {
            SceneManager.LoadScene("Menu");
        }
        
        imagem.GetComponent<Image>().sprite= tutorial[iterador];
    }
}
