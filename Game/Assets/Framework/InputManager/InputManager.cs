using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
   private static InputSystem input = new InputSystem();
    public static Vector2 inputTest => input.Gameplay.Move.ReadValue<Vector2>();

    //��������ϵͳ
    public static void OnEnable()
    {
        input.Enable();
    }
}
