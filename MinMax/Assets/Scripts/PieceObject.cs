using System;
using UnityEngine;

public class PieceObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite mainTexSprite;
    [SerializeField] private Color pickUpColor = Color.cyan;
    [SerializeField] private Color placeableColor =  Color.red;

    private Vector3 defaultPosition;
    
    private bool isDragging = false;
    private Vector3 offset;

    private TileObject currentTile;


    void OnEnable()
    {
        spriteRenderer.material.SetFloat("_Thickness", 0);
        spriteRenderer.material.SetTexture("_MainTex", mainTexSprite.texture);
        defaultPosition = transform.position; //transform.TransformPoint(transform.position);
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
        //spriteRenderer.material.SetTexture("_MainTex", mainTexSprite.texture);
        //Debug.Log( "Pre-change: " + spriteRenderer.material.GetFloat("_Thickness"));
        
        //spriteRenderer.material.SetFloat("_Thickness", 0);
        
        //Debug.Log("Post-change" + spriteRenderer.material.GetFloat("_Thickness"));
      
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        TurnOnOutline(pickUpColor);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        TurnOffOutline();
        transform.position = defaultPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Tile"))
        {
            TileObject tile = collision.GetComponent<TileObject>();
            if (tile != null && currentTile != tile)
            {
                TurnOnOutline(placeableColor);
                Debug.Log("Place");
            }
        }
    }

    private void TurnOnOutline(Color color)
    {
        spriteRenderer.material.SetFloat("_Thickness", 1);
        spriteRenderer.material.SetColor("_Color", color);
        spriteRenderer.material.SetTexture("_MainTex", mainTexSprite.texture);
    }

    private void TurnOffOutline()
    {
        spriteRenderer.material.SetFloat("_Thickness", 0);
    }

    public void SetTileObject(TileObject tileObject)
    {
        this.currentTile = tileObject;
    }
}
