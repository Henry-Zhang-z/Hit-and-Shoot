using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����������
/// </summary>
public class SceneControl : SingletonMono<SceneControl>
{
    /// <summary>
    /// ����ת��
    /// </summary>
    /// <param name="_from">��ʼ����</param>
    /// <param name="_to">ת����ĳ���</param>
    public void Transition(string _from,string _to)
    {

    }

    IEnumerator TransitionScene(string _from,string _to)
    {
       yield return null;
    }
}
