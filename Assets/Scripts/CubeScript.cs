using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private Color _color;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}