using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharAnimaions : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation("StartPose", "StartPoseIndex", Random.Range(0, 3)); //Ice, Fire, Necro
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            PlayAnimation("Buff", "BuffIndex", Random.Range(0, 2)); //Normal, Olhando p/ jogador
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Debuff");
            PlayAnimation("Debuff", "DebuffIndex", Random.Range(0, 2)); //Normal, Olhando p/ jogador
        }

    else if (Input.GetKeyDown(KeyCode.I))
    {
        Debug.Log("Idle");
        PlayAnimation("Idle", "IdleIndex", Random.Range(0, 2)); //Normal, Olhando p/ jogador
    }

    else if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        Debug.Log("Dmg");
        PlayAnimation("Dmg", "DmgIndex", Random.Range(0, 3)); // Fire, Ice, Necro
    }

    else if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        Debug.Log("Drink");
        PlayAnimation("Drink", "DrinkIndex", Random.Range(0, 3)); // Normal, Soberbo, Olhando p/ jogador
    }

    else if (Input.GetKeyDown(KeyCode.P))
    {
        Debug.Log("Pedra");
        PlayAnimation("Pedrao", "PedraIndex", Random.Range(0, 2)); 
    }

    else if (Input.GetKeyDown(KeyCode.V))
    {
        Debug.Log("Victory");
        PlayAnimation("Victory", "VictoryIndex", Random.Range(0, 5)); // Fire, Ice, General, Mockery, Necro
    }
    else if (Input.GetKeyDown(KeyCode.T))
    {
        Debug.Log("Tp");
        PlayAnimation("Tp", "TpIndex", Random.Range(0, 3)); // Normal, Praise the sun, nao lembro
    }
    else if (Input.GetKeyDown(KeyCode.A))
    {
        Debug.Log("Acertou");
        PlayAnimation("Acertou", "AcertouIndex", Random.Range(0, 3)); // Normal, felicidade, Olhando p/ jogador
    }
    else if (Input.GetKeyDown(KeyCode.E))
    {
        Debug.Log("Errou");
        PlayAnimation("Errou", "ErrouIndex", Random.Range(0, 3)); // Normal, batendo na cara, Olhando p/ jogador
    }
    else if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        Debug.Log("HNormal");
        PlayAnimation("HNormal", "HNIndex", Random.Range(0, 4)); // Deboche, Recuando, Olhando p/ jogador, Normal
    }
    else if (Input.GetKeyDown(KeyCode.Alpha4))
    {
        Debug.Log("HitUlt");
        PlayAnimation("HitUlt", "HUIndex", Random.Range(0, 4)); // Normal, Deboche, Olhando p/ jogador, Taunt
    }
    else if (Input.GetKeyDown(KeyCode.F))
    {
        Debug.Log("Fogo");
        PlayAnimation("Fogo", "FogoIndex", Random.Range(0, 2)); // Normal, Ult
    }
    else if (Input.GetKeyDown(KeyCode.G))
    {
        Debug.Log("Gelo");
        PlayAnimation("Gelo", "GeloIndex", Random.Range(0, 2)); // Normal, Ult
    }
    else if (Input.GetKeyDown(KeyCode.N))
    {
        Debug.Log("Necro");
        PlayAnimation("Necro", "NecroIndex", Random.Range(0, 2)); // Normal, Ult
        }


}
    private void PlayAnimation(string triggerName, string indexName, int index)
    {
        anim.SetInteger(indexName, index);
        anim.SetTrigger(triggerName);
    }
}
