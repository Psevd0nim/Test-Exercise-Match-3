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
    private bool canShake = true;

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
        if(_gameManager.GameCondition(gameObject))
            WrongChoose();
    }

    void WrongChoose()
    {
        float time = 1f;
        if (canShake)
        {
            gameObject.transform.DOShakePosition(time, randomness: 10, strength: 0.2f);
            //gameObject.transform.DOShakeRotation(1f);
            canShake = false;
            StartCoroutine(WaitToShakeAgain(time));
        }
    }

    IEnumerator WaitToShakeAgain(float time)
    {
        yield return new WaitForSeconds(time);
        canShake = true;
    }
}