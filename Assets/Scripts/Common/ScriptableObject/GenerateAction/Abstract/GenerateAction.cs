using UnityEngine;

//[CreateAssetMenu(fileName = "GenerateAction", menuName = "Scriptable Objects/GenerateAction")]
public abstract class GenerateAction : ScriptableObject
{
    public abstract void Generate(GameObject user);
}
