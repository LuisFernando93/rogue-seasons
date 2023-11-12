using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropTransparency : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float opacity = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Prop") && collision.isTrigger)
        {
            spriteRenderer = collision.GetComponent<SpriteRenderer>();
            if (collision.GetComponent<SpriteRenderer>() == null) ChangeOpacityInChild(collision.transform, 0);
            else ChangeOpacity(spriteRenderer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Prop") && collision.isTrigger)
        {
            spriteRenderer = collision.GetComponent<SpriteRenderer>();
            if (collision.GetComponent<SpriteRenderer>() == null) ChangeOpacityInChild(collision.transform, 1);
            else ResetOpacity(spriteRenderer);
        }
    }

    private void ChangeOpacity(SpriteRenderer sr)
    {
        Color spriteColor = sr.color;
        spriteColor.a = opacity;
        sr.color = spriteColor;
    }

    private void ResetOpacity(SpriteRenderer sr)
    {
        Color spriteColor = sr.color;
        spriteColor.a = 255;
        sr.color = spriteColor;
    }

    //Usado em casos como a arvore que possui childs diferentes compondo ela
    //0 para setar opacidade, 1 para resetar
    private void ChangeOpacityInChild(Transform parent, int i)
    {
        Debug.Log("a");
        foreach(Transform child in parent)
        {
            if (i == 0)
            {
                if (child.GetComponent<SpriteRenderer>() != null) ChangeOpacity(child.GetComponent<SpriteRenderer>());

            }
            else
            {
                if (child.GetComponent<SpriteRenderer>() != null) ResetOpacity(child.GetComponent<SpriteRenderer>());
            }
        }
    }
}
