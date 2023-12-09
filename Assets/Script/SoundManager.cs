using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioListener listner;

    public AudioClip itemPickupEffect;

    private static SoundManager instance;
    public static SoundManager Instance => instance;
    public void Awake()
    {
        if (instance == null) // ΩÃ±€≈Ê µÓ∑œ
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }
}