using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent
{
    public FMODUnity.EventReference m_eventReference;
    private FMOD.Studio.EventInstance m_eventInstance;
    bool m_isInit = false;

    public void Init(GameObject _emitter)
    {
        m_eventInstance = FMODUnity.RuntimeManager.CreateInstance(m_eventReference);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_eventInstance, _emitter.transform);
        m_isInit = true;
        Debug.Log($"{m_eventReference.Path} is initialized.");
    }

    public void Start(GameObject _emitter)
    {
        if (!m_isInit)
            Init(_emitter);
        m_eventInstance.start();
        Debug.Log($"{m_eventReference.Path} is playing.");
    }

    public void Stop()
    {
        m_eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        m_eventInstance.release();
        Debug.Log($"{m_eventReference.Path} was stopped and cleared.");
    }
}
