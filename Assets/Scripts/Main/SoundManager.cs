using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    private AudioSource AudioSource;
    [SerializeField] private AudioClip SuccessClick;
    [SerializeField] private AudioClip FailClick;

    private void Start()
    {
        Instance = this;
        AudioSource = GetComponent<AudioSource>();
    }

    public void SuccessClickMethod()
    {
        AudioSource.PlayOneShot(SuccessClick, 1f);
    }

    public void FailClickMethod()
    {
        AudioSource.PlayOneShot(FailClick, 0.25f);
    }
}