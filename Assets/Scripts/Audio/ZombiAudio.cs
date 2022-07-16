using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAudio : MonoBehaviour
{
    [Header("Foleys")]
    [SerializeField] private AudioEvent m_impactFoley;

    [Header("SFX")]
    [SerializeField] private AudioEvent m_attackSFX;
    [SerializeField] private AudioEvent m_hitSFX;
    [SerializeField] private AudioEvent m_nearSFX;

    [Header("Feedbacks")]
    [SerializeField] private AudioEvent m_hitMarkerFB;

    private Zombi m_zombi;
    private Transform m_charT;

    private float m_dangerZoneDist = 5f;
    private float m_currentDist;
    private bool m_isInDangerZone;

    private void OnDisable()
    {
        if (m_zombi)
        {
            Eat.OnEat -= OnEat;
            Bullet.OnHitObject -= Bullet_OnHitObject;
        }
    }

    private void Start()
    {
        gameObject.TryGetComponent(out m_zombi);
        if (m_zombi)
        {
            Eat.OnEat += OnEat;
            Bullet.OnHitObject += Bullet_OnHitObject;
        }
        m_charT = FindObjectOfType<Character>().transform;
    }

    private void Update()
    {
        m_currentDist = (m_charT.position - transform.position).magnitude;
        if (m_currentDist < m_dangerZoneDist && !m_isInDangerZone)
        {
            Debug.Log("Enter danger zone");
            m_isInDangerZone = true;
            m_nearSFX.PlayOneShot(gameObject);
        }
        if (m_currentDist > m_dangerZoneDist + 1 && m_isInDangerZone)
        {
            Debug.Log("Exit danger zone");
            m_isInDangerZone = false;
        }
    }

    private void Bullet_OnHitObject(Bullet _bullet, Hitable _hit)
    {
        if (_hit == m_zombi)
        {
            m_hitSFX.PlayOneShot(gameObject);
            m_hitMarkerFB.PlayOneShot(gameObject);
            m_impactFoley.PlayOneShot(gameObject);
        }
    }

    private void OnEat(Zombi _zombi)
    {
        m_attackSFX.PlayOneShot(gameObject);
    }
}
