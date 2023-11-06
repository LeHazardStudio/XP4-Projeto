using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class ButtonManager : MonoBehaviour
{

    public GameObject boneco;
    public GameObject RightHandEffect;
    
    [SerializeField] private UnityEngine.UI.Button fireBtn;
    [SerializeField] private UnityEngine.UI.Button iceBtn;
    [SerializeField] private UnityEngine.UI.Button darkBtn;
    public GameObject fireEffect;
    public GameObject iceEffect;
    private bool click = false;

    private void Awake()
    {
       
            fireBtn.onClick.AddListener(() =>
                {
                    if (click == false)
                    {
                        click = true;
                        boneco.GetComponent<Animator>().SetTrigger("StartPose");
                        boneco.GetComponent<Animator>().SetInteger("Index", 1);
                        boneco.GetComponent<Animator>().SetInteger("StartPoseIndex", 1);
                        StartCoroutine(anim(1.0f, fireEffect, 3.0f));
                        boneco.transform.position = new Vector3(boneco.transform.position.x, 0, boneco.transform.position.z);
                    }


                }
            );
            iceBtn.onClick.AddListener(() =>
                {
                    if (click == false)
                    {
                        click = true;
                        boneco.GetComponent<Animator>().SetTrigger("StartPose");
                        boneco.GetComponent<Animator>().SetInteger("Index", 1);
                        boneco.GetComponent<Animator>().SetInteger("StartPoseIndex", 0);

                        StartCoroutine(anim(1.5f, iceEffect, 2.5f));
                        boneco.transform.position = new Vector3(boneco.transform.position.x, 0, boneco.transform.position.z);
                    }
                }
            );
            darkBtn.onClick.AddListener(() =>
                {
                    if (click == false)
                    {
                        click = true;
                        boneco.GetComponent<Animator>().SetTrigger("StartPose");
                        boneco.GetComponent<Animator>().SetInteger("Index", 1);
                        boneco.GetComponent<Animator>().SetInteger("StartPoseIndex", 2);
                        boneco.transform.position = new Vector3(boneco.transform.position.x, 0, boneco.transform.position.z);
                    }
                }
            );
            
        
        
    }

    private IEnumerator anim(float inicio, GameObject g, float fim)
    {

        yield return new WaitForSeconds(inicio);
        GameObject efeito = Instantiate(g,new Vector3(boneco.transform.position.x, boneco.transform.position.y, boneco.transform.position.z - 1.0f),g.transform.rotation);
        yield return new WaitForSeconds(fim);
        Destroy(efeito);
        click = false;

    }
}
