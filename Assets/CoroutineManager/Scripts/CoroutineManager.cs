// CoroutineManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static CoroutineManager Instance { get; private set; }

    [Tooltip("Coroutine を呼び出す間隔（秒）")]
    public float interval = 1f;

    // 登録されたコルーチン対象
    private readonly List<ICoroutineUpdatable> coroutines = new List<ICoroutineUpdatable>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // 更新ループを開始
        StartCoroutine(CoroutineLoop());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator CoroutineLoop()
    {
        while (true)
        {
            for (int i = 0; i < coroutines.Count; i++)
            {
                coroutines[i].ManagedCoroutine(interval);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    // コルーチン対象を登録
    public void Register(ICoroutineUpdatable coroutineUpdatable)
    {
        if (!coroutines.Contains(coroutineUpdatable))
        {
            coroutines.Add(coroutineUpdatable);
        }
    }

    // コルーチン対象を解除
    public void Unregister(ICoroutineUpdatable coroutineUpdatable)
    {
        coroutines.Remove(coroutineUpdatable);
    }
}
