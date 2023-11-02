using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{ //O Script que define os atributos de cada carta
    public int mana; //O custo de mana dela
    public int damage; //O dano dela
    public List<GameObject> attackAreaP1; //As areas que ela ataca do campo do inimigo, ta P1 pq � o player que ta usando
    public List<GameObject> attackAreaP2; //As areas que ela ataca do campo do player, ta P2 pq � o inimigo que ta usando
    public float attackBuff; //Quanto de buff de ataque a carta da
    public float defenseBuff; //Quanto de buff de defesa a carta da
    public float particleY; //A posi��o Y da particula dela
    public float particleY2; //A posi��o Y do segundo efeito de particula dela, caso tenha mais de um
    public int abRounds; //Quantas rodadas dura o buff de ataque
    public int dbRounds; //Quantas rodadas dura o buff de defesa
    public bool isAttack; //Diz se a carta � uma carta de ataque
    public bool isAttackBuff; //Diz se a carta � uma carta de buff de ataque
    public bool isDefenseBuff; //Diz se a carta � uma carta de buff de defesa
    public bool isTeleport; //Diz se a carta � uma carta de teleporte
    public bool isMana; //Diz se a carta � uma carta de mana
    public bool isUltimate; //Diz se a carta � uma carta de ultimate
    public GameObject effect; //O efeito de particula dela
    public GameObject effect2; //O segundo efeito de particula dela, caso tenha mais de um
    public float rotation; //A rota��o do efeito de particula
    public float rotation2; //A rota��o do segundo efeito de particula, caso tenha mais um
    public int indexAnim; //O index da anima��o que ela puxa
    public string trigger; //O trigger da anima��o que ela ativa
    public int numero; //O index da carta, pelo q eu me lembro
    
}
