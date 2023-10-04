using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    #region Properties
    private T m_prefab;
    private Transform m_parent;
    private List<T> m_pool = new List<T>();
    #endregion

    #region Pool Methods
    internal Pool(T a_prefab, Transform a_parent)
    {
        m_prefab = a_prefab;
        m_parent = a_parent;
    }

    internal void SetPoolSize(int a_size, bool a_keepUnusedObj = true)
    {
        int missingObj = a_size - m_pool.Count;
        if (missingObj > 0)
        {
            for (int i = 0; i < missingObj; ++i)
            {
                m_pool.Add(MonoBehaviour.Instantiate<T>(m_prefab, m_parent));
            }
        }

        if(a_keepUnusedObj)
        {
            for (int i = a_size; i < m_pool.Count; ++i)
            {
                m_pool[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = m_pool.Count-1; i >= a_size; --i)
            {
                DestroyObject(m_pool[i].gameObject);
                m_pool.RemoveAt(i);
            }
        }

        m_prefab.gameObject.SetActive(false);
    }

    internal void Clear()
    {
        for (int i = 0; i < m_pool.Count; ++i)
        {
            DestroyObject(m_pool[i].gameObject);
        }
        m_pool.Clear();
    }

    private void DestroyObject(Object a_object)
    {
        if (Application.isPlaying)
        {
            MonoBehaviour.Destroy(a_object);
        }
        else
        {
            MonoBehaviour.DestroyImmediate(a_object);
        }
    }

    internal int Count { get { return m_pool.Count; } }

    internal T this[int i]
    {
        get { return m_pool[i]; }
        set { m_pool[i] = value; }
    }
    #endregion
}
