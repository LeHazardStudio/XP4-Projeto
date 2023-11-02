using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Board : MonoBehaviour
{
    public List<GameObject> BoardPositions; //Lista das posi��es do tabuleiro do Player
    public List<GameObject> EnemyPositions; //Lista das posi��es do tabuleiro do Inimigo
    public List<int> positionsIndex; //Lista do Index de cada posi��o 
    public GameObject player; //O player
    public bool pressed; //Verifica se o player ja apertou o bot�o de andar
    public bool pressedStone; //Verifica se o player ja apertou o bot�o de pedra
    public bool pressedShield; //Verifica se o player ja apertou o bot�o de escudo
    public Deck d; //O script de Deck
    public JogoManagement jm; //O script manager
    public Enemy enm; //O script do Inimigo
    // Start is called before the first frame update
    void Start()
    { //Puxa cada script da cena
        d = FindObjectOfType<Deck>();
        jm = FindObjectOfType<JogoManagement>();
        enm = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walk()
    { 
        if (!pressed) // Se o player n�o tiver apertado o bot�o de andar, ele verifica todas as casas que est�o perto do player pelo float t e liga elas
        {
            for (int number = 1; number <= 9; number++)
            {
                float t = Vector3.Distance(player.transform.position, BoardPositions[number].transform.position);
                if (t <= 6.5 && t >= 1.5)
                {
                    BoardPositions[number].GetComponent<MeshRenderer>().enabled = true;
                    BoardPositions[number].GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    BoardPositions[number].GetComponent<MeshRenderer>().enabled = false;
                    BoardPositions[number].GetComponent<BoxCollider>().enabled = false;
                }
                pressed = true;
            }
        }
        else //Se ele j� tiver apertado, ele s� desativa todas as casas
        {
            for (int number = 1; number <= 9; number++)
            {
                BoardPositions[number].GetComponent<MeshRenderer>().enabled = false;
                BoardPositions[number].GetComponent<BoxCollider>().enabled = false;
            }
            pressed = false;
        }
    }

    public IEnumerator movePlayer(GameObject g) //Faz a movimenta��o do player no tabuleiro, a partir da casa que ele clicou
    {
        
        if (g.tag == "BoardPositions" && g.GetComponent<MeshRenderer>().enabled)
        {
            if (d.teleport) //Se foi pelo teleporte:
            {
                jm.playerCollision = false; //Desativa a colis�o do player
                GameObject temp = d.drawed.Find(obj => obj.name == d.selectedCard.name); //Puxa o prefab da carta que o player usou da m�o dele
                GameObject effect = Instantiate(d.selectedCard.GetComponent<Cards>().effect, player.transform.position, Quaternion.identity); //Cria a particula do teleporte
                effect.transform.position = new Vector3(effect.transform.position.x + 2, d.selectedCard.GetComponent<Cards>().particleY, effect.transform.position.z); //Posiciona a particula
                effect.transform.Rotate(0.0f, d.selectedCard.GetComponent<Cards>().rotation, 0.0f, Space.Self); //Roda ela
                jm.p1cust = d.selectedCard.GetComponent<Cards>().mana; //Define o custo de mana do player a partir da carta usada
                d.hand.Remove(d.selectedCard); //Remove a carta da m�o do player
                d.drawed.Remove(temp); //Remove o prefab da lista de prefabs
                //Destroy(g);
                d.draw = false; //Diz que o player ainda n�o comprou uma carta
                d.choosed = false; //Diz que o player ainda n�o escolheu uma carta
                d.use = true; //Diz que o player usou uma carta
                d.teleport = false; //Diz que o player ainda n�o usou teleporte
                jm.lastBoardPlayer = g; //Define a ultima casa que o player foi como a casa selecionada
                jm.walk = true; //Diz que o player andou
                walk(); //Ativa a fun��o do bot�o de andar
                yield return new WaitForSeconds(1.0f); 
                Destroy(effect); //Destroi o efeito
                enm.ChooseCard(); //Diz pro inimigo escolher uma carta
            }
            else //Se n�o foi pelo teleporte:
            {
                jm.lastBoardPlayer = g; //Define a ultima casa que o player foi como a casa selecionada
                jm.walk = true; //Diz que o player andou
                walk(); //Ativa a fun��o do bot�o de andar
            }
        }

        
    }

    public void throwStone()
    {
        if (!pressedStone) // Se o player n�o tiver apertado o bot�o de pedra, ele ativa todas as casas do inimigo
        {
            for (int number = 1; number <= 9; number++)
            {
               
                
                EnemyPositions[number].GetComponent<MeshRenderer>().enabled = true;
                EnemyPositions[number].GetComponent<BoxCollider>().enabled = true;
                pressedStone = true;
            }
        }
        else //Se ele ja tiver apertado, ele desativa todas as casas do inimigo
        {
            for (int number = 1; number <= 9; number++)
            {
                EnemyPositions[number].GetComponent<MeshRenderer>().enabled = false;
                EnemyPositions[number].GetComponent<BoxCollider>().enabled = false;
            }
            pressedStone = false;
        }
    }

    public IEnumerator useStone(GameObject g) //Fun��o para usar a pedra na casa selecionada
    {
        if (!pressedShield) //Ve se ele ja n�o selecionou o escudo
        {
            if (g.tag == "EnemyPositions" && g.GetComponent<MeshRenderer>().enabled) //Ve se realmente foi selecionada uma casa disponivel do inimigo
            {

                d.attackAreas.Add(g); //Adiciona ele na lista das casas que ser�o atacadas
                d.draw = false; //Diz que o player ainda n�o comprou uma carta
                d.choosed = false; //Diz que o player ainda n�o escolheu uma carta
                d.use = true; //Diz que o player usou uma carta
                jm.p1damage = 3; //Define o dano do player nesse turno como 3
                jm.p1cust = 0; //Define o custo de mana do player nesse turno como 0
                jm.rock = true; //Diz que ele usou uma pedra
                d.selectedCard = null; //Coloca a carta selecionada como nulo
                pressedShield = true; //Diz que ele pressionou escudo 
                throwStone(); //Ativa a fun�ao do bot�o da pedra
                yield return new WaitForSeconds(0.2f);
                enm.ChooseCard(); //Diz pro inimigo escolher uma carta
            }
        }
    }


    public void useShield() //Fun�ao ativada quando o player clica no bot�o de escudo
    {
            d.draw = false; //Diz que o player ainda n�o comprou uma carta
            d.choosed = false; //Diz que o player ainda n�o escolheu uma carta
            d.use = true; //Diz que o player usou uma carta
            jm.p1damage = 0; //Define o dano do player nesse turno como 0
            jm.p1cust = 0; //Define o custo de mana do player nesse turno como 0
            jm.shield = true; //Define que o player ativou o escudo
            player.GetComponent<Animator>().SetInteger("Index", 1); //Ativa as anima��es do escudo
            player.GetComponent<Animator>().SetTrigger("Shield");
            
            d.selectedCard = null; //Coloca a carta selecionada como nulo
            enm.ChooseCard(); //Diz pro inimigo escolher uma carta

    }

    public void cardAction(GameObject g, GameObject card) //Fun�ao que ativa a a��o da carta, puxando uma casa do inimigo e a carta
    {
        if (g.tag == "EnemyPositions" && g.GetComponent<MeshRenderer>().enabled) //Ve se realmente foi selecionada uma casa disponivel do inimigo
        {
            StartCoroutine(d.useCard(card)); //Ativa a fun��o para a usar a carta
            d.choosed = true; //Diz que o player escolheu uma carta
            for (int number = 1; number <= 9; number++) //Desativa todas as casas do inimigo
            {
                if (EnemyPositions[number] != g)
                {
                    EnemyPositions[number].GetComponent<MeshRenderer>().enabled = false;
                    BoardPositions[number].GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
}
