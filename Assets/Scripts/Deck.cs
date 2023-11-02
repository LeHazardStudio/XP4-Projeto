using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Deck : MonoBehaviour
{
    public List<GameObject> deckIce; //O deck de gelo, sem estar embaralhado
    public List<GameObject> deckFire; //O deck de fogo, sem estar embaralhado
    public List<GameObject> deckDark; //O deck de dark arts, sem estar embaralhado
    public List<GameObject> deck; //O deck embaralhado depois que o player escolhe o que ele vai usar
    public List<GameObject> drawed; //A lista dos prefabs que estão na mão do player
    public List<GameObject> hand; //A lista dos objetos instaciados que estão na mão do player
    public List<int> get; //O index das cartas que estão na mão do player
    public GameObject deckObject; //O objeto do deckp1 da cena
    public GameObject deckSelectButton; //Os botões da seleção de deck da cena
    public GameObject gameHud; //O hud do jogo
    public GameObject center; //O objeto que representa o centro da tela
    public GameObject selectedCard; //A ultima carta selecionada
    public GameObject cardHud; //O hud da carta (Summon, back, etc.)
    public GameObject actionHud; //O hud de açoes (Walk, rock, etc.)
    public GameObject player; //O player
    public GameObject cardEffect; //O efeito de particula da carta selecionada
    public List<GameObject> cardPlaces; //As posições das cartas na tela
    public List<GameObject> attackAreas; //As areas que serão atacadas no campo do oponente
    public bool decided; //Verifica se o player ja decidiu o deck
    public bool choosed; //Verifica se o player ja escolheu uma carta
    private int count;
    public bool deckFull; //Verifica se o deck embaralhado já esta cheio ou não
    public bool draw; //Verifica se o player ja comprou uma carta
    public bool use; //Verifica se o player ja usou uma carta
    public bool viewingCard; //Verifica se o player esta vendo um carta
    public bool teleport; //Verifica se o teleporte foi usado
    public int usedCard; //Diz o valor da carta usada
    public bool skip; //Verifica se o skip foi usado
    public CameraControl cc; //O script da camera
    public JogoManagement jm; //O script de manager
    public Enemy enm; //O script do inimigo
    public Image image; //A imagem grande da carta
    public Board b; //O script do tabuleiro
    

    void Start()
    { //Puxa cada script da cena, e desativa a imagem grande e o hud da carta
        jm = FindObjectOfType<JogoManagement>();
        cc = FindObjectOfType<CameraControl>();
        enm = FindObjectOfType<Enemy>();
        b = FindObjectOfType<Board>();
        image.gameObject.SetActive(false);
        cardHud.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (!decided)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(deckDefine(deckIce));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(deckDefine(deckFire));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(deckDefine(deckDark));
            }
        }*/

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Se o botão esquerdo for clicado, ativa a função de usar os objetos
        {
            cc.ClickToUse();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) //Se o botão direito for clicado, ativa a função de ver os objetos
        {
            cc.ClickToView();
        }

       /* if(Input.GetKeyDown(KeyCode.R) && drawed.Count < 5)
        {
            DrawCard();
        }*/
        foreach(GameObject a in hand) //Pra cada carta na mão, ele coloca a carta na posição da tela equivalente ao seu index, e ve se o player tem mana pra usar ela ou não alterando o tamanho dela dependendo do resultado
        {
            a.transform.position = cardPlaces[hand.IndexOf(a)].transform.position;

            if (a != null && a.GetComponent<Cards>().mana > jm.P1_MANA){
                a.transform.localScale = new Vector3 ( 0.45f, 0.45f, 0.45f );
                //a.GetComponent<SpriteRenderer>().color = Color.black; 
            }

            else {
                a.transform.localScale = new Vector3 ( 0.6f, 0.6f, 0.6f);

            }
        }
        
    }

    
    IEnumerator deckDefine(List<GameObject> deckBase) //Função que embaralha o deck selecionado e entrega a primeira mão
    {
        decided = true; //Diz que o player escolheu o deck
        deckSelectButton.SetActive(false);  //Desativa os botões de seleção de deck
        gameHud.SetActive(true); //Ativa o hud do jogo
        while (!deckFull) //Enquanto o deck embaralhado não estiver cheio ele pega uma carta aleatoria do deck original e joga pro embaralhado 
        {
            while (deck.Count != deckBase.Count)
            {
                    print("ou");
                    int r = Random.Range(0, 30);
                    if (!get.Contains(r))
                    {
                        
                     
                        deck.Add(deckBase[r]);
                        get.Add(r);

                    }
            }
            deckFull = true;
        }
         foreach (GameObject i in deck) //Enquanto o numero de cartas na mao não for 5, ele puxa a primeira carta do deck e adiciona ela na lista de prefabs, depois instancia ela na tela e adiciona o objeto novo na lista dos instaciados
         {
                if(drawed.Count == 5)
                {
                    break;
                }
            drawed.Add(i);
            print(drawed.Count);
            GameObject a = Instantiate(i, cardPlaces[drawed.Count - 1].transform.position, cardPlaces[drawed.Count - 1].transform.rotation);
            a.GetComponent<SpriteRenderer>().sprite = i.GetComponent<SpriteRenderer>().sprite;
            a.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            a.name = i.name;  
            a.transform.parent = deckObject.transform;
            hand.Add(a);
            yield return new WaitForSeconds(0.25f);
         }
        

        while (deck.Count > 25) //Enquanto ele não tiver removido 5 cartas do deck, ele remove as cartas que forem puxadas do deck
        {
            foreach (GameObject i in drawed)
            {
                print("abuble");
                if (deck.Contains(i))
                {
                    
                    deck.Remove(i);
                }
            }
            
        }
        
    }

    public IEnumerator useCard(GameObject g) //A função que usa a carta selecionada
    {
        int rnd = Random.Range(0, 4);
        print("ta funcionando");
        if (!use) //Verifica se nenhuma carta ja foi usado, caso não: 
        {
            
                GameObject temp = drawed.Find(obj => obj.name == g.name); //Puxa o prefab da carta selecionada
               if (temp != null && temp.GetComponent<Cards>().mana <= jm.P1_MANA) //Ve se tem mana pra usar ela
               {
                    player.GetComponent<Animator>().SetInteger("Index", temp.GetComponent<Cards>().indexAnim);
                    player.GetComponent<Animator>().SetTrigger(temp.GetComponent<Cards>().trigger); //Ativa a animação dela
                    if (g.GetComponent<Cards>().numero == 1)
                    {
                        player.GetComponent<Animator>().SetInteger("Index", rnd);
                    }

                    GameObject demo = Instantiate(temp, center.transform.position, center.transform.rotation); //Instancia ela no meio da tela
                    demo.transform.localScale = new Vector3(1, 1, 1);
                    yield return new WaitForSeconds(1.5f);
                    cardEffect = demo.GetComponent<Cards>().effect; //Seta o efeito da carta
                    
                if (!teleport) //Se teleporte ainda não tiver sido usado: 
                {
                    if (demo.GetComponent<Cards>().isAttack) //Verifica se é uma carta de ataque, se for: 
                    {
                        jm.p1damage = demo.GetComponent<Cards>().damage; //Seta o dano do player como o da carta
                        print(demo.GetComponent<Cards>().attackAreaP1.Count + "contou");
                        
                        for (int i = 0; i < demo.GetComponent<Cards>().attackAreaP1.Count; i++) //Adiciona todas as areas de ataque da carta na lista das areas que vão ser atacadas
                        {
                           
                            print(demo.GetComponent<Cards>().attackAreaP1.Count + "contou");
                            GameObject area = b.EnemyPositions.Find(obj => obj.name == demo.GetComponent<Cards>().attackAreaP1[i].name);
                            if (b.EnemyPositions.Contains(area))
                            {
                                print(demo.GetComponent<Cards>().attackAreaP1.Count + "contou dnv");
                                //area.GetComponent<MeshRenderer>().enabled = true;
                                // area.GetComponent<BoxCollider>().enabled = true;
                                
                                attackAreas.Add(area);
                                
                                /* for (int number = 1; number <= 9; number++)
                                 {
                                     if (b.EnemyPositions[number] != area)
                                     {
                                         if (!attackAreas.Contains(b.EnemyPositions[number]))
                                         {
                                             print("terminou");
                                             b.EnemyPositions[number].GetComponent<MeshRenderer>().enabled = false;
                                             b.EnemyPositions[number].GetComponent<BoxCollider>().enabled = false;
                                         }
                                     }
                                 }*/



                            }
                            else
                            {
                                print("error");
                            }

                        }
                        print("attack");
                    }
                    else if (demo.GetComponent<Cards>().isAttackBuff) //Se for uma carta de buff de ataque
                    {
                        jm.p1attackBuff = demo.GetComponent<Cards>().attackBuff + jm.p1attackBuff; //Adiciona o buff da carta nos buffs do player
                        jm.p1abRounds = demo.GetComponent<Cards>().abRounds; //Seta a quantidade de rounds do buff
                        jm.bdActivated = true; //Diz que um buff foi ativado
                        jm.p1damage = 0; //Diz que o dano é 0
                        print("buff");
                    }
                    else if (demo.GetComponent<Cards>().isDefenseBuff) //Se for uma carta de buff de defesa
                    {
                        jm.p1defenseBuff = demo.GetComponent<Cards>().defenseBuff + jm.p1defenseBuff; //Adiciona o buff da carta nos buffs do player
                        jm.p1dbRounds = demo.GetComponent<Cards>().dbRounds; //Seta a quantidade de rounds do buff
                        jm.bdActivated = true; //Diz que um buff foi ativado
                        jm.p1damage = 0; //Diz que o dano é 0
                        print("buff");
                    }
                    else if (demo.GetComponent<Cards>().isTeleport) //Se for uma carta de teleporte:
                    {
                        for (int number = 1; number <= 9; number++) //Ativa todas as casas do player
                        {
                            if (b.BoardPositions[number] != g)
                            {
                                b.BoardPositions[number].GetComponent<MeshRenderer>().enabled = true;
                                b.BoardPositions[number].GetComponent<BoxCollider>().enabled = true;

                            }

                        }
                        Destroy(demo); //Destroi o objeto instaciado no centro
                        jm.p1damage = 0; //Diz que o dano é 0
                        print("tp");
                        teleport = true; //Diz que teleporte foi ativado
                        selectedCard = g; //Seta a carta selecionada
                        print("tp: " + selectedCard.name);
                        yield break;
                    }
                    else //Se for uma carta de mana:
                    {
                        jm.p1damage = 0; //Diz que dano é 0
                        print("mp");
                    }
                    jm.p1cust = demo.GetComponent<Cards>().mana; //Seta o custo do player como o da carta
                    Destroy(demo); //Destroi o objeto instanciado no centro
                 
                    drawed.Remove(temp); //Remove o prefab da lita
                    //Destroy(g);
                    draw = false; //Diz que o player ainda não comprou uma carta
                    choosed = false; //Diz que o player ainda não escolheu uma carta
                    use = true; //Diz que o player usou uma carta
                    yield return new WaitForSeconds(1f);
                    enm.ChooseCard(); //Fala pro inimigo escolher uma carta
                }
               /* else
                {
                    jm.p1cust = demo.GetComponent<Cards>().mana;
                    Destroy(demo);
                    hand.Remove(g);
                    drawed.Remove(temp);
                    //Destroy(g);
                    draw = false;
                    choosed = false;
                    use = true;
                    jm.walk = true;
                    teleport = false;
                    yield return new WaitForSeconds(1f);
                    enm.ChooseCard();
                }*/
                
               }


            
        }
    }


    public void SelectCard(GameObject g) //Função ativa quando o player seleciona a carta na mão dele
    {
        print("clicou em: " + g.name);
        if (!choosed) //Ve se ele ja nao escolheu uma carta, caso não:
        {

            GameObject temp = drawed.Find(obj => obj.name == g.name); //Puxa o prefab da carta
            if (temp != null && temp.GetComponent<Cards>().mana <= jm.P1_MANA) //Ve se tem mana suficiente
            {
                selectedCard = g; //Define a carta seleciona
                actionHud.gameObject.SetActive(false); //Desativa o hud de açoes
                cardHud.gameObject.SetActive(true); //Ativa o hud da carta
                image.sprite = g.GetComponent<SpriteRenderer>().sprite; //Seta a imagem grande da carta como a carta
                image.gameObject.SetActive(true); //Ativa a imagem grande
                for (int i = 1; i < 10; i++) //Desativa todos as casas do tabuleiro do inimigo, pra não bugar caso varias cartas sejam selecionadas em sequencia
                {
                    b.EnemyPositions[i].GetComponent<MeshRenderer>().enabled = false;
                    b.EnemyPositions[i].GetComponent<BoxCollider>().enabled = false;
                }
                if (g.GetComponent<Cards>().isAttack) //Ve se é uma carta de ataque, se for: 
                {
                    for (int i = 0; i < g.GetComponent<Cards>().attackAreaP1.Count; i++) //Ativa as casas do inimigo que ela ataca
                    {
                        GameObject area = b.EnemyPositions.Find(obj => obj.name == g.GetComponent<Cards>().attackAreaP1[i].name);
                        area.GetComponent<MeshRenderer>().enabled = true;
                        area.GetComponent<BoxCollider>().enabled = true;
                    }
                }
            }
        }
    }

    public void DrawCard() //Função que faz a compra da carta no inicio do turno, funciona igual a de definir deck mas só pra uma carta inves de 5
    {
        if (!draw && deck.Count > 0)
        {
            int r = Random.Range(0, deck.Count);
            GameObject g = deck[r];
            drawed.Add(g);
            print(drawed.Count);
            GameObject a = Instantiate(g, cardPlaces[usedCard].transform.position, cardPlaces[usedCard].transform.rotation);
            a.name = g.name;
            a.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            a.transform.parent = deckObject.transform;
            deck.Remove(g);
            hand.Add(a);
            use = false;
            draw = true;
        }
        else if (deck.Count <= 0)
        {
            use = false;
            draw = true;
        }
    }

    /*public void ViewCard(GameObject g)
    {
        image.sprite = g.GetComponent<SpriteRenderer>().sprite;
        image.gameObject.SetActive(true);
        
    }*/
        
    public void fireDeck() //Escolhe o deck de fogo
    {
        if (!decided)
        {
        enm.ChooseDeck();
        StartCoroutine(deckDefine(deckFire));
         
        }
    }

    public void iceDeck() //Escolhe o deck de gelo
    {
        if (!decided)
        {
            enm.ChooseDeck();
            StartCoroutine(deckDefine(deckIce));

        }
    }

    public void darkDeck() //Escolhe o deck de dark arts
    {
        if (!decided)
        {
            enm.ChooseDeck();
            StartCoroutine(deckDefine(deckDark));
        }
    }

    public void skipTurn() //Pula o turno, zerando todos os valores e dizendo que o player fez uma ação, então mandando o inimigo escolher uma carta
    {
        draw = false;
        use = true;
        jm.p1damage = 0;
        jm.p1cust = 0;
        skip = true;
        selectedCard = null;
        enm.ChooseCard();
        //jm.TurnFinished = true;
    }

    public void SummonCard() //Função do botão summon
    {
        for (int i = 1; i <= 9; i++) //Desativa toda as casas do inimigo
        {
            b.EnemyPositions[i].GetComponent<MeshRenderer>().enabled = false;
            b.EnemyPositions[i].GetComponent<BoxCollider>().enabled = false;
        }
        StartCoroutine(useCard(selectedCard)); //Ativa a função de usar a carta
        cardHud.SetActive(false); //Desativa o hud da carta
        actionHud.SetActive(true); //Ativa o hud de ações
        
    }

    public void Back() //Função do botão de voltar da carta
    {
        cardHud.SetActive(false); //Desativa o hud da carta
        actionHud.SetActive(true); //Ativa o hud de ações
        for (int i = 1; i <= 9; i++) //Desativa todas as casas do inimigo
        {
            b.EnemyPositions[i].GetComponent<MeshRenderer>().enabled = false;
            b.EnemyPositions[i].GetComponent<BoxCollider>().enabled = false;
        }
    }

}
