using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class ButtonManager : MonoBehaviour
{

    public GameObject boneco;
    
    
    [SerializeField] private UnityEngine.UI.Button fireBtn;
    [SerializeField] private UnityEngine.UI.Button iceBtn;
    [SerializeField] private UnityEngine.UI.Button darkBtn;
    private bool click = false;

    public static int deckN; 

    private void Awake()
    {
        deckN = 0;
       
            fireBtn.onClick.AddListener(() =>
                {
                    if (click == false)
                    {
                        click = true;
                        boneco.GetComponent<Animator>().SetTrigger("StartPose");
                        boneco.GetComponent<Animator>().SetInteger("Index", 1);
                        boneco.GetComponent<Animator>().SetInteger("StartPoseIndex", 1);
                        boneco.transform.position = new Vector3(boneco.transform.position.x, 0, boneco.transform.position.z);
                        deckN = 1;
                    }

                    click = false;
                  


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
                        boneco.transform.position = new Vector3(boneco.transform.position.x, 0, boneco.transform.position.z);
                        deckN = 2;
                    }
                    click = false;
                    
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
                        deckN = 3;
                    }
                    click = false;
                    
                }
            );
            
        
        
    }

    
}
