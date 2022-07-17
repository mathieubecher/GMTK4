using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStateSetter : MonoBehaviour
{
    [SerializeField] private int m_musicState;

    void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicStates", m_musicState);
    }
}
