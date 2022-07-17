using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private AudioEvent m_selectSFX;
    [SerializeField] private AudioEvent m_submitSFX;

    public void OnSelect()
    {
        m_selectSFX.PlayOneShot(AudioManager.instance.gameObject);
    }

    public void OnSubmit()
    {
        m_submitSFX.PlayOneShot(AudioManager.instance.gameObject);
    }
}
