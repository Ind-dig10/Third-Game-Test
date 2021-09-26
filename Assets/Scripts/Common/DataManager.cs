using UnityEngine;

public static class DataManager
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string MOUSE_Y = "Mouse Y";
    public const string PLAYER_TAG = "Player";
    public const string CAMERA_TAG = "MainCamera";

    [SerializeField]
    private static float movementSpeed = 10f;

    [SerializeField]
    private static float acceleration = 2f;

    [SerializeField]
    private static float hightCamera = 2.3f;

    [SerializeField]
    private static float speedSmooth = 8f;

    public static float MovementSpeed => movementSpeed;

    public static float Acceleration => acceleration;

    public static float HightCamera => hightCamera;

    public static float SpeedSmooth => speedSmooth;
}
