using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float GetDirection()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool IsJumpPressed()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    public bool IsDashPressed()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

}
