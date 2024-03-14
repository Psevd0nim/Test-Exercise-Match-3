using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private GameManager _gameManager;
    public int X;
    public int Y;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject Particle;
    private ParticleSystem.MainModule particleSettings;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        particleSettings = Particle.GetComponent<ParticleSystem>().main;
    }

    private void OnMouseDown()
    {
        if(!_gameManager.GameOverOrWin)
        if(_gameManager.GameCondition(gameObject))
            WrongChoose();
    }

    void WrongChoose()
    {
        if (_gameManager.points[Y, X].position == gameObject.transform.position)
        {
            gameObject.transform.DOShakePosition(1f, randomness: 10, strength: 0.1f);
            SoundManager.Instance.FailClickMethod();
        }
    }

    public void DestroyCube()
    {
        particleSettings.startColor = _spriteRenderer.color;
        Instantiate(Particle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}