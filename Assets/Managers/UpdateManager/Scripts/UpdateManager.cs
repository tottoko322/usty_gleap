// UpdateManager.cs
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static UpdateManager Instance { get; private set; }

    // 更新対象のオブジェクトを保持するリスト
    private readonly List<IUpdatable> updatables = new List<IUpdatable>();

    void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        // 登録されている全てのオブジェクトの更新メソッドを呼び出す
        for (int i = 0; i < updatables.Count; i++)
        {
            updatables[i].ManagedUpdate(dt);
        }
    }

    // 更新リストにオブジェクトを登録するメソッド
    public void Register(IUpdatable updatable)
    {
        if (!updatables.Contains(updatable))
        {
            updatables.Add(updatable);
        }
    }

    // 更新リストからオブジェクトを解除するメソッド
    public void Unregister(IUpdatable updatable)
    {
        updatables.Remove(updatable);
    }
}