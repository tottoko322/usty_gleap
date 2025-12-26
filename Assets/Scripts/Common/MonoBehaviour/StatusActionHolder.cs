using UnityEngine;

public class StatusActionHolder : MonoBehaviour
{
    [SerializeField] private SelfStatusAction[] selfActions;
    [SerializeField] private TargetStatusAction[] targetActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SelfStatusAction GetSelfStatusActionFromIndex(int index)
    {
        return selfActions[index];
    }

    public TargetStatusAction GetTargetStatusActionFromIndex(int index)
    {
        return targetActions[index];
    }
    // 名前で SelfStatusAction を取得
    public SelfStatusAction GetSelfStatusActionFromName(string actionName)
    {
        foreach (var action in selfActions)
        {
            if (action.name == actionName)
                return action;
        }
        Debug.LogWarning($"SelfStatusAction \"{actionName}\" が見つかりませんでした");
        return null;
    }

    // 名前で TargetStatusAction を取得
    public TargetStatusAction GetTargetStatusActionFromName(string actionName)
    {
        foreach (var action in targetActions)
        {
            if (action.name == actionName)
                return action;
        }
        Debug.LogWarning($"TargetStatusAction \"{actionName}\" が見つかりませんでした");
        return null;
    }
}
