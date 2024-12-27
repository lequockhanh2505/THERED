using UnityEngine;

public class SnapZone : MonoBehaviour
{
    public bool isOccupied = false;
    public int requiredBlockType;
    private SpriteRenderer spriteRenderer;
    public Color validColor = Color.green;
    public Color invalidColor = Color.red;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetHighlight(bool highlight, bool isValid)
    {
        if (highlight)
        {
            spriteRenderer.color = isValid ? validColor : invalidColor;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
