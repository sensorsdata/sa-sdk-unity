using SensorsAnalytics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SensorsDataAPI))]

public class SA_Inspector : Editor
{

    bool isEnableLog = false;
    string serverUrl = "";
    GUIStyle titleStyle;
    GUIStyle labelStyle;


    //网络类型配置
    int finalNetworkType = 0;
    //全埋点相关的配置
    int finalAutoTrackType = 0;

    private void OnEnable()
    {
        isEnableLog = this.serializedObject.FindProperty("isEnableLog").boolValue;
        serverUrl = this.serializedObject.FindProperty("serverUrl").stringValue;
        finalAutoTrackType = this.serializedObject.FindProperty("autoTrackType").intValue;
        finalNetworkType = this.serializedObject.FindProperty("networkType").intValue;

        // 标题样式
        titleStyle = new GUIStyle();
        titleStyle.fontSize = 16;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.normal.textColor = Color.white;

        // 开关配置样式
        labelStyle = new GUIStyle();
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.normal.textColor = Color.white;
    }

    // 自定义绘制 Inspector 界面
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        this.serializedObject.Update();
        EditorGUILayout.LabelField("SensorsData Unity SDK Config", titleStyle);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Version", SensorsDataAPI.SDK_VERSION);

        // 初始化配置
        if (string.IsNullOrEmpty(serverUrl))
        {
            EditorGUILayout.HelpBox("请输入数据接收地址...", MessageType.Warning);
        }
        serverUrl = EditorGUILayout.TextField("Server Url", serverUrl);

        SensorsDataAPI sensorsDataAPI = (SensorsDataAPI)target;
        isEnableLog = EditorGUILayout.Toggle("Enable Log", isEnableLog);

        AutoTrackTypes();
        NetworkTypes();
        this.serializedObject.FindProperty("isEnableLog").boolValue = isEnableLog;
        this.serializedObject.FindProperty("serverUrl").stringValue = serverUrl;
        this.serializedObject.ApplyModifiedProperties();
    }

    private void OnValidate()
    {
    }


    private void AutoTrackTypes()
    {
        int tmpResult = 0;
        EditorGUILayout.LabelField("AutoTrackTypes", labelStyle);
        if (EditorGUILayout.Toggle("AppStart", (finalAutoTrackType & 1) != 0))
        {
            tmpResult = 1;
        }
        if (EditorGUILayout.Toggle("AppEnd", (finalAutoTrackType & 1 << 1) != 0))
        {
            tmpResult |= 1 << 1;
        }
        finalAutoTrackType = tmpResult;
        this.serializedObject.FindProperty("autoTrackType").intValue = finalAutoTrackType;
    }

    private void NetworkTypes()
    {
        int tmpResult = 0;
        EditorGUILayout.LabelField("NetworkTypes（只支持 Android & iOS）", labelStyle);
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

        // 初始化配置，设置网络策略，只针对 Android & iOS 生效
        this.serializedObject.FindProperty("networkType").intValue = finalNetworkType;
    }


}