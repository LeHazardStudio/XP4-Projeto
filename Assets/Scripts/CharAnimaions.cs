using System.Collections;
using System.Collections.Generic;
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

            anim.SetInteger("BuffIndex", Random.Range(0,2));
            anim.SetTrigger("Buff");

            anim.SetInteger("IdleIndex", Random.Range(0, 2));
            anim.SetTrigger("Idle");

            anim.SetInteger("DmgIndex", Random.Range(0, 2));
            anim.SetTrigger("Dmg");

            anim.SetInteger("DrinkIndex", Random.Range(0, 2));
            anim.SetTrigger("Drink");
        }
    }
}
