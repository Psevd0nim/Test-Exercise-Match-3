using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public GameManager _gameManager;
    public int X;
    public int Y;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        
    }

    private void OnMouseDown()
    {
        _gameManager.GameCondition(gameObject);
    }
}