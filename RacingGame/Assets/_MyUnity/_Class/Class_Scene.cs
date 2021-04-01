using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Working on PlayerPrebs and Scene
/// </summary>
public class Class_Scene
{
    public Class_Scene()
    {

    }

    /// <summary>
    /// Chance Scene by Scene's Name
    /// </summary>
    /// <param name="s_SceneNameToChance"></param>
    public Class_Scene(string s_SceneNameToChance)
    {
        Set_ChanceScene(s_SceneNameToChance);
    }

    /// <summary>
    /// Chance Scene by Scene's Name
    /// </summary>
    /// <param name="s_SceneName"></param>
    public void Set_ChanceScene(string s_SceneName)
    //Chance Scene by Scene's Name
    {
        Debug.LogWarning("Set_ChanceScene: " + s_SceneName);
        SceneManager.LoadScene(s_SceneName);
    }

    /// <summary>
    /// Get Scene build Index
    /// </summary>
    /// <returns></returns>
    public int Get_SceneBuildIndex()
    //Get Scene build Index
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// Save Value in System of Window or Android
    /// </summary>
    /// <param name="s_ValueName"></param>
    /// <param name="s_Value"></param>
    public void Set_PlayerPrefs(string s_ValueName, string s_Value)
    //Save Value in System of Window or Android
    {
        Debug.Log("Set_PlayerPrefs: " + "(STRING) " + "\"" + s_Value + "\"" + " >> " + "\"" + s_ValueName + "\"");
        PlayerPrefs.SetString(s_ValueName, s_Value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Save Value in System of Window or Android
    /// </summary>
    /// <param name="s_ValueName"></param>
    /// <param name="i_Value"></param>
    public void Set_PlayerPrefs(string s_ValueName, int i_Value)
    //Save Value in System of Window or Android
    {
        Debug.Log("Set_PlayerPrefs: " + "(INT) " + "\"" + i_Value + "\"" + " >> " + "\"" + s_ValueName + "\"");
        PlayerPrefs.SetInt(s_ValueName, i_Value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Save Value in System of Window or Android
    /// </summary>
    /// <param name="s_ValueName"></param>
    /// <param name="f_Value"></param>
    public void Set_PlayerPrefs(string s_ValueName, float f_Value)
    //Save Value in System of Window or Android
    {
        Debug.Log("Set_PlayerPrefs: " + "(FLOAT) " + "\"" + f_Value + "\"" + " >> " + "\"" + s_ValueName + "\"");
        PlayerPrefs.SetFloat(s_ValueName, f_Value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Get Value in System of Window or Android
    /// </summary>
    /// <param name="s_ValueName"></param>
    /// <returns></returns>
    public string Get_PlayerPrefs_String(string s_ValueName)
    //Get Value in System of Window or Android
    {
        try
        {
            Debug.Log("Get_PlayerPrefs_String: " + "\"" + s_ValueName + "\"" + " >> " + "\"" + PlayerPrefs.GetString(s_ValueName) + "\"");
            return PlayerPrefs.GetString(s_ValueName);
        }
        catch
        {
            Debug.LogError("Get_PlayerPrefs_String: Not Exist" + "\"" + s_ValueName + "\"");
            return null;
        }
    }

    /// <summary>
    /// Get Value in System of Window or Android
    /// </summary>
    /// <param name="s_ValueName"></param>
    /// <returns></returns>
    public int Get_PlayerPrefs_Int(string s_ValueName)
    //Get Value in System of Window or Android
    {
        try
        {
            Debug.Log("Get_PlayerPrefs_Int: " + "\"" + s_ValueName + "\"" + " >> " + "\"" + PlayerPrefs.GetInt(s_ValueName) + "\"");
            return PlayerPrefs.GetInt(s_ValueName);
        }
        catch
        {
            Debug.LogError("Get_PlayerPrefs_Int: Not Exist" + "\"" + s_ValueName + "\"");
            return 0;
        }
    }

    /// <summary>
    /// Get Value in System of Window or Android
    /// </summary>
    /// <param name="s_ValueName"></param>
    /// <returns></returns>
    public float Get_PlayerPrefs_Float(string s_ValueName)
    //Get Value in System of Window or Android
    {
        try
        {
            Debug.Log("Get_PlayerPrefs_Float: " + "\"" + s_ValueName + "\"" + " >> " + "\"" + PlayerPrefs.GetFloat(s_ValueName) + "\"");
            return PlayerPrefs.GetFloat(s_ValueName);
        }
        catch
        {
            Debug.LogError("Get_PlayerPrefs_Float: Not Exist" + "\"" + s_ValueName + "\"");
            return 0.0f;
        }
    }
}
