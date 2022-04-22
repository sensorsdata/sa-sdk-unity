using System;
using System.Collections.Generic;
using SensorDataAnalytics.Utils;
using SensorsAnalytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SensorsAnalytics.Wrapper;

public class Sample : MonoBehaviour,IDynamicSuperProperties
{
    private const string Name = "profileSet";
    public GUIStyle guiStyle;
    public GUISkin guiSkin;
    private Vector2 scrollVector2;
    private bool finalResult = false;

    // Start is called before the first frame update
    void Awake()
    {
        print("sample awake====");
    }

    private void handSchemeUrl(string url)
    {

    }

    private void OnGUI()
    {
        //print("ongui====");
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        GUI.skin = guiSkin;
        guiStyle.fontSize = 30;
        GUILayout.BeginArea(new Rect(screenWidth * 0.1f, screenHeight * 0.15f, screenWidth * 0.8f, screenHeight * 0.7f));
        scrollVector2 = GUILayout.BeginScrollView(scrollVector2);

        GUILayout.Label("SensorsData Unity SDK");


        if (GUILayout.Button("Track 事件"))
        {
            DateTime dateTime = DateTime.Now;
            
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("country", "中国");
            dictionary.Add("address", dateTime);
            //dictionary.Add("test_array", new int[] { 1, 2, 3, 4, 5 });

            print("Track an Event.");
            SensorsDataAPI.Track("Jjcheng112", dictionary);
        }

       
        GUILayout.Space(20);

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Second")
        {
            if (GUILayout.Button("Load Sample Scence"))
            {
                SceneManager.LoadScene("Sample");
            }

            GUILayout.Space(20);
            if (GUILayout.Button("set android max cache size"))
            {
                print("set android max cache size.");
                SensorsDataAPI.SetAndroidMaxCacheSize(20 * 1024 * 1024);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("set ios max cache size"))
            {
                print("set ios max cache size.");
                SensorsDataAPI.SetiOSMaxCacheSize(20);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("delete all"))
            {
                print("delete all.");
                SensorsDataAPI.DeleteAll();
            }
            GUILayout.Space(20);
            if (GUILayout.Button("set flush bulk size"))
            {
                print("set flush bulk size.");
                SensorsDataAPI.SetFlushBulkSize(90);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("set flush interval"))
            {
                print("set flush inteval.");
                SensorsDataAPI.SetFlushInterval(10 * 1000);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("set flush network type"))
            {
                print("set flush network type.");
                SensorsDataAPI.SetFlushNetworkPolicy((int)NetworkType.TYPE_ALL);
            }

            GUILayout.Space(20);
            if (GUILayout.Button("profile set once"))
            {
                print("profile set once.");
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("age", "9999");
                SensorsDataAPI.ProfileSetOnce(dictionary);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("track installation"))
            {
                print("track installation.");
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("testinstall", "9999");
                SensorsDataAPI.TrackAppInstall(dictionary, true);
            }
        }
        else
        {
            if (GUILayout.Button("Load Second Scence"))
            {
                SceneManager.LoadScene("Second");
            }
            //场景一中定义的测试按钮
            GUILayout.Space(20);
            if (GUILayout.Button("Identiy"))
            {
                print("identity test.");
                SensorsDataAPI.Identify("identify_custom_123");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("Login"))
            {
                print("Login test.");
                SensorsDataAPI.Login("zhangwei_login");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("Logout"))
            {
                print("logout test.");
                SensorsDataAPI.Logout();
            }
            GUILayout.Space(20);
            if (GUILayout.Button("Profile Set"))
            {
                print("profile set test.");
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("age", "18888888888888");
                SensorsDataAPI.ProfileSet(dictionary);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("register super properties"))
            {
                print("register super properties test.");
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("super_test1", "tttt");
                dictionary.Add("super_test2", "ssss");

                SensorsDataAPI.RegisterSuperProperties(dictionary);
            }
            GUILayout.Space(20);
            if (GUILayout.Button("get supper properties"))
            {
                print("get super properties test.");
                Dictionary<string, object> superProperties = SensorsDataAPI.GetSuperProperties();
                print("result is: " + SAUtils.Parse2JsonStr(superProperties));
            }
            GUILayout.Space(20);
            if (GUILayout.Button("unregister super properties"))
            {
                print("unregister super properties.");
                SensorsDataAPI.UnregisterSuperProperty("super_test1");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("clear super properties"))
            {
                print("clear super properties.");
                SensorsDataAPI.ClearSuperProperties();
            }
            GUILayout.Space(20);
            if (GUILayout.Button("start timer"))
            {
                print("start timer.");
                SensorsDataAPI.TrackTimerStart("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("pause timer"))
            {
                print("pause timer.");
                SensorsDataAPI.TrackTimerPause("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("resume timer"))
            {
                print("resume timer.");
                SensorsDataAPI.TrackTimerResume("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("end timer"))
            {
                print("end timer.");
                SensorsDataAPI.TrackTimerEnd("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("clear timer"))
            {
                print("clear timer.");
                SensorsDataAPI.ClearTrackTimer();
            }
            GUILayout.Space(20);
            if (GUILayout.Button("remove timer"))
            {
                print("remove timer.");
                SensorsDataAPI.RemoveTimer("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("flush"))
            {
                print("flush test.");
                SensorsDataAPI.Flush();
            }

        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();


        //GUI.Label(new Rect(padding, height, maxWidth, 80), "Sensors Data", guiStyle);
        //height += 80 + padding;

        //if (GUI.Button(new Rect(padding, height, maxWidth, 100), "Track 一条事件"))
        //{
        //    print("Track an Event.");
        //    Dictionary<string, object> dictionary = new Dictionary<string, object>();
        //    dictionary.Add("country", "中国");
        //    dictionary.Add("address", "中国2222");
        //    SensorsDataAPI.Track("ViewProduct", dictionary);
        //}
    }

    void Start()
    {

    }

    private void setButtonText(Button button, string text)
    {
        button.GetComponentInChildren<Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {

    }

    Dictionary<string, object> IDynamicSuperProperties.getProperties()
    {
        print("======12312312");
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("isGirl", finalResult);
        finalResult = !finalResult;
        return dic;
    }
}
