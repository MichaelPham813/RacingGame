using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on GameObject and Prepab
/// </summary>
public class Class_Object
{
    public Class_Object()
    {

    }

    /// <summary>
    /// Create Prepab inside Parent GameObject
    /// </summary>
    /// <param name="g_Prepab"></param>
    /// <param name="t_Parent"></param>
    /// <returns></returns>
    public GameObject Set_Prepab_Create(GameObject g_Prepab, Transform t_Parent)
    //Create Prepab inside Parent GameObject
    {
        return MonoBehaviour.Instantiate(g_Prepab, t_Parent) as GameObject;
    }

    /// <summary>
    /// Create Prepab inside Scene
    /// </summary>
    /// <param name="g_Prepab"></param>
    /// <param name="v_Pos"></param>
    /// <param name="q_Rotation"></param>
    /// <returns></returns>
    public GameObject Set_Prepab_Create(GameObject g_Prepab, Vector3 v_Pos, Quaternion q_Rotation)
    //Create Prepab inside Scene
    {
        return MonoBehaviour.Instantiate(g_Prepab, v_Pos, q_Rotation) as GameObject;
    }
}