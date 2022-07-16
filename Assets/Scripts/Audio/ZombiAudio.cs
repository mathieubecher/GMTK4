using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAudio : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioEvent m_attackFoley;
    [SerializeField] private AudioEvent m_hitFoley;
    [SerializeField] private AudioEvent m_nearFoley;

    [Header("Feedbacks")]
    [SerializeField] private AudioEvent m_hitMarkerFB;

    private Zombi m_zombi;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        gameObject.TryGetComponent(out m_zombi);
    }
}
