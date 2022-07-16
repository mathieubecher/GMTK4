using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    [Header("Navigation")]
    [SerializeField] private float m_moveSpeed = 3f;
    [SerializeField] private Animator m_animator;

    [Header("Shoot")] 
    [SerializeField] private int m_magazineSize = 6;
    [SerializeField] private float m_shootCooldown = 0.1f;
    [SerializeField] private float m_reloadDuration = 0.5f;
    [Header("Shoot Spawn")] 
    [SerializeField] private Target m_target;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private float m_offsetBulletAtSpawn = 0.6f;
    [Header("Life")] 
    [SerializeField] private float m_invunerabilityDuration = 1.0f;
    [SerializeField] private int m_life = 3;

    // private
    private Controller m_controller;
    private Rigidbody2D m_rigidbody;
    
    private int m_leftBullet;
    private bool m_canShoot = true;
    private bool m_canEat = true;
    [HideInInspector] public bool stop;
    
    
    public delegate void ShootDelegate(int _bullet);
    public event ShootDelegate OnShoot;
    
    public delegate void ReloadDelegate();
    public event ReloadDelegate OnReload;
    
    public delegate void DamagedDelegate(int _life);
    public event DamagedDelegate OnDamaged;
    
    public delegate void DeadAction();
    public event DeadAction OnDead;

    //getter
    public int life { get => m_life; }
    public float maxSpeed { get => m_moveSpeed; }
    public float currentSpeed { get => m_rigidbody.velocity.magnitude; }
    public int leftBullet { get => m_leftBullet; }
    public Vector2 targetDir { get =>  m_controller.targetDirection; }
    
    void Awake()
    {
        m_controller = GetComponent<Controller>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_leftBullet = m_magazineSize;
    }
    
    void OnEnable()
    {
        m_controller = GetComponent<Controller>();
        m_controller.OnShoot += Shoot;
        m_controller.OnReload += Reload;
    }

    void OnDisable()
    {
        m_controller.OnShoot -= Shoot;
        m_controller.OnReload -= Reload;
    }
    
    void Update()
    {
        if (stop || m_life <= 0)
        {
            return;
        }
        m_target.direction = targetDir;
        m_rigidbody.velocity = m_controller.moveDirection * m_moveSpeed;

        m_animator.SetFloat("x", m_rigidbody.velocity.x-0.01f);
        m_animator.SetFloat("y", m_rigidbody.velocity.y -0.01f);
    }

    void Shoot()
    {
        if (stop || m_life <= 0) return;
        
        if (m_leftBullet <= 0 || !m_canShoot)
            return;
        
        --m_leftBullet;
        GameObject gameObjectRef = Instantiate(m_bullet);
        gameObjectRef.transform.position = transform.position + (Vector3)m_target.direction * m_offsetBulletAtSpawn;
        gameObjectRef.GetComponent<Bullet>().SetDirection(m_target.direction);

        OnShoot?.Invoke(m_leftBullet);
        
        if (m_leftBullet == 0)
        {
            Reload();
            return;
        }
        
        IEnumerator coroutine = Wait(m_shootCooldown);
        StartCoroutine(coroutine);
        
    }

    private void Reload()
    {
        if (stop || m_life <= 0) return;
        
        if (m_leftBullet == m_magazineSize || !m_canShoot)
            return; 
        
        OnReload?.Invoke();
        m_leftBullet = m_magazineSize;
        IEnumerator coroutine = Wait(m_reloadDuration);
        StartCoroutine(coroutine);
    }

    IEnumerator Wait(float _duration)
    {
        m_canShoot = false;
        yield return new WaitForSeconds(_duration);
        m_canShoot = true;
    }

    public void StopActor( Vector2 _velocity)
    {
        m_rigidbody.isKinematic = true;
        m_rigidbody.velocity = _velocity;
        stop = true;
    }
    public void RestartActor()
    {
        m_rigidbody.isKinematic = false;
        stop = false;
    }

    public bool Eat(Eat eat)
    {
        if (!m_canEat) return false;
        StartCoroutine(nameof(Invulnerability));
        return true;
    }

    private IEnumerator Invulnerability()
    {
        
        m_canEat = false;
        --m_life;
        if (m_life <= 0)
        {
            m_life = 0;
            Dead();
        }
        else
        {
            
            OnDamaged?.Invoke(m_life);
        }
        
        yield return new WaitForSeconds(m_invunerabilityDuration);
        
        if (m_life > 0) m_canEat = true;
    }

    private void Dead()
    {
        m_rigidbody.velocity = Vector2.zero;
        OnDead?.Invoke();
    }
}
