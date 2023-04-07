using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
   private static InputSystem input = new InputSystem();
    public static Vector2 Move => input.Gameplay.Move.ReadValue<Vector2>();
    public static Vector2 MousePos => input.Gameplay.MousePosition.ReadValue<Vector2>();
    public static bool ShootPress => input.Gameplay.Shoot.IsPressed();
    public static bool ShootRelease => input.Gameplay.Shoot.WasReleasedThisFrame();
    //��������ϵͳ
    public static void OnEnable()
    {
        input.Enable();
    }
}
