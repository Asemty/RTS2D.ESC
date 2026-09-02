using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfigSO", menuName = "Scriptable Objects/CameraConfigSO")]
public class CameraConfigSO : ScriptableObject
{
    public float moveSpeedByMouse;
    public float moveSpeedByKeyboard;
    public float borderSize;
    public float boundsOffset;
}
