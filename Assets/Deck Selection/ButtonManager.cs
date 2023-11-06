using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ButtonManager : MonoBehaviour
{

    public GameObject boneco;
    
    [SerializeField] private UnityEngine.UI.Button fireBtn;
    [SerializeField] private UnityEngine.UI.Button iceBtn;
    [SerializeField] private UnityEngine.UI.Button darkBtn;

    private void Awake()
    {
        fireBtn.onClick.AddListener(() =>
            {
                boneco.GetComponent<Animator>().SetTrigger("StartPose");
                boneco.GetComponent<Animator>().SetInteger("Index", 1);
                boneco.GetComponent<Animator>().SetInteger("StartPoseIndex", 1);
            }
            );
        iceBtn.onClick.AddListener(() =>
            {
                boneco.GetComponent<Animator>().SetTrigger("StartPose");
                boneco.GetComponent<Animator>().SetInteger("Index", 1);
                boneco.GetComponent<Animator>().SetInteger("StartPoseIndex", 0);
            }
        );
        darkBtn.onClick.AddListener(() =>
            {
                boneco.GetComponent<Animator>().SetTrigger("StartPose");
                boneco.GetComponent<Animator>().SetInteger("Index", 1);
                boneco.GetComponent<Animator>().SetInteger("StartPoseIndex", 2);
            }
        );
    }
}
