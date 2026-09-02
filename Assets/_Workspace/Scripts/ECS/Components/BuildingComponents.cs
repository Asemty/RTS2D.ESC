using Scellecs.Morpeh;
using UnityEngine;

public struct BuildRequestComponent: IComponent
{
    public Vector2Int buildingPos;
    public BuildData buildData;
}
public struct BuildDataArchiveComponent : IComponent
{
    public BuildData[] buildsArchive;
}
public struct BuildPrefabComponent : IComponent
{
    public SpriteRenderer prefab;
}