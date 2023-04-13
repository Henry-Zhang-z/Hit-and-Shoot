using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����������
/// </summary>
public class SceneControl : SingletonMono<SceneControl>
{
    [HideInInspector]
    public int level;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;
    protected override void Awake()
    {
        base.Awake();
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
       
    }
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
       // yield return Fade(1f);
        EventManager.Send(EventName.EnterScene);
        yield return SceneManager.UnloadSceneAsync(_from);
        //����to����
        yield return SceneManager.LoadSceneAsync(_to,LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        if (_to == "Gameplay") EventManager.Send<int>(EventName.LoadLevel, level);
        EventManager.Send(EventName.ExitScene);
       // if (_from != "Gameplay") yield return Fade(0f);
    }

    IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        //��ֹ������ײ
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(targetAlpha - fadeCanvasGroup.alpha) / fadeDuration;
        //��͸����ֵ��Ŀ��ֵ��ͬ(����)ʱ,�������䣬�ָ�������ײ
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            //�������
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;

    }
}
