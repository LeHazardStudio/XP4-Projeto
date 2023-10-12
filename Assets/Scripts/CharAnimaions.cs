using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharAnimaions : MonoBehaviour
{
    private Animator anim;
    private IEnumerator Start()
    {
        anim = GetComponent<Animator>();

        while (true)
        {
            yield return new WaitForSeconds(3);

            anim.SetInteger("StartPoseIndex", Random.Range(0, 3)); //Ice, Fire, Necro
            anim.SetTrigger("StartPose");


            anim.SetInteger("BuffIndex", Random.Range(0, 2)); //Normal, Olhando p/ jogador
            anim.SetTrigger("Buff");


            anim.SetInteger("DebuffIndex", Random.Range(0, 2)); //Normal, Olhando p/ jogador
            anim.SetTrigger("Debuff");


            anim.SetInteger("IdleIndex", Random.Range(0, 2)); //Normal, Olhando p/ jogador
            anim.SetTrigger("Idle");

            anim.SetInteger("DmgIndex", Random.Range(0, 3)); // Fire, Ice, Necro
            anim.SetTrigger("Dmg");

            anim.SetInteger("DrinkIndex", Random.Range(0, 3)); //Normal, Soberbo, Olhando p/ jogador
            anim.SetTrigger("Drink");

            anim.SetInteger("PedraIndex", Random.Range(0, 2)); 
            anim.SetTrigger("Pedra");

            anim.SetInteger("VictoryIndex", Random.Range(0, 5)); // Fire, Ice, General, Mockery, Necro
            anim.SetTrigger("Victory");

        }
    }
}
