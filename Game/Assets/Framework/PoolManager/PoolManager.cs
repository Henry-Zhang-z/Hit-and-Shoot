using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������
public class ObjectPool
{
    public GameObject objectPrefab;
    public Queue<GameObject> pool;
    public int objectCount;

    public ObjectPool(GameObject _objectPrefab, Queue<GameObject> _pool, int _objectCount)
    {
        objectPrefab = _objectPrefab;
        pool = _pool;
        objectCount = _objectCount;
    }

}

/// <summary>
/// ����ع�����
/// </summary>
public class PoolManager : SingletonMono<PoolManager>
{
    public Dictionary<PoolName, ObjectPool> poolDic = new Dictionary<PoolName, ObjectPool>();
    /// <summary>
    /// �����µĶ����
    /// </summary>
    /// <param name="_objectPrefab">��ӵ�������е�Ԥ����</param>
    /// <param name="_objectCount">����</param>
    /// <param name="_poolName">����ص�����</param>
    public void CreateNewPool(GameObject _objectPrefab, int _objectCount, PoolName _poolName)
    {

        if (!poolDic.ContainsKey(_poolName))
        {
            //�����¶����
            poolDic.Add(_poolName, new ObjectPool(_objectPrefab, new Queue<GameObject>(), _objectCount));
        }
        //����������Ӷ���
        for (int i = 0; i < poolDic[_poolName].objectCount; i++)
        {
            var newObject = Instantiate(_objectPrefab);
            //poolDic[_poolName].pool.Enqueue(newObject);
            ReturnPool(_poolName, newObject);
        }
    }

    /// <summary>
    /// �ö��󷵻ض����
    /// </summary>
    /// <param name="_poolName">����ص�����</param>
    /// <param name="_newObject">���ض���صĶ���</param>
    public void ReturnPool(PoolName _poolName, GameObject _newObject)
    {
        poolDic[_poolName].pool.Enqueue(_newObject);
        _newObject.SetActive(false);
    }

    /// <summary>
    /// �Ӷ������ȡ������
    /// </summary>
    /// <param name="_poolName">����ص�����</param>
    /// <returns></returns>
    public GameObject GetFromPool(PoolName _poolName)
    {
        //��������ж��󲻹���ʱ���ٴ�����
        if (poolDic[_poolName].pool.Count == 0)
        {
            CreateNewPool(poolDic[_poolName].objectPrefab, poolDic[_poolName].objectCount, _poolName);
        }
        var newObject = poolDic[_poolName].pool.Dequeue();
        newObject.SetActive(true);
        return newObject;
    }
    /// <summary>
    /// ��ն����
    /// </summary>
    /// <param name="_poolName">���������</param>
    public void Clear(PoolName _poolName)
    {
        poolDic[_poolName].pool.Clear();
    }
    /// <summary>
    /// ������ж����
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
    }
}
