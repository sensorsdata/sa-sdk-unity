using SensorsAnalytics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SensorsDataAPI))]
public class SA_Inspector : Editor
{

    bool isEnalbeLog = true;
    string serverUrl = "";
    GUIStyle titleStyle;
    GUIStyle labelStyle;

    private void OnEnable()
    {
        isEnalbeLog = this.serializedObject.FindProperty("isEnableLog").boolValue;
        serverUrl = this.serializedObject.FindProperty("serverUrl").stringValue;
        //finalResult = this.serializedObject.FindProperty("autoTrackType").intValue;
        finalNetworkType = this.serializedObject.FindProperty("networkType").intValue;
        titleStyle = new GUIStyle();
        titleStyle.fontSize = 15;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.normal.textColor = Color.white;

        labelStyle = new GUIStyle();
        labelStyle.fontStyle = FontStyle.Bold;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        this.serializedObject.Update();
        EditorGUILayout.LabelField("SensorsData Unity SDK Config", titleStyle);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Version", SensorsDataAPI.SDK_VERSION);
        serverUrl = EditorGUILayout.TextField("Server Url", serverUrl);

        SensorsDataAPI sensorsDataAPI = (SensorsDataAPI)target;
        isEnalbeLog = EditorGUILayout.Toggle("Enable Log", isEnalbeLog);
        //autoTrackTypes();
        networkTypes();
        this.serializedObject.FindProperty("isEnableLog").boolValue = isEnalbeLog;
        this.serializedObject.FindProperty("serverUrl").stringValue = serverUrl;
        this.serializedObject.ApplyModifiedProperties();
    }

    private void OnValidate()
    {
    }

    /*
     //全埋点相关的配置，暂时不需要
     int finalResult = 0;
     private void autoTrackTypes()
     {
         int tmpResult = 0;
         EditorGUILayout.LabelField("AutoTrackTypes", labelStyle);
         if (EditorGUILayout.Toggle("AppStart", (finalResult & 1) != 0))
         {
             tmpResult = 1;
         }
         if (EditorGUILayout.Toggle("AppEnd", (finalResult & 1 << 1) != 0))
         {
             tmpResult |= 1 << 1;
         }
         finalResult = tmpResult;
         this.serializedObject.FindProperty("autoTrackType").intValue = finalResult;
     }*/

    //网络类型配置
    int finalNetworkType = 0;
    private void networkTypes()
    {
        int tmpResult = 0;
        EditorGUILayout.LabelField("NetworkTypes", labelStyle);
        if (EditorGUILayout.Toggle("2G", (finalNetworkType & 1) != 0))
        {
            tmpResult = 1;
        }
        if (EditorGUILayout.Toggle("3G", (finalNetworkType & 1 << 1) != 0))
        {
            tmpResult |= 1 << 1;
        }
        if (EditorGUILayout.Toggle("4G", (finalNetworkType & 1 << 2) != 0))
        {
            tmpResult |= 1 << 2;
        }
        if (EditorGUILayout.Toggle("5G", (finalNetworkType & 1 << 4) != 0))
        {
            tmpResult |= 1 << 4;
        }
        if (EditorGUILayout.Toggle("Wifi", (finalNetworkType & 1 << 3) != 0))
        {
            tmpResult |= 1 << 3;
        }
       
        finalNetworkType = tmpResult;
        this.serializedObject.FindProperty("networkType").intValue = finalNetworkType;
    }
}