using UnityEngine;

[CreateAssetMenu(fileName = "BuildDataSO", menuName = "Scriptable Objects/BuildDataSO")]
public class BuildDataSO : ScriptableObject
{
    public BuildData[] builds;
}
[System.Serializable]
public class BuildData
{
    public string name;
    public Sprite sprite;
    public Vector2Int size;
}
