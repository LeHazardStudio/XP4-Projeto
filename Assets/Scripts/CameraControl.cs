using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public GameObject mainCamera; //A camera do jogo
    public GameObject card; //A ultima carta clicada pelo player
    bool hitting; //Esses dois nem são utilizados mas eu n tiro pq sei la vai que acontece algo
    bool viewingCards;
    Deck d; //O script de deck
    Board b; //O script do tabuleiro
    void Start()
    { //Puxa cada script da cena
        d = FindObjectOfType<Deck>();
        b = FindObjectOfType<Board>();
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //O raio criado pelo mouse do player
        RaycastHit hit; //O hit do raio
        if(Physics.Raycast (ray, out hit, 1000))
        {
            if(hit.collider.gameObject.tag == "Card" && hit.collider.gameObject != null) //Verifica que se o player clicou numa carta, se sim:
            {
                hitting = true; //Define que ele clicou
                card = hit.collider.gameObject; //Define a carta
            }
           
           
            
        }
        else //Se não:
        {
            hitting = false; //Define que ele não clicou
        }

       /* if (hitting)
        {
            card.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if(hitting == false && card != null)
        {
            card.GetComponent<SpriteRenderer>().color = Color.white;
        }*/
       
    }

    public void ClickToUse() //A função ativada quando o player clica com o botão esquerdo
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;
        if(Physics.Raycast (ray2, out hit2, 1000)) //Vai registrar tudo que o player clicar
        {
            
            if (b.pressed) //Caso o botão de andar tenha sido apertado
            {
                StartCoroutine(b.movePlayer(hit2.collider.gameObject)); //Ativa a função de mover o player caso ele tenha clicado numa casa ativada do tabuleiro dele
                
            }
            if (b.pressedStone) //Caso o botão de pedra tenha sido apertado
            {
                StartCoroutine(b.useStone(hit2.collider.gameObject)); //Ativa a função de usar a pedra caso ele tenha clicado numa casa ativada do inimigo
            }
           
            if (!d.choosed) //Caso ele ainda não tenha escolhido uma carta
            {
                d.SelectCard(hit2.collider.gameObject); //Ativa a função de selecionar carta caso ele tenha clicado numa carta da mão dele
                //* b.cardAction(hit2.collider.gameObject, d.selectedCard);
                //StartCoroutine(d.useCard(hit2.collider.gameObject));
            }
            if (d.teleport) //Caso ele esteja teleportando
            {
                b.pressed = true; //Diz que ele apertou o botão de movimento
                b.movePlayer(hit2.collider.gameObject); //Ativa a função de mover o player caso ele tenha clicado numa casa ativada do tabuleiro dele
                d.useCard(d.selectedCard); //Ativa a função de usar a carta com a carta que foi selecionada
            }
        }

    }


    public void ClickToView() //A função ativada quando o player clica com o botão direito, acho que ela nem é usada mais
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;
        if(Physics.Raycast (ray2, out hit2, 1000))  //Vai registrar tudo que o player clicar
        {
            Debug.Log(hit2.collider.gameObject.name); //Debuga o objeto que foi clicado
            //d.ViewCard(hit2.collider.gameObject);
        }
        else //Se ele não tiver clicado em nada
        {
            d.image.gameObject.SetActive(false); //Desativa o objeto da imagem grande da carta
        }
    }
}
