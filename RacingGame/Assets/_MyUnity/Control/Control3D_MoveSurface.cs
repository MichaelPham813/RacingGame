using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Rigid3D_Component))]

public class Control3D_MoveSurface : MonoBehaviour
//Move Control Surface (X & Z)
{
    private Rigid3D_Component cs_Rigid;
    //Use "Move" of this Script

    [Header("Keyboard")]
    public KeyCode k_MoveUp = KeyCode.UpArrow;
    //Control Move Up (Forward)

    public KeyCode k_MoveDown = KeyCode.DownArrow;
    //Control Move Down (Backward)

    public KeyCode k_MoveLeft = KeyCode.LeftArrow;
    //Control Move Left

    public KeyCode k_MoveRight = KeyCode.RightArrow;
    //Control Move Right

    public bool b_MutiButton = true;
    //Control Muti X & Z

    public KeyCode k_SpeedChance = KeyCode.LeftShift;
    //Control Speed Chance
    
    [Header("Move")]
    public float f_SpeedNormal = 2f;
    //Normal Speed Move

    public float f_SpeedChance = 5f;
    //Chance Speed Move
    private float f_SpeedCur;
    //Current Speed Move

    public bool b_StopRightAway = false;
    //Control Stop without Speed Stop Velocity

    public float f_SpeedStop = 3f;
    //Speed Stop

    private void Awake()
    {
        cs_Rigid = GetComponent<Rigid3D_Component>();
    }

    private void Update()
    {
        Set_SpeedChance();
        Set_MoveButton();
    }

    /// <summary>
    /// Control Move by Keyboard
    /// </summary>
    public void Set_MoveButton()
    {
        if (!b_MutiButton)
        //Not Allow Muti Control X & Z
        {
            if ((Input.GetKey(k_MoveLeft) || Input.GetKey(k_MoveRight)) && 
                (Input.GetKey(k_MoveUp) || Input.GetKey(k_MoveDown)))
            //Control X & Z >> Not Move
            {
                Set_Stop_X();
                Set_Stop_Z();
                return;
            }
        }

        if (Input.GetKey(k_MoveLeft) && Input.GetKey(k_MoveRight))
            //Press "Left" & "Right" >> Not Move X
            Set_Stop_X();
        else
        {
            if (Input.GetKey(k_MoveLeft))
                //Control Left
                Set_Move_X(-1);
            else
            if (Input.GetKey(k_MoveRight))
                //Control Right
                Set_Move_X(1);
            else
                //Not Control X
                Set_Stop_X();
        }

        if (Input.GetKey(k_MoveDown) && Input.GetKey(k_MoveUp))
            //Press "Up" & "Down" >> Not Move Z
            Set_Stop_Z();
        else
        {
            if (Input.GetKey(k_MoveDown))
                //Control Down
                Set_Move_Z(-1);
            else
            if (Input.GetKey(k_MoveUp))
                //Control Up
                Set_Move_Z(1);
            else
                //Not Control Z
                Set_Stop_Z();
        }
    }

    /// <summary>
    /// Control Move X
    /// </summary>
    /// <param name="i_ButtonMove_X_Right">If ">0" is Move Right or "<0" is Move Left</param>
    public void Set_Move_X(int i_ButtonMove_X_Right)
    {
        cs_Rigid.Set_MoveX_Velocity(i_ButtonMove_X_Right, f_SpeedCur, f_SpeedCur);
    }

    /// <summary>
    /// Control Move Z
    /// </summary>
    /// <param name="i_ButtonMove_Z_Forward">If ">0" is Move Forward or "<0" is Move Backward</param>
    public void Set_Move_Z(int i_ButtonMove_Z_Forward)
    {
        cs_Rigid.Set_MoveZ_Velocity(i_ButtonMove_Z_Forward, f_SpeedCur, f_SpeedCur);
    }

    /// <summary>
    /// Control Stop X
    /// </summary>
    public void Set_Stop_X()
    {
        if (b_StopRightAway)
            cs_Rigid.Set_StopX_Velocity();
        else
            cs_Rigid.Set_StopX_Velocity(f_SpeedStop);
    }

    /// <summary>
    /// Control Stop Z
    /// </summary>
    public void Set_Stop_Z()
    {
        if (b_StopRightAway)
            cs_Rigid.Set_StopZ_Velocity();
        else
            cs_Rigid.Set_StopZ_Velocity(f_SpeedStop);
    }

    /// <summary>
    /// Control Speed Chance
    /// </summary>
    public void Set_SpeedChance()
    {
        f_SpeedCur = (Input.GetKey(k_SpeedChance)) ? f_SpeedChance : f_SpeedNormal;
    }
}
