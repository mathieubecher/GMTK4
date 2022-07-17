using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private CinemachineVirtualCamera m_camera;
    private Transform m_target;
    void Awake()
    {
        m_camera = GetComponent<CinemachineVirtualCamera>();
        m_target = m_camera.Follow;
    }
     
    void Update()
    {
    }
    
    public void FreezeTime(float _duration)
    {
        StartCoroutine(FreezeTimeCoroutine(_duration));
    }
    IEnumerator FreezeTimeCoroutine(float duration)
    {
        Time.timeScale = .0000001f;
        yield return new WaitForSeconds(duration * Time.timeScale);
        Time.timeScale = 1.0f;
    }
    public void Shake(float _duration, float _force)
    {
        StartCoroutine(ShakeCoroutine(_duration, _force));
    }
    IEnumerator ShakeCoroutine(float _duration, float _force)
    {
        var noise = m_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = _force;
        yield return new WaitForSeconds(_duration * Time.timeScale);
        noise.m_AmplitudeGain = 0f;
    }

    public void OnBulletTouch(Bullet _bullet, Hitable _other)
    {
        if (_other.TryGetComponent(out Character character))
        {
            
        }
        else if (_other.TryGetComponent(out Bullet bullet))
        {
            
        }
        else if (_other.TryGetComponent(out Hitable hit))
        {
        }
    }
}
