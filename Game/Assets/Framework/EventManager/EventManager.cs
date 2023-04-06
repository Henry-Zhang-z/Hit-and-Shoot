using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo { }
public class EventInfo : IEventInfo
{
    public UnityAction action;
}
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> action;
}
public class EventInfo<T, K> : IEventInfo
{
    public UnityAction<T, K> action;
}
public static class EventManager
{
    /// <summary>
    /// keyΪ�¼�������
    /// valueΪ�������¼���ί�к���
    /// </summary>
    private static Dictionary<EventName, IEventInfo> eventDict = new Dictionary<EventName, IEventInfo>();

    #region ����¼�����
    /// <summary>
    /// ����¼�����
    /// �޲���
    /// </summary>
    /// <param name="_eventName">�¼�������</param>
    /// <param name="_action">׼�����������¼���ί�к���</param>
    public static void Register(EventName _eventName, UnityAction action)
    {
        //�ж��ֵ����Ƿ��и��¼�
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo).action += action;
        else
            eventDict.Add(_eventName, new EventInfo() { action = action });
    }
    /// <summary>
    /// ����¼�����
    /// һ������
    /// </summary>
    /// <typeparam name="T">����1</typeparam>
    /// <param name="_eventName">�¼�������</param>
    /// <param name="action">׼�����������¼���ί�к���</param>
    public static void Register<T>(EventName _eventName, UnityAction<T> action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo<T>).action += action;
        else
            eventDict.Add(_eventName, new EventInfo<T>() { action = action });
    }
    /// <summary>
    /// ����¼�����
    /// ��������
    /// </summary>
    /// <typeparam name="T">����1</typeparam>
    /// <typeparam name="K">����2</typeparam>
    /// <param name="_eventName">�¼�����</param>
    /// <param name="action">׼�����������¼���ί�к���</param>
    public static void Register<T, K>(EventName _eventName, UnityAction<T, K> action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo<T, K>).action += action;
        else
            eventDict.Add(_eventName, new EventInfo<T, K>() { action = action });
    }
    #endregion

    #region ɾ���¼�����
    /// <summary>
    /// �Ƴ��¼�����
    /// �޲���
    /// </summary>
    /// <param name="_eventName">�¼�����</param>
    /// <param name="action">Ҫɾ���Ĵ����¼���ί�к���</param>
    public static void Remove(EventName _eventName, UnityAction action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo).action -= action;
    }
    /// <summary>
    /// �Ƴ��¼�����
    /// һ������
    /// </summary>
    /// <typeparam name="T">����1</typeparam>
    /// <param name="_eventName">�¼�����</param>
    /// <param name="action">Ҫɾ���Ĵ����¼���ί�к���</param>
    public static void Remove<T>(EventName _eventName, UnityAction<T> action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo<T>).action -= action;
    }
    /// <summary>
    /// �Ƴ��¼�����
    /// ��������
    /// </summary>
    /// <typeparam name="T">����1</typeparam>
    /// <typeparam name="K">����2</typeparam>
    /// <param name="_eventName">�¼�����</param>
    /// <param name="action">Ҫɾ���Ĵ����¼���ί�к���</param>
    public static void Remove<T, K>(EventName _eventName, UnityAction<T, K> action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo<T, K>).action -= action;

    }
    #endregion

    #region �¼�����
    /// <summary>
    /// �¼�����
    /// �޲���
    /// </summary>
    /// <param name="_eventName">�¼�������</param>
    public static void Send(EventName _eventName)
    {
        if (eventDict.ContainsKey(_eventName))
        {
            (eventDict[_eventName] as EventInfo).action.Invoke();
        }
    }
    /// <summary>
    /// �¼�����
    /// һ������
    /// </summary>
    /// <typeparam name="T">����1</typeparam>
    /// <param name="_eventName">�¼�������</param>
    /// <param name="t">����1</param>
    public static void Send<T>(EventName _eventName, T t)
    {
        if (eventDict.ContainsKey(_eventName))
        {
            (eventDict[_eventName] as EventInfo<T>).action.Invoke(t);
        }
    }
    /// <summary>
    /// �¼�����
    /// ��������
    /// </summary>
    /// <typeparam name="T">����1</typeparam>
    /// <typeparam name="K">����2</typeparam>
    /// <param name="_eventName">�¼�������</param>
    /// <param name="t">����1</param>
    /// <param name="k">����2</param>
    public static void Send<T, K>(EventName _eventName, T t, K k)
    {
        if (eventDict.ContainsKey(_eventName))
        {
            (eventDict[_eventName] as EventInfo<T, K>).action.Invoke(t, k);
        }
    }
    #endregion

    #region ����¼�
    /// <summary>
    /// ����¼�
    /// </summary>
    public static void Clear()
    {
        eventDict.Clear();
    }
    #endregion
}
