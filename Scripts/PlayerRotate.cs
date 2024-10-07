using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : Game_Manager
{
    void Update()
    {
        fPlayerRot_Y = Input.GetAxis("Mouse Y");
        vecPlayerRotPos_Y = new Vector3(-fPlayerRot_Y, 0, 0);
        transform.Rotate(vecPlayerRotPos_Y * fMouse_Sens, Space.Self);
    }
}
