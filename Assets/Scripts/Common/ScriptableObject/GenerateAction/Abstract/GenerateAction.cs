using UnityEngine;

//[CreateAssetMenu(fileName = "GenerateAction", menuName = "Scriptable Objects/GenerateAction")]
public abstract class GenerateAction : ScriptableObject
{
    public abstract void Execute(GameObject user);
}
