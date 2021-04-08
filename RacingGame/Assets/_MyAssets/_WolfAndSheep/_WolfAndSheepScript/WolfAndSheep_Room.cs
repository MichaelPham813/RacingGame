using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfAndSheep_Room : MonoBehaviour
{
    //Public

    /// <summary>
    /// Room Title UI
    /// </summary>
    [Header("Header")]
    public Text t_Title;

    /// <summary>
    /// Room Name UI
    /// </summary>
    public Text t_Room;

    /// <summary>
    /// Player Name
    /// </summary>
    [Header("Name")]
    public Text t_Name_Wolf;

    /// <summary>
    /// Player Name
    /// </summary>
    public Text t_Name_Sheep01;

    /// <summary>
    /// Player Name
    /// </summary>
    public Text t_Name_Sheep02;

    /// <summary>
    /// Player Name
    /// </summary>
    public Text t_Name_Sheep03;

    /// <summary>
    /// Player Name
    /// </summary>
    public Text t_Name_Sheep04;

    /// <summary>
    /// Player Ready
    /// </summary>
    [Header("Ready")]
    public GameObject g_Ready_Sheep01;

    /// <summary>
    /// Player Ready
    /// </summary>
    public GameObject g_Ready_Sheep02;

    /// <summary>
    /// Player Ready
    /// </summary>
    public GameObject g_Ready_Sheep03;

    /// <summary>
    /// Player Ready
    /// </summary>
    public GameObject g_Ready_Sheep04;

    /// <summary>
    /// Button Ready
    /// </summary>
    [Header("Button")]
    public GameObject g_ButtonReady;

    /// <summary>
    /// Button Ready Text (Chance Text by READY check)
    /// </summary>
    public Text t_Ready_Button;

    /// <summary>
    /// Button Start
    /// </summary>
    public GameObject g_ButtonStart;

    /// <summary>
    /// Room List (0: Wolf and 1-4: Sheep)
    /// </summary>
    [Header("Data")]
    public List<string> l_Room_ID;

    /// <summary>
    /// Room List (0: Wolf and 1-4: Sheep)
    /// </summary>
    public List<string> l_Room_Name;

    /// <summary>
    /// Room List (0: Wolf and 1-4: Sheep)
    /// </summary>
    public List<string> l_Room_Type;

    /// <summary>
    /// Room List (0: Wolf and 1-4: Sheep)
    /// </summary>
    public List<string> l_Room_Ready;

    //Private

    /// <summary>
    /// Max PLAYER in ROOM
    /// </summary>
    private const int i_MAX_PLAYER = 5;

    /// <summary>
    /// Check ME is WOLF?
    /// </summary>
    private bool b_Wolf = false;

    /// <summary>
    /// Working on Firebase
    /// </summary>
    private Class_Firebase cl_Firebase;

    /// <summary>
    /// Working on Scene and Prepab
    /// </summary>
    private Class_Scene cl_Scene;

    /// <summary>
    /// Get ROOM NAME
    /// </summary>
    private string s_Room = "";

    /// <summary>
    /// Check if ME Ready
    /// </summary>
    private bool b_Ready_Me = false;

    /// <summary>
    /// Room ID with Wolf ID
    /// </summary>
    private string s_ID = "";

    /// <summary>
    /// Step Update
    /// </summary>
    private int i_Step = 0;

    /// <summary>
    /// Check if ME Start
    /// </summary>
    private bool b_Start_Me = false;

    /// <summary>
    /// Check Data on Firebase Database is Chance
    /// </summary>
    private bool b_Chance = false;

    //Method

    private void Start()
    {
        cl_Firebase = new Class_Firebase();

        cl_Scene = new Class_Scene();

        //Get if ME is WOLF
        b_Wolf = (cl_Scene.Get_PlayerPrefs_String("_RoomType") == "Wolf");

        //Get Room Name
        s_Room = cl_Scene.Get_PlayerPrefs_String("_RoomName");
        t_Room.text = s_Room;

        l_Room_ID = new List<string>();
        l_Room_Name = new List<string>();
        l_Room_Type = new List<string>();
    }

    private void Update()
    {
        Set_Step();
    }

    private void OnDestroy()
    {
        Set_Exit();
    }

    //Help

    /// <summary>
    /// Get ID in LIST of ROOM
    /// </summary>
    /// <param name="s_IDFind"></param>
    /// <returns></returns>
    private int Get_Index_ID(string s_IDFind)
    {
        for (int i = 0; i < l_Room_ID.Count; i++)
        {
            if (l_Room_ID[i] == s_IDFind)
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

        for(int i = 0; i < s_RoomData.Length; i++)
        {
            if(s_RoomData[i] == '$')
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
    /// Working on Step
    /// </summary>
    private void Set_Step()
    {
        switch (i_Step)
        {
            case 0:
                //Step01: Start Room
                {
                    WolfAndSheep_Room_Data cs_MyRoomData = new WolfAndSheep_Room_Data();

                    if (b_Wolf)
                    //If ME is Wolf
                    {
                        cs_MyRoomData = new WolfAndSheep_Room_Data(
                            cl_Firebase.Get_FirebaseAuth_DisplayName(),
                            "Wolf",
                            "Ready");

                        cl_Firebase.Set_FirebaseDatabase_Value(
                            "_Room" + s_Room + "/_RoomStart",
                            "RoomWait");
                        //_Room$ROOM-NAME/_RoomStart

                        cl_Firebase.Set_FirebaseDatabase_Value(
                            "_Room" + s_Room + "/_RoomKick",
                            "");
                        //_Room$ROOM-NAME/_RoomKick
                    }
                    else
                    //If ME is Sheep
                    {
                        cs_MyRoomData = new WolfAndSheep_Room_Data(
                            cl_Firebase.Get_FirebaseAuth_DisplayName(),
                            "Sheep",
                            "NotReady");
                    }

                    cl_Firebase.Set_FirebaseDatabase_Value(
                        "_Room" + s_Room + "/" + cl_Firebase.Get_FirebaseAuth_ID(),
                        Get_SetToFirebaseDatabase(cs_MyRoomData));
                    //_Room$ROOM-NAME/$ID/ROOM-DATA

                    i_Step = 1;
                }
                break;
            case 1:
                //Step02: Start Event
                {
                    cl_Firebase.Set_FirebaseEvent_ChildAdded(
                        "_Room" + s_Room,
                        Event_Join);
                    cl_Firebase.Set_FirebaseEvent_ChildRemoved(
                        "_Room" + s_Room,
                        Event_Out);
                    //_Room$ROOM-NAME/$ID

                    cl_Firebase.Set_FirebaseEvent_ValueChanged(
                        "_Room" + s_Room + "/_RoomStart",
                        Event_Start);
                    //_Room$ROOM-NAME/_RoomStart

                    cl_Firebase.Set_FirebaseEvent_ValueChanged(
                        "_Room" + s_Room + "/_RoomKick",
                        Event_Kick);
                    //_Room$ROOM-NAME/_RoomKick

                    i_Step = 2;
                }
                break;
            case 2:
                //Step03: Working on UI ROOM
                {
                    Set_RoomUI();

                    if (b_Start_Me)
                    //If ME is Wolf and ME Start this Game
                    {
                        Set_Wolf();
                        i_Step = 3;
                    }
                    if (s_ID != "")
                    //If ME is Sheep and get Start Game Message
                    {
                        i_Step = 4;
                    }
                }
                break;
            case 3:
                //Step04: Game Start (Set Message)
                {
                    cl_Firebase.Set_FirebaseDatabase_Value(
                        "_Room" + s_Room + "/_RoomStart",
                        cl_Firebase.Get_FirebaseAuth_ID());
                    //_Room$ROOM-NAME/_START-GAME

                    i_Step = 4;
                }
                break;
            case 4:
                //Step04: Game Start (Get Message)
                {
                    Set_Start();
                }
                break;
        }
    }

    /// <summary>
    /// Exit Working
    /// </summary>
    private void Set_Exit()
    {
        if(i_Step != 4)
        {
            if (b_Wolf)
            //If ME is Wolf
            {
                for (int i = 0; i < l_Room_ID.Count; i++)
                {
                    if (l_Room_ID[i] != cl_Firebase.Get_FirebaseAuth_ID())
                    {
                        WolfAndSheep_Room_Data cs_RoomData = new WolfAndSheep_Room_Data();

                        cs_RoomData = new WolfAndSheep_Room_Data(
                            l_Room_Name[i],
                            "Wolf",
                            "Ready");

                        cl_Firebase.Set_FirebaseDatabase_Value(
                            "_Room" + s_Room + "/" + l_Room_ID[i],
                            Get_SetToFirebaseDatabase(cs_RoomData));
                        //_Room$ROOM-NAME/$ID/ROOM-DATA
                        break;
                    }
                }
            }

            if (l_Room_ID.Count > 1)
            {
                cl_Firebase.Set_FirebaseDatabase_Delete(
                    "_Room" + s_Room + "/" + cl_Firebase.Get_FirebaseAuth_ID());
            }
            else
            if (l_Room_ID.Count == 1)
            {
                cl_Firebase.Set_FirebaseDatabase_Delete(
                    "_Room" + s_Room);
            }
        }
    }

    /// <summary>
    /// UI Step Working
    /// </summary>
    private void Set_RoomUI()
    {
        if (!b_Chance)
            return;

        //Check Sheep current Read
        int i_Sheep = 0;

        //Show Current PLAYER in ROOM
        for (int i = 0; i < l_Room_ID.Count; i++)
        {
            if (l_Room_Type[i] == "Sheep")
            {
                i_Sheep++;
                switch (i_Sheep)
                {
                    case 1:
                        t_Name_Sheep01.text = l_Room_Name[i];
                        g_Ready_Sheep01.SetActive(l_Room_Ready[i] == "Ready");
                        break;
                    case 2:
                        t_Name_Sheep02.text = l_Room_Name[i];
                        g_Ready_Sheep02.SetActive(l_Room_Ready[i] == "Ready");
                        break;
                    case 3:
                        t_Name_Sheep03.text = l_Room_Name[i];
                        g_Ready_Sheep03.SetActive(l_Room_Ready[i] == "Ready");
                        break;
                    case 4:
                        t_Name_Sheep04.text = l_Room_Name[i];
                        g_Ready_Sheep04.SetActive(l_Room_Ready[i] == "Ready");
                        break;
                }
            }
            else
            if (l_Room_Type[i] == "Wolf")
            {
                t_Name_Wolf.text = l_Room_Name[i];
            }
        }

        //Hide Emty
        for (int i = i_Sheep + 1; i < i_MAX_PLAYER; i++)
        {
            switch (i)
            {
                case 1:
                    t_Name_Sheep01.text = "...";
                    g_Ready_Sheep01.SetActive(false);
                    break;
                case 2:
                    t_Name_Sheep02.text = "...";
                    g_Ready_Sheep02.SetActive(false);
                    break;
                case 3:
                    t_Name_Sheep03.text = "...";
                    g_Ready_Sheep03.SetActive(false);
                    break;
                case 4:
                    t_Name_Sheep04.text = "...";
                    g_Ready_Sheep04.SetActive(false);
                    break;
            }
        }

        //Button Set
        if (b_Wolf)
        //If ME is Wolf
        {
            t_Title.text = "MY ROOM";

            //Check Start
            int i_Ready_Count = 0;
            for (int i = 0; i < l_Room_Ready.Count; i++)
            {
                if (l_Room_Ready[i] == "Ready")
                {
                    i_Ready_Count++;
                }
            }
            g_ButtonReady.SetActive(false);
            g_ButtonStart.SetActive(i_Ready_Count == (l_Room_Ready.Count) && l_Room_Ready.Count > 1);
        }
        else
        //If ME is Sheep
        {
            t_Title.text = "IN ROOM";

            g_ButtonReady.SetActive(true);
            g_ButtonStart.SetActive(false);
        }

        b_Chance = false;
    }

    /// <summary>
    /// Start Working
    /// </summary>
    private void Set_Start()
    {
        cl_Scene.Set_PlayerPrefs("_GameSheep", l_Room_ID.Count - 1);
        cl_Scene.Set_PlayerPrefs("_GameID", s_ID);

        int i_FindMe = Get_Index_ID(cl_Firebase.Get_FirebaseAuth_ID());

        cl_Scene.Set_PlayerPrefs("_GameType", l_Room_Type[i_FindMe]);
        cl_Scene.Set_ChanceScene(s_SceneGame);
    }

    /// <summary>
    /// Before Start Working
    /// </summary>
    private void Set_Wolf()
    {
        if (b_Wolf)
        //If ME is Wolf
        {
            int i_Sheep = 0;

            for (int i = 0; i < l_Room_ID.Count; i++)
            {
                if (l_Room_Type[i] == "Sheep")
                //If PLAYER is Sheep
                {
                    i_Sheep++;
                    l_Room_Type[i] = "Sheep" + i_Sheep.ToString();
                }

                WolfAndSheep_Room_Data cs_RoomData = new WolfAndSheep_Room_Data(
                    l_Room_Name[i],
                    l_Room_Type[i],
                    l_Room_Ready[i]);

                cl_Firebase.Set_FirebaseDatabase_Value(
                    "_Room" + s_Room + "/" + l_Room_ID[i],
                    Get_SetToFirebaseDatabase(cs_RoomData));
                //_Room$ROOM-NAME/$ID/ROOM-DATA
            }
        }
    }

    //Event

    /// <summary>
    /// Event KICK OUT ROOM
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Event_Kick(object sender, ValueChangedEventArgs e)
    {
        if(e.Snapshot.Key == cl_Firebase.Get_FirebaseAuth_ID())
        {
            cl_Firebase.Set_FirebaseDatabase_Value(
                "_Room" + s_Room + "/_RoomKick",
                "");
            //_Room$ROOM-NAME/_RoomKick

            cl_Scene.Set_ChanceScene(s_SceneBack);
        }
    }

    /// <summary>
    /// Event START GAME
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Event_Start(object sender, ValueChangedEventArgs e)
    {
        if (e.Snapshot.Value.ToString() != "RoomWait")
        //If START GAME is TRUE
        {
            s_ID = e.Snapshot.Value.ToString();
        }
    }

    /// <summary>
    /// Event OUT ROOM
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Event_Out(object sender, ChildChangedEventArgs e)
    {
        if (e.Snapshot.Key == "_RoomStart")
            return;

        if (e.Snapshot.Key == "_RoomKick")
            return;

        int i_IndexID = Get_Index_ID(e.Snapshot.Key);

        if (i_IndexID != -1)
        {
            l_Room_ID.RemoveAt(i_IndexID);
            l_Room_Name.RemoveAt(i_IndexID);
            l_Room_Type.RemoveAt(i_IndexID);
            l_Room_Ready.RemoveAt(i_IndexID);

            b_Chance = true;
        }
    }

    /// <summary>
    /// Event JOIN ROOM
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Event_Join(object sender, ChildChangedEventArgs e)
    {
        if (e.Snapshot.Key == "_RoomStart")
            return;

        if (e.Snapshot.Key == "_RoomKick")
            return;

        if (b_Wolf)
        //If ME is Wolf
        {
            if (l_Room_ID.Count < i_MAX_PLAYER)
            //If ROOM still EMTY
            {
                l_Room_ID.Add(e.Snapshot.Key);

                WolfAndSheep_Room_Data cs_RoomData = Get_GetFromFirebaseDatabase(e.Snapshot.Value.ToString());

                l_Room_Name.Add(cs_RoomData._Name);
                l_Room_Type.Add(cs_RoomData._Type);
                l_Room_Ready.Add(cs_RoomData._Ready);

                b_Chance = true;

                cl_Firebase.Set_FirebaseEvent_ValueChanged(
                    "_Room" + s_Room + "/" + e.Snapshot.Key, Event_Player);
            }
            else
            //If ROOM is FULL
            {
                cl_Firebase.Set_FirebaseDatabase_Value(
                    "_Room" + s_Room + "/_RoomKick",
                    e.Snapshot.Key);
                //_Room$ROOM-NAME/_RoomKick
            }
        }
        else
        //If ME is Sheep
        {
            l_Room_ID.Add(e.Snapshot.Key);

            WolfAndSheep_Room_Data cs_RoomData = Get_GetFromFirebaseDatabase(e.Snapshot.Value.ToString());

            l_Room_Name.Add(cs_RoomData._Name);
            l_Room_Type.Add(cs_RoomData._Type);
            l_Room_Ready.Add(cs_RoomData._Ready);

            b_Chance = true;

            cl_Firebase.Set_FirebaseEvent_ValueChanged(
                "_Room" + s_Room + "/" + e.Snapshot.Key, Event_Player);
        }
    }

    /// <summary>
    /// Event PLAYER CHANCe STATE
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Event_Player(object sender, ValueChangedEventArgs e)
    {
        int i_IndexID = Get_Index_ID(e.Snapshot.Key);

        if (i_IndexID != -1)
        {
            WolfAndSheep_Room_Data cs_RoomData = Get_GetFromFirebaseDatabase(e.Snapshot.Value.ToString());

            l_Room_Type[i_IndexID] = cs_RoomData._Type;
            l_Room_Ready[i_IndexID] = cs_RoomData._Ready;

            b_Chance = true;

            if (!b_Wolf)
            {
                if(l_Room_ID[i_IndexID] == cl_Firebase.Get_FirebaseAuth_ID())
                {
                    if (l_Room_Type[i_IndexID] == "Wolf")
                    {
                        b_Wolf = true;
                    }
                }
            }
        }
    }

    //Ready

    /// <summary>
    /// Button READY in Canvas
    /// </summary>
    public void Button_Ready_Canvas()
    {
        WolfAndSheep_Room_Data cs_MyRoomData = new WolfAndSheep_Room_Data();

        if (b_Ready_Me)
        //If READY >> NOT READY
        {
            t_Ready_Button.text = "READY";

            cs_MyRoomData = new WolfAndSheep_Room_Data(
                cl_Firebase.Get_FirebaseAuth_DisplayName(),
                "Sheep",
                "NotReady");
        }
        else
        if (!b_Ready_Me)
        //If NOT READY >> READY
        {
            t_Ready_Button.text = "WAIT";

            cs_MyRoomData = new WolfAndSheep_Room_Data(
                cl_Firebase.Get_FirebaseAuth_DisplayName(),
                "Sheep",
                "Ready");
        }

        b_Ready_Me = !b_Ready_Me;

        cl_Firebase.Set_FirebaseDatabase_Value(
            "_Room" + s_Room + "/" + cl_Firebase.Get_FirebaseAuth_ID(),
            Get_SetToFirebaseDatabase(cs_MyRoomData));
        //_Room$ROOM-NAME/$ID/ROOM-DATA
    }

    //Start

    [Header("Scene")]
    /// <summary>
    /// Scene LOAD
    /// </summary>
    public string s_SceneGame = "WolfAndSheep_Game";

    /// <summary>
    /// Button START in Canvas
    /// </summary>
    public void Button_Start_Canvas()
    {
        if (b_Wolf)
        //If ME is Wolf
        {
            b_Start_Me = true;
        }
    }

    //Back

    /// <summary>
    /// Scene MAIN
    /// </summary>
    public string s_SceneBack = "WolfAndSheep_Main";

    /// <summary>
    /// Button BACK in Canvas
    /// </summary>
    public void Button_Back_Canvas()
    {
        cl_Scene.Set_ChanceScene(s_SceneBack);
    }

}
