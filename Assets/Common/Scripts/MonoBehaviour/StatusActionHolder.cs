using UnityEngine;

public class StatusActionHolder : MonoBehaviour
{
    [SerializeField] private SelfStatusAction[] selfActions;
    [SerializeField] private TargetStatusAction[] targetActions;
    [SerializeField] private GenerateAction[] generateActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // indexで取得
    public SelfStatusAction GetSelfStatusActionFromIndex(int index)
    {
        return selfActions[index];
    }

    public TargetStatusAction GetTargetStatusActionFromIndex(int index)
    {
        return targetActions[index];
    }

    public GenerateAction GetGenerateActionFromIndex(int index)
    {
        return generateActions[index];
    }
    // 名前で取得
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

    public GenerateAction GetGenerateActionFromName(string actionName)
    {
        foreach (var action in generateActions)
        {
            if (action.name == actionName)
                return action;
        }
        Debug.LogWarning($"generateAction \"{actionName}\" が見つかりませんでした");
        return null;
    }
}
