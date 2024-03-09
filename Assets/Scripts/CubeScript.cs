using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private Color _color;
    private SpriteRenderer _spriteRenderer;
    bool freeSpace;
    public GameManager _gameManager;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //private void OnMouseDown()
    //{
    //    Destroy(gameObject);
    //    freeSpace = true;
    //}

    //private void Update()
    //{
    //    if (freeSpace)
    //        SomethingDo();
    //}

    //void SomethingDo()
    //{
    //    Vector2 currentPosition = gameObject.transform.position;
    //    if (currentPosition + new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1f) == null)
    //    {
    //        gameObject.transform.Translate(currentPosition + new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1f));
    //    }
    //}

    private void OnMouseDown()
    {
        var pos = gameObject.transform.position;
        Destroy(gameObject);
        _gameManager.points[(int)pos.y, (int)pos.x].freeSpace = true;
        //SomethingDo(currentPosition);

    }

    void SomethingDo(Vector3 position)
    {
        //Destroy(_gameManager.points[(int)(position.y + 1f), (int)(position.x)].cube);
        //_gameManager.points[(int)(position.y +1f), (int)(position.x)].cube.transform.Translate(position);
        //_gameManager.points[(int)(position.y + 1f), (int)(position.x)].cube.transform.DOMove(position, 1f);
        //_gameManager.points[(int)(position.y), (int)(position.x)].cube = _gameManager.points[(int)(position.y + 1f), (int)(position.x)].cube;
        //_gameManager.points[(int)(position.y + 1f), (int)(position.x)].cube = null;

    }
}