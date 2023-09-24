using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public int mana;
    public int damage; 
    public List<GameObject> attackArea;
    public float attackBuff;
    public float defenseBuff;
    public int abRounds;
    public int dbRounds;
    public bool isAttack;
    public bool isAttackBuff;
    public bool isDefenseBuff;
    public bool isTeleport;
    public bool isMana;
}
