using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteOutline : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component
    public Color outlineColor = Color.black;  // Color of the outline
    public float outlineOffset = 0.03f;  // Offset of the outline
    public float outlineThickness = 0.03f;  // Thickness of the outline

    private SpriteRenderer[] outlineRenderers;  // Array to hold the outline SpriteRenderer components

    void Start()
    {
        CreateOutlineRenderers();
        PositionOutlineRenderers();
        RotateOutlineRenderers();
    }

    void LateUpdate()
    {
        UpdateOutlineRenderers();
        SetOutlineSprites();
    }

    private void CreateOutlineRenderers()
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
        }
    }

    private void PositionOutlineRenderers()
    {
        // Position the outline game objects around the original sprite with the given outline offset
        Vector3 spritePosition = spriteRenderer.transform.position;

        outlineRenderers[0].transform.position = spritePosition + Vector3.up * outlineOffset;
        outlineRenderers[1].transform.position = spritePosition + Vector3.down * outlineOffset;
        outlineRenderers[2].transform.position = spritePosition + Vector3.left * outlineOffset;
        outlineRenderers[3].transform.position = spritePosition + Vector3.right * outlineOffset;
    }

    private void RotateOutlineRenderers()
    {
        // Rotate the outline to match the sprite's rotation
        Quaternion spriteRotation = spriteRenderer.transform.rotation;
        foreach (SpriteRenderer outlineRenderer in outlineRenderers)
        {
            outlineRenderer.transform.rotation = spriteRotation;
        }
    }

    private void SetOutlineSprites()
    {
        // Set the outline's sprite to match the current frame of the animated sprite
        foreach (SpriteRenderer outlineRenderer in outlineRenderers)
        {
            outlineRenderer.sprite = spriteRenderer.sprite;
            outlineRenderer.color = outlineColor;

            // Adjust the scale of the outline sprite based on the outline thickness
            float scaleFactor = (spriteRenderer.bounds.size.magnitude + outlineThickness) / spriteRenderer.bounds.size.magnitude;
            outlineRenderer.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }
    }

    private void UpdateOutlineRenderers()
    {
        // Update the sorting layer and order of the outline sprites
        foreach (SpriteRenderer outlineRenderer in outlineRenderers)
        {
            outlineRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
            outlineRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
        }
    }
}