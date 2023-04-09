using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����������
/// </summary>
public class SceneControl : SingletonMono<SceneControl>
{
    [HideInInspector]
    public int level;
    //protected override void Awake()
    //{
    //    base.Awake();
    //    SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    //}
    /// <summary>
    /// ����ת��
    /// </summary>
    /// <param name="_from">��ʼ����</param>
    /// <param name="_to">ת����ĳ���</param>
    public void Transition(string _from,string _to)
    {
        StartCoroutine(TransitionScene(_from, _to));
    }

    IEnumerator TransitionScene(string _from,string _to)
    {
        //ж��from����
        EventManager.Send(EventName.EnterScene);
        yield return SceneManager.UnloadSceneAsync(_from);
        //����to����
        yield return SceneManager.LoadSceneAsync(_to,LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        EventManager.Send(EventName.ExitScene);

        if (_to == "Gameplay") EventManager.Send<int>(EventName.LoadLevel,level);
    }
}
