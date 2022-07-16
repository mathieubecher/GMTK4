using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Pathfinding;
public class Zombi : Hitable
{
    [SerializeField] private TextMeshPro m_tmpDiceScoreDisplay;
    
    [HideInInspector] public bool stop;
    
    [Header("AI")]
    [SerializeField] private float m_defaultSpeed = 1f;
    [SerializeField] private float m_nextWaypointDistance = 1f;
    [SerializeField] private float m_refreshPathFrequency = 1f;
    
    [Header("Hit")]
    [SerializeField] private float m_hitSpeed = 1f;
    [SerializeField] private float m_hitDuration = 1f;
    
    // private
    private int m_diceScore = 1;
    
    private List<Vector3> m_path;
    private Seeker m_seeker;
    private int m_currentWaypoint = 0;
    private bool m_reachedEndOfPath = false;
    private float m_lastTimeRefresh;
    
    private Character m_character;
    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_character = FindObjectOfType<Character>();
        m_seeker = FindObjectOfType<Seeker>();

        RefreshPath();
    }

    void OnPathComplete(Path _path, bool _repeat = false)
    {
        if (_path.error) return;
        m_path = new List<Vector3>(_path.vectorPath);
        m_currentWaypoint = 0;
        m_reachedEndOfPath = false;
        m_lastTimeRefresh = m_refreshPathFrequency;

    }
    private void RefreshPath()
    {
        m_lastTimeRefresh = 0.5f;
        m_seeker.StartPath(m_rigidbody.position, m_character.transform.position, OnPathComplete);
    }
    private void FixedUpdate()
    {
        if (stop)
        {
            return;
        }

        m_lastTimeRefresh -= Time.deltaTime;
        
        if(m_lastTimeRefresh <= 0f) RefreshPath();
        else if (m_path == null)
        {
            m_rigidbody.velocity = Vector2.zero;
            return;
        }
        else if (m_currentWaypoint >= m_path.Count)
        {
            Vector2 desiredDirection = (Vector2)m_character.transform.position - m_rigidbody.position;
            float distance = desiredDirection.magnitude;
            float desiredSpeed = (m_defaultSpeed * (float)m_diceScore);
            desiredDirection.Normalize();
            if (distance < desiredSpeed * Time.deltaTime)
            {
                desiredSpeed = distance / Time.deltaTime;
            }

            m_rigidbody.velocity = desiredDirection * (m_defaultSpeed * (float)m_diceScore);

            RefreshPath();
        }
        else
        {
            Vector2 desiredDirection = ((Vector2)m_path[m_currentWaypoint] - m_rigidbody.position);
            float distance = desiredDirection.magnitude;
            desiredDirection.Normalize();

            Vector2 desiredVelocity = desiredDirection * (m_defaultSpeed * (float)m_diceScore);
            m_rigidbody.velocity = desiredVelocity;

            if (distance < m_nextWaypointDistance) ++m_currentWaypoint;
        }

    }

    public override void Hit(Bullet _bullet)
    {
        if (stop) return;
        
        Vector3 direction = transform.position - _bullet.transform.position;
        direction.Normalize();
        IEnumerator coroutine = Stun(direction);
        StartCoroutine(coroutine);
    }

    private IEnumerator Stun(Vector3 _direction)
    {
        stop = true;
        m_rigidbody.velocity = _direction * m_hitSpeed;
        yield return new WaitForSeconds(m_hitDuration);
        m_diceScore = Random.Range(1,6);
        m_tmpDiceScoreDisplay.text = ""+m_diceScore;
        stop = false;
        RefreshPath();
    }
}
