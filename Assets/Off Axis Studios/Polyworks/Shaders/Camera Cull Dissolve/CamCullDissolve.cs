using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCullDissolve : MonoBehaviour
{

    public Transform m_objectToTrack = null;

    private Material m_materialRef = null;
    private Renderer m_renderer = null;

    public Renderer Renderer
    {
        get
        {
            if(m_renderer == null)
            {
                m_renderer = GetComponent<Renderer>();
            }
            return m_renderer;
        }
    }

    public Material MaterialRef
    {
        get
        {
            if (m_materialRef == null)
            {
                m_materialRef = Renderer.material;
            }
            return m_materialRef;
        }
    }

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
        m_materialRef = m_renderer.material;
    }

    private void Update()
    {
        MaterialRef.SetVector("_Position", m_objectToTrack.position);
    }

    private void OnDestroy()
    {
        m_renderer = null;
        if(m_materialRef != null)
        {
            Destroy(m_materialRef);
        }

        m_materialRef = null;
    }
}
