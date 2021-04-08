using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfAndSheep_Main : MonoBehaviour
{
    /// <summary>
    /// FIREBASE
    /// </summary>
    private Class_Firebase cl_Firebase;

    /// <summary>
    /// Working on Scene
    /// </summary>
    Class_Scene cl_Scene;

    /// <summary>
    /// Text DISPLAY NAME
    /// </summary>
    [Header("Header")]
    public Text t_DisplayName;

    private void Start()
    {
        cl_Firebase = new Class_Firebase();

        if (cl_Firebase.Get_FirebaseAuth_Login())
        {
            t_DisplayName.text = cl_Firebase.Get_FirebaseAuth_DisplayName();
        }

        cl_Scene = new Class_Scene(true);
    }

    private void Update()
    {
        if (b_Step_Room)
        {
            if (cl_Firebase.Get_FirebaseDatabase_Get_Done("RoomProgess"))
            {
                if (cl_Firebase.Get_Data().Get_Convert_Bool(cl_Firebase.Get_Data().Get_Data("RoomCheck")))
                //If Wolf Room Found
                {
                    cl_Scene.Set_PlayerPrefs("_RoomType", "Sheep");
                }
                else
                //If Wolf Room not Found
                {
                    cl_Scene.Set_PlayerPrefs("_RoomType", "Wolf");
                }
                cl_Scene.Set_PlayerPrefs("_RoomName", i_RoomName.text);
                cl_Scene.Set_ChanceScene(s_SceneRoom);
            }
        }
    }

    private void OnDestroy()
    {
        cl_Firebase.Set_FirebaseEnd();
    }

    //Room Find

    /// <summary>
    /// Room Panel
    /// </summary>
    [Header("Room")]
    public GameObject g_PanelRoom;

    /// <summary>
    /// Room Name Input
    /// </summary>
    public InputField i_RoomName;

    /// <summary>
    /// Scene Room
    /// </summary>
    [Header("Scene")]
    public string s_SceneRoom = "WolfAndSheep_Room";

    /// <summary>
    /// Button ROOM in Canvas
    /// </summary>
    public void Button_Room_Canvas()
    {
        g_PanelRoom.SetActive(true);
    }

    /// <summary>
    /// Check Room Working
    /// </summary>
    private bool b_Step_Room = false;

    /// <summary>
    /// Button ACCESS in Panel
    /// </summary>
    public void Button_Access_Panel()
    {
        if (!b_Step_Room)
        {
            b_Step_Room = true;

            StartCoroutine(cl_Firebase.Set_FirebaseDatabase_KeyExist_IEnumerator(
                "_Room" + i_RoomName.text, "RoomCheck", "RoomProgess"));
            //Start Check Wolf Room Exist
        }
    }

    /// <summary>
    /// Button BACK in Room Panel
    /// </summary>
    public void Button_Back_Panel()
    {
        g_PanelRoom.SetActive(false);
    }

    //Match Find

    public void Button_Match_Canvas()
    {

    }

    //Back

    /// <summary>
    /// Scene START
    /// </summary>
    public string s_SceneBack = "Android_FirebaseStart";

    /// <summary>
    /// Button BACK in Canvas
    /// </summary>
    public void Button_Back_Canvas()
    {
        Class_Scene cs_Scene = new Class_Scene(s_SceneBack);
    }
}
