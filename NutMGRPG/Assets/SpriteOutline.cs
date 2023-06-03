using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOutline : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component
    public Color outlineColor = Color.black;  // Color of the outline
    public float outlineOffset = 0.1f;  // Offset of the outline
    public float outlineThickness = 0.1f;  // Thickness of the outline

    private SpriteRenderer[] outlineRenderers;  // Array to hold the outline SpriteRenderer components

    void Start()
    {
        // Create an array to hold the outline renderers
        outlineRenderers = new SpriteRenderer[4];

        // Create four outline game objects and attach SpriteRenderer components to them
        for (int i = 0; i < 4; i++)
        {
            GameObject outlineObject = new GameObject("Outline" + i);
            outlineObject.transform.SetParent(transform);
            outlineObject.transform.localPosition = Vector3.zero;

            outlineRenderers[i] = outlineObject.AddComponent<SpriteRenderer>();

            // Set the outline's sorting layer and order
            outlineRenderers[i].sortingLayerName = spriteRenderer.sortingLayerName;
            outlineRenderers[i].sortingOrder = spriteRenderer.sortingOrder - 1;

            // Set the outline's sprite to be the same as the main sprite
            outlineRenderers[i].sprite = spriteRenderer.sprite;

            // Set the color of the outline
            outlineRenderers[i].color = outlineColor;

            // Adjust the scale of the outline sprite based on the outline thickness
            float scaleFactor = (spriteRenderer.bounds.size.magnitude + outlineThickness) / spriteRenderer.bounds.size.magnitude;
            outlineRenderers[i].transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }

        // Position the outline game objects around the original sprite with the given outline offset
        Vector3 spritePosition = spriteRenderer.transform.position;

        outlineRenderers[0].transform.position = spritePosition + Vector3.up * outlineOffset;
        outlineRenderers[1].transform.position = spritePosition + Vector3.down * outlineOffset;
        outlineRenderers[2].transform.position = spritePosition + Vector3.left * outlineOffset;
        outlineRenderers[3].transform.position = spritePosition + Vector3.right * outlineOffset;
    }
}