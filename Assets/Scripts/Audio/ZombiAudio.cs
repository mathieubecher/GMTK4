using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAudio : MonoBehaviour
{
    [Header("Foleys")]
    [SerializeField] private AudioEvent m_impactFoley;
    [SerializeField] private AudioEvent m_moveFoley;

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
        m_moveFoley.Play(gameObject);
    }

    private void Update()
    {
        m_currentDist = (m_charT.position - transform.position).magnitude;
        if (m_currentDist < m_dangerZoneDist && !m_isInDangerZone)
        {
            m_isInDangerZone = true;
            m_nearSFX.PlayOneShot(gameObject);
        }
        if (m_currentDist > m_dangerZoneDist + 1 && m_isInDangerZone)
        {
            m_isInDangerZone = false;
        }
        m_moveFoley.SetParameter("EntitySpeed", m_zombi.currentSpeed / 6f);
    }

    private void Bullet_OnHitObject(Bullet _bullet, GameObject _hit, Bullet.ContactType _type)
    {
        if (_type == Bullet.ContactType.Zombi)
        {
            if (_hit.TryGetComponent(out Zombi _zombi))
            {
                if (_zombi == m_zombi)
                {
                    m_hitSFX.PlayOneShot(gameObject);
                    //m_hitMarkerFB.PlayOneShot(gameObject);
                    m_impactFoley.PlayOneShot(gameObject);
                    
                }
            }
        }
    }

    private void OnEat(Zombi _zombi)
    {
        m_attackSFX.PlayOneShot(gameObject);
    }
}
