using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ɱ������ί��
public delegate void EventData(params object[] args);

/// <summary>
/// �¼�������
/// </summary>
public static class EventManager
{
    //�洢�¼����ֵ�
    private static Dictionary<EventName, EventData> eventDict = new Dictionary<EventName, EventData>();
    /// <summary>
    /// ע���¼�
    /// </summary>
    /// <param name="_eventName">�¼���</param>
    /// <param name="_action">��������</param>
    public static void Register(EventName _eventName, EventData _action)
    {
        if (eventDict.ContainsKey(_eventName)) eventDict[_eventName] += _action;
        else eventDict.Add(_eventName, _action);
    }
    /// <summary>
    /// ע���¼�
    /// </summary>
    /// <param name="_eventName">�¼�����</param>
    /// <param name="_action">��������</param>
    public static void Remove(EventName _eventName, EventData _action)
    {
        if (eventDict.ContainsKey(_eventName)) eventDict[_eventName] -= _action;
    }
    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="_eventName">�¼�����</param>
    /// <param name="_args">�¼�����</param>
    public static void Send(EventName _eventName, params object[] _args)
    {
        if (eventDict.ContainsKey(_eventName)) eventDict[_eventName]?.Invoke(_args);
    }
}
