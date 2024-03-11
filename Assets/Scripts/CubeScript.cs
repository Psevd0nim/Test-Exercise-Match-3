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
    private GameObject Particle;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(_gameManager.GameCondition(gameObject))
            WrongChoose();
    }

    void WrongChoose()
    {
        if (_gameManager.points[Y, X].position == gameObject.transform.position)
        {
            gameObject.transform.DOShakePosition(1f, randomness: 10, strength: 0.1f);
            //gameObject.transform.DOShakeRotation(1f);
        }
    }

    public void DestroyCube()
    {
        gameObject.transform.DOScale(0.01f, 0.7f).SetLink(gameObject);
        //Instantiate(Particle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}