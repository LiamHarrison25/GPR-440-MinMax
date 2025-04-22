using System;
using UnityEngine;

public class PieceObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite mainTexSprite;


    void OnEnable()
    {
        spriteRenderer.material.SetFloat("_Thickness", 0);
        spriteRenderer.material.SetTexture("_MainTex", mainTexSprite.texture);
    }

    private void Update()
    {
        //spriteRenderer.material.SetTexture("_MainTex", mainTexSprite.texture);
        //Debug.Log( "Pre-change: " + spriteRenderer.material.GetFloat("_Thickness"));
        
        //spriteRenderer.material.SetFloat("_Thickness", 0);
        
        //Debug.Log("Post-change" + spriteRenderer.material.GetFloat("_Thickness"));
      
    }

    public void OnMouseDown()
    {
        spriteRenderer.material.SetFloat("_Thickness", 1);
    }

    public void OnMouseUp()
    {
        spriteRenderer.material.SetFloat("_Thickness", 0);
    }
}
