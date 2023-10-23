using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public int mana;
    public int damage; 
    public List<GameObject> attackAreaP1;
    public List<GameObject> attackAreaP2;
    public float attackBuff;
    public float defenseBuff;
    public float particleY;
    public int abRounds;
    public int dbRounds;
    public bool isAttack;
    public bool isAttackBuff;
    public bool isDefenseBuff;
    public bool isTeleport;
    public bool isMana;
    public GameObject effect;
    public float rotation; 
}
