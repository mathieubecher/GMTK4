using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Pathfinding;
using Random = UnityEngine.Random;

public class Zombi : Hitable
{
    [SerializeField] private TextMeshPro m_tmpDiceScoreDisplay;
    [SerializeField] private Animator m_bodyAnimator;
    [SerializeField] private Animator m_headAnimator;

    [HideInInspector] public bool stop;

    [Header("AI")] [SerializeField] private float m_defaultSpeed = 1f;
    [SerializeField] private float m_nextWaypointDistance = 1f;
    [SerializeField] private float m_refreshPathFrequency = 1f;

    [Header("Hit")] [SerializeField] private float m_hitSpeed = 1f;
    [SerializeField] private float m_hitDuration = 1f;

    // private
    private int m_diceScore = 1;

    private List<Vector3> m_path;
    private Seeker m_seeker;
    private int m_currentWaypoint = 0;
    private float m_lastTimeRefresh;

    private Character m_character;
    private Rigidbody2D m_rigidbody;

    //getter
    public float maxSpeed { get => m_diceScore * m_defaultSpeed; }
    public float currentSpeed { get => m_rigidbody.velocity.magnitude; }

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_character = FindObjectOfType<Character>();
        m_seeker = FindObjectOfType<Seeker>();

        RefreshPath();
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
        StartCoroutine("Wait");
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        EnableZombi();
    }

    void OnPathComplete(Path _path, bool _repeat = false)
    {
        if (_path.error) return;
        m_path = new List<Vector3>(_path.vectorPath);
        m_currentWaypoint = 0;
        m_lastTimeRefresh = m_refreshPathFrequency;

    }

    private void RefreshPath()
    {
        m_lastTimeRefresh = 0.5f;
        m_seeker.StartPath(m_rigidbody.position, m_character.transform.position, OnPathComplete);
    }

    private void Update()
    {
        UpdateAnim();
    }

    private void UpdateAnim()
    {
        m_bodyAnimator.SetFloat("x", (stop ? -1f : 1f) * m_rigidbody.velocity.x);
        m_bodyAnimator.SetFloat("y", (stop ? -1f : 1f) * m_rigidbody.velocity.y);

        m_headAnimator.SetFloat("x", m_rigidbody.velocity.x);
        m_headAnimator.SetFloat("y", m_rigidbody.velocity.y);
        m_headAnimator.SetBool("roll", stop);
    }

    private void FixedUpdate()
    {
        if (stop) return;

        m_lastTimeRefresh -= Time.deltaTime;

        if (m_lastTimeRefresh <= 0f) RefreshPath();
        else if (m_path == null)
        {
            m_rigidbody.velocity = Vector2.zero;
            return;
        }
        else if (m_currentWaypoint >= m_path.Count)
        {
            Vector2 desiredDirection = (Vector2)m_character.transform.position - m_rigidbody.position;
            float distance = desiredDirection.magnitude;
            float desiredSpeed = maxSpeed;
            desiredDirection.Normalize();
            if (distance < desiredSpeed * Time.deltaTime)
            {
                desiredSpeed = distance / Time.deltaTime;
            }

            m_rigidbody.velocity = desiredDirection * maxSpeed;

            RefreshPath();
        }
        else
        {
            Vector2 desiredDirection = ((Vector2)m_path[m_currentWaypoint] - m_rigidbody.position);
            float distance = desiredDirection.magnitude;
            desiredDirection.Normalize();

            Vector2 desiredVelocity = desiredDirection * maxSpeed;
            m_rigidbody.velocity = desiredVelocity;

            if (distance < m_nextWaypointDistance) ++m_currentWaypoint;
        }

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
        EnableZombi();
        gameObject.layer = LayerMask.NameToLayer("PhysicZombi");
        m_rigidbody.velocity = _direction * m_hitSpeed;
        yield return new WaitForSeconds(m_hitDuration);
        m_diceScore = Random.Range(1, 7);
        m_tmpDiceScoreDisplay.text = "" + m_diceScore;
        stop = false;
        gameObject.layer = LayerMask.NameToLayer("Zombi");
        RefreshPath();
    }
}
