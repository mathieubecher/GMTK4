using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterZombi : Hitable
{
    [SerializeField] private Animator m_bodyAnimator;
    [SerializeField] private Animator m_headAnimator;
    [SerializeField] private int m_diceScore = 1;

    [Header("Shoot")] [SerializeField] private float m_shootCooldown = 6f;
    [SerializeField] private List<Quaternion> m_shootAngleOffset;

    [Header("Shoot Spawn")] [SerializeField]
    private GameObject m_bullet;

    [SerializeField] private float m_offsetBulletAtSpawn = 0.6f;

    [Header("Hit")] [SerializeField] private float m_hitDuration = 1f;

    [HideInInspector] public bool stop;

    public Animator animator
    {
        get => m_bodyAnimator;
    }

    public Rigidbody2D rigidbody
    {
        get => m_rigidbody;
    }

    private DiceGestor m_diceGestor;
    private Character m_character;
    private Rigidbody2D m_rigidbody;

    private float m_currentCooldown = 0.0f;

    public Vector2 targetDir{get => (m_character.transform.position - transform.position).normalized; }

public delegate void ShootDelegate();
    public event ShootDelegate OnShoot;
    private void Awake()
    {
        m_diceGestor = GetComponentInChildren<DiceGestor>();
        m_character = FindObjectOfType<Character>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_currentCooldown = m_shootCooldown;
        m_diceGestor.Draw(m_diceScore);

    }
    private void OnEnable()
    {
        Door.OnDoorExit += DisableZombi;
        m_character.OnDamaged += CharacterDamage;
        m_character.OnDead += DisableZombi;
    }


    private void OnDisable()
    {
        Door.OnDoorExit -= DisableZombi;
        m_character.OnDamaged -= CharacterDamage;
        m_character.OnDead += DisableZombi;
    }
    
    private void DisableZombi()
    {
        m_rigidbody.velocity = Vector2.zero;
        UpdateAnim();
        enabled = false;
        m_headAnimator.SetBool("disable", true);
    }

    private void EnableZombi()
    {
        enabled = true;
        m_headAnimator.SetBool("disable", false);
    }
    private void CharacterDamage(int _life)
    {
        DisableZombi();
        StartCoroutine(nameof(Wait));
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        EnableZombi();
    }

    private void Update()
    {
        
        if (stop) return;
        UpdateAnim();
        m_currentCooldown -= Time.deltaTime;
        if (m_currentCooldown < 0.0f)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (stop) return;
        m_rigidbody.velocity = Vector2.zero;
    }
    private void UpdateAnim()
    {
        Vector2 direction = targetDir;
        m_bodyAnimator.SetFloat("x", direction.x);
        m_bodyAnimator.SetFloat("y", direction.y);
        
        m_headAnimator.SetFloat("x", direction.x);
        m_headAnimator.SetFloat("y", direction.x);
        m_headAnimator.SetBool("roll", stop);
    }


    void Shoot()
    {
        if (stop ) return;
        Vector2 direction = (m_character.transform.position - transform.position).normalized;
        for (int i = 0; i < m_diceScore && i < m_shootAngleOffset.Count; ++i)
        {
            Vector2 localDir = m_shootAngleOffset[i] * direction;
            GameObject gameObjectRef = Instantiate(m_bullet);
            gameObjectRef.transform.position = transform.position + (Vector3)direction * m_offsetBulletAtSpawn;
            Bullet bullet = gameObjectRef.GetComponent<Bullet>();
            bullet.speed = 1.0f;
            bullet.origin = this;
            bullet.SetDirection(localDir);
        }
        OnShoot?.Invoke();
        
        m_currentCooldown = m_shootCooldown;

    }
    public override void Hit(Bullet _bullet)
    {
        if (stop) return;

        Vector3 direction = _bullet.GetComponent<Rigidbody2D>().velocity;
        direction.Normalize();
        IEnumerator coroutine = Stun(direction);
        StartCoroutine(coroutine);
    }
    private IEnumerator Stun(Vector3 _direction)
    {
        stop = true;
        UpdateAnim();
        EnableZombi();
        yield return new WaitForSeconds(m_hitDuration);
        m_diceScore = m_diceGestor.Roll(((m_rigidbody.velocity.x < 0.0f && m_rigidbody.velocity.y < 0.0f) ||
                                         (m_rigidbody.velocity.x > 0.0f && m_rigidbody.velocity.y > 0.0f)));
        
        stop = false;
    }
}
