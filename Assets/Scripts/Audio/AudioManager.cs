using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioEvent m_menuMusic;
    [SerializeField] private AudioEvent m_mainMusic;
    [SerializeField] private AudioEvent m_bulletWoodImpact;

    private void OnEnable()
    {
        Bullet.OnHitObject += Bullet_OnHitObject;
    }

    private void OnDisable()
    {
        Bullet.OnHitObject -= Bullet_OnHitObject;
    }

    private void Bullet_OnHitObject(Bullet _bullet, GameObject _hit, Bullet.ContactType _contactType)
    {
        if (_contactType == Bullet.ContactType.Wall)
        {
            GameObject emitter = new GameObject();
            emitter.transform.position = _bullet.transform.position;
            m_bulletWoodImpact.PlayOneShot(emitter);
            StartCoroutine(DestroyCooldown(emitter));
        }
    }

    private void Awake()
    {
        if (instance)
            Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            m_menuMusic.Stop();
            m_mainMusic.Play(gameObject);
        }
        else
        {
            m_mainMusic.Stop();
            m_menuMusic.Play(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            m_mainMusic.Play(gameObject);
        }
        else
        {
            m_menuMusic.Play(gameObject);
        }
    }

    private IEnumerator DestroyCooldown(GameObject _emitter)
    {
        yield return new WaitForSeconds(2f);
        Destroy(_emitter);
    }
}
