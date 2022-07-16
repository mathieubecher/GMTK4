using System.Collections;
using TMPro;
using UnityEngine;

public class Zombi : Hitable
{
    [SerializeField] private float m_defaultSpeed = 1f;
    [SerializeField] private float m_hitSpeed = 1f;
    [SerializeField] private float m_hitDuration = 1f;
    [SerializeField] private TextMeshPro m_tmpDiceScoreDisplay;
    
    private Rigidbody2D m_rigidbody;
    [HideInInspector] public bool stop;
    private int m_diceScore = 1;
    private Character m_character;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_character = FindObjectOfType<Character>();
    }

    private void Update()
    {
        if (stop)
        {
            return;
        }

        Vector3 desiredDirection = m_character.transform.position - transform.position;
        desiredDirection.Normalize();
        m_rigidbody.velocity = desiredDirection * (m_defaultSpeed * (float)m_diceScore);
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
    }
}
