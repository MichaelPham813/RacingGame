using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfAndSheep_Game : MonoBehaviour
{
    //Public

    /// <summary>
    /// Room List (0: Wolf and 1-4: Sheep)
    /// </summary>
    [Header("Data")]
    public List<string> l_Game_ID;

    /// <summary>
    /// Room List (0: Wolf and 1-4: Sheep)
    /// </summary>
    public List<string> l_Game_Name;

    /// <summary>
    /// Room List (0: Wolf and 1-4: Sheep)
    /// </summary>
    public List<string> l_Game_Type;

    /// <summary>
    /// Room List (0: Wolf and 1-4: Sheep)
    /// </summary>
    public List<string> l_Game_Load;

    //Private

    /// <summary>
    /// Working on Firebase
    /// </summary>
    private Class_Firebase cl_Firebase;

    /// <summary>
    /// Working on Scene
    /// </summary>
    private Class_Scene cl_Scene;

    /// <summary>
    /// Get ROOM NAME
    /// </summary>
    private string s_Room = "";

    /// <summary>
    /// Room ID with Wolf ID
    /// </summary>
    private string s_ID = "";

    /// <summary>
    /// My Room Type
    /// </summary>
    private string s_Type = "";

    /// <summary>
    /// Count Sheep in Room
    /// </summary>
    private int i_Sheep = 0;

    /// <summary>
    /// Step Update
    /// </summary>
    private int i_Step = 0;

    //Method

    private void Start()
    {
        cl_Firebase = new Class_Firebase();

        cl_Scene = new Class_Scene();

        s_Room = cl_Scene.Get_PlayerPrefs_String("_RoomName");
        s_ID = cl_Scene.Get_PlayerPrefs_String("_GameID");
        s_Type = cl_Scene.Get_PlayerPrefs_String("_GameType");
        i_Sheep = cl_Scene.Get_PlayerPrefs_Int("_GameSheep");

        l_Game_ID = new List<string>();
        l_Game_Name = new List<string>();
        l_Game_Load = new List<string>();
    }

    private void Update()
    {
        Set_Step();
    }

    private void OnDestroy()
    {
        cl_Firebase.Set_FirebaseEnd();
    }

    //Help

    /// <summary>
    /// Get ID in LIST of ROOM
    /// </summary>
    /// <param name="s_IDFind"></param>
    /// <returns></returns>
    private int Get_Index_ID(string s_IDFind)
    {
        for (int i = 0; i < l_Game_ID.Count; i++)
        {
            if (l_Game_ID[i] == s_IDFind)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Get STRING to UPDATE on Firebase Database
    /// </summary>
    private string Get_SetToFirebaseDatabase(WolfAndSheep_Room_Data cs_RoomData)
    {
        return cs_RoomData._Name + "$" + cs_RoomData._Type + "$" + cs_RoomData._Ready;
    }

    /// <summary>
    /// GET STRING to READ from Firebase Database
    /// </summary>
    /// <param name="s_RoomData"></param>
    /// <returns></returns>
    private WolfAndSheep_Room_Data Get_GetFromFirebaseDatabase(string s_RoomData)
    {
        WolfAndSheep_Room_Data cs_RoomData = new WolfAndSheep_Room_Data();

        int i_Read = 0;

        for (int i = 0; i < s_RoomData.Length; i++)
        {
            if (s_RoomData[i] == '$')
            {
                i_Read++;
            }
            else
            {
                switch (i_Read)
                {
                    case 0:
                        cs_RoomData._Name += s_RoomData[i];
                        break;
                    case 1:
                        cs_RoomData._Type += s_RoomData[i];
                        break;
                    case 2:
                        cs_RoomData._Ready += s_RoomData[i];
                        break;
                }
            }
        }

        return cs_RoomData;
    }

    //Work

    /// <summary>
    /// Set ME in Load
    /// </summary>
    private void Set_Me()
    {
        WolfAndSheep_Room_Data cs_MyRoomData = new WolfAndSheep_Room_Data();

        cs_MyRoomData = new WolfAndSheep_Room_Data(
            cl_Firebase.Get_FirebaseAuth_DisplayName(),
            s_Type,
            "Loaded");

        cl_Firebase.Set_FirebaseDatabase_Value(
            "_Game" + s_Room + s_ID + "/" + cl_Firebase.Get_FirebaseAuth_ID(),
            Get_SetToFirebaseDatabase(cs_MyRoomData));
        //_Room$ROOM-NAME/$ID/ROOM-DATA
    }

    /// <summary>
    /// Working on Step
    /// </summary>
    private void Set_Step()
    {
        switch(i_Step)
        {
            case 0:
                //Step01: ME Set in
                {
                    Set_Me();

                    i_Step = 1;
                }
                break;
            case 1:
                //Step02: Load Player
                {
                    cl_Firebase.Set_FirebaseEvent_ChildAdded(
                        "_Game" + s_Room + s_ID,
                        Event_Load);

                    i_Step = 2;
                }
                break;
            case 2:
                //Step03: Load Wait
                {
                    if(Get_Load())
                    //If more than half of Player Loaded >> Start Game
                    {
                        i_Step = 3;
                    }
                }
                break;
            case 3:
                //Step04: Game Start
                {

                }
                break;
        }
    }

    /// <summary>
    /// Check Load
    /// </summary>
    /// <returns></returns>
    private bool Get_Load()
    {
        if(l_Game_ID.Count >= i_Sheep / 2)
        {
            int i_Load = 0;

            for(int i = 0; i < l_Game_ID.Count; i++)
            {
                if(l_Game_Load[i] == "Loaded")
                {
                    i_Load++;
                }
            }

            if (i_Load >= i_Sheep / 2)
            {
                return true;
            }
        }
        return false;
    }

    //Event

    /// <summary>
    /// Event Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Event_Load(object sender, ChildChangedEventArgs e)
    {
        if (e.Snapshot.Key == "_GameTurn")
            return;

        l_Game_ID.Add(e.Snapshot.Key);

        WolfAndSheep_Room_Data cs_RoomData = Get_GetFromFirebaseDatabase(e.Snapshot.Value.ToString());

        l_Game_Name.Add(cs_RoomData._Name);
        l_Game_Type.Add(cs_RoomData._Type);
        l_Game_Load.Add(cs_RoomData._Ready);

        //cl_Firebase.Set_FirebaseEvent_ValueChanged(
        //    "_Room" + s_Room + "/" + e.Snapshot.Key, Event_Player);
    }

    //===============================

    //Back

    /// <summary>
    /// Scene MAIN
    /// </summary>
    [Header("Scene")]
    public string s_SceneBack = "WolfAndSheep_Main";

    /// <summary>
    /// Button BACK in Canvas
    /// </summary>
    public void Button_Back_Canvas()
    {
        cl_Scene.Set_ChanceScene(s_SceneBack);
    }
}
