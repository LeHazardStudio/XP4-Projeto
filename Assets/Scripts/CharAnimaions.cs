using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharAnimaions : MonoBehaviour
{
    private Animator anim;
    private string lastAnimation = "Idle";
    public float angulo = 90.0f;

    // Mapeia as teclas aos nomes das animações e índices
    private Dictionary<KeyCode, (string, string, int)> keyToAnimation = new Dictionary<KeyCode, (string, string, int)>
{
    {KeyCode.Alpha0, ("StartPose", "StartPoseIndex", 3)},//Ice, Fire, Necro
    {KeyCode.Alpha1, ("Buff", "BuffIndex", 2)},//Normal, Olhando p/ jogador
    {KeyCode.Alpha2, ("Debuff", "DebuffIndex", 2)},//Normal, Olhando p/ jogador
    {KeyCode.Alpha3, ("Idle", "", 0)},
    {KeyCode.Alpha4, ("Dmg", "DmgIndex", 3)},// Fire, Ice, Necro
    {KeyCode.Alpha5, ("Drink", "DrinkIndex", 3)},// Normal, Soberbo, Olhando p/ jogador
    {KeyCode.Alpha6, ("Pedra", "PedraIndex", 2)},  
    {KeyCode.Alpha7, ("Victory", "VictoryIndex", 5)}, // Fire, Ice, General, Mockery, Necro
    {KeyCode.Alpha8, ("Tp", "TpIndex", 3)}, // Normal, Praise the sun, nao lembro
    {KeyCode.Alpha9, ("Acertou", "AcertouIndex", 3)},//Normal, Olhando p/ jogador
    {KeyCode.Q, ("Errou", "ErrouIndex", 3)},// Normal, batendo na cara, Olhando p/ jogador
    {KeyCode.W, ("HNormal", "HNIndex", 4)}, // Deboche, Recuando, Olhando p/ jogador, Normal
    {KeyCode.E, ("HitUlt", "HUIndex", 4)},// Normal, Deboche, Olhando p/ jogador, Taunt
    {KeyCode.R, ("Fogo", "FogoIndex", 2)}, // Normal, Ult
    {KeyCode.T, ("Gelo", "GeloIndex", 2)}, // Normal, Ult
    {KeyCode.Y, ("Necro", "NecroIndex", 2)}, // Normal, Ult
    {KeyCode.U, ("Shield", "ShieldIndex", 4)}, // Normal, Gelo, Fogo, Necro
    {KeyCode.I, ("StepSide", "StepSideIndex", 8)} // StepBack, StepBack L, StepBack R, SideStep R->L, StepFront, StepFront L, StepFront R, SideStep L->R
};

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        foreach (var kvp in keyToAnimation)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                string triggerName = kvp.Value.Item1;
                string indexName = kvp.Value.Item2;
                int maxIndex = kvp.Value.Item3;

                if (!string.IsNullOrEmpty(indexName))
                {
                    int index = Random.Range(0, maxIndex);
                    anim.SetInteger(indexName, index);
                }

                Debug.Log(triggerName);
                PlayAnimation(triggerName);

                lastAnimation = triggerName;

            }
        }

    }

    public void OnAcertou()
    {
        anim.SetBool("Acertou", true);
    }

    public void OnErrou()
    {
        anim.SetBool("Errou", true);
    }

    private void PlayAnimation(string triggerName)
    {
        anim.SetBool("Acertou", false);
        anim.SetBool("Errou", false);

        anim.SetTrigger(triggerName);
        if (triggerName != "Idle")
        {
            float desiredRotation = angulo * Mathf.Deg2Rad;
            transform.rotation = Quaternion.Euler(0, angulo, 0);
        }
        lastAnimation = triggerName;
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Idle");
            EnqueueAnimation("Idle", "IdleIndex", Random.Range(0, 3));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Debuff");
            EnqueueAnimation("Debuff", "DebuffIndex", Random.Range(0, 2));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Drink");
            EnqueueAnimation("Drink", "DrinkIndex", Random.Range(0, 3));
            EnqueueAnimation("Buff", "BuffIndex", Random.Range(0, 2));
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animationQueue.Count > 0)
        {
            string nextAnimation = animationQueue.Dequeue();
            PlayAnimation(nextAnimation);
        }
    }

    private void EnqueueAnimation(string triggerName, string indexName, int index)
    {
        anim.SetInteger(indexName, index);
        animationQueue.Enqueue(triggerName);
    }

    private void PlayAnimation(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void OnAnimationComplete()
    {
        Debug.Log("ta chamando");
        if (animationQueue.Count == 0)
        { 
        FinishedAnimation("FinishAnimation", true);
        }
    }

    private void FinishedAnimation(string boolName, bool value)
    {
        anim.SetBool(boolName, value);
    }*/
}
