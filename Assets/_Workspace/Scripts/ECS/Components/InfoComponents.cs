using Scellecs.Morpeh;
using UnityEngine;

public struct MouseInfoComponent : IComponent
{
    public Vector2 screenPosition;
    public bool isOverUI;
    public bool[] isDown;
    public bool[] isOldDown;
    public bool[] isClick;
    public enum Buttons
    {
        left = 0,
        right = 1, 
        middle = 2
    }
}
public struct KeyboardInfoComponent : IComponent
{
    public bool isArrowUp;
    public bool isArrowDown;
    public bool isArrowLeft;
    public bool isArrowRight;
}
public struct EnvironmentInfoComponent : IComponent
{
    public Vector2 screenSize; // if multiple cameras are used - a custome screen size is requared
}
public struct CellInfoComponent : IComponent
{
    public Vector2 cellSize;
}