using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FadeObject : MonoBehaviour
{
    public float alpha = 0f;
    public float fadeSpeed = 0.5f;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        alpha += fadeSpeed * Time.deltaTime;
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

        if (alpha >= 1f || alpha <= 0f)
        {
            fadeSpeed = -fadeSpeed;
        }
    }
}