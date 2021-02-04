using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]internal PlayerController player;

    public enum dodge
    {
        forward,
        left,
        back,
        right,
        non
    };

    internal bool PickInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    internal dodge dodging()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            return dodge.forward;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            return dodge.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            return dodge.back;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            return dodge.right;
        }
        return dodge.non;
    }
    internal bool runInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            return true;
        }
        return false;
    }
    internal bool mouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    internal bool attackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal bool ShowOrHideInputs(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
