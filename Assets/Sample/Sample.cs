using System;
using System.Collections.Generic;
using SensorDataAnalytics.Utils;
using SensorsAnalytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SensorsAnalytics.Wrapper;

public class Sample : MonoBehaviour, IDynamicSuperProperties
{
    private const string Name = "profileSet";
    public GUIStyle guiStyle;
    public GUISkin guiSkin;
    private Vector2 scrollVector2;
    private bool finalResult = false;

    static Sample saInstance;
    // Start is called before the first frame update
    void Awake()
    {
        //判断是否需要 SDK 处理 
        if (saInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            saInstance = this;
            Application.deepLinkActivated += handSchemeUrl;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void handSchemeUrl(string url)
    {
        print("======deeplink url:  " + url);
        SensorsDataAPI.HandleSchemeUrl(url); //调用封装好的方法
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
            dictionary.Add("current_time", dateTime);
            dictionary.Add("double_value", 1234.567);
            dictionary.Add("float_value", 12.12f);
            dictionary.Add("int_value", 1234);
            dictionary.Add("bool_value", true);
            dictionary.Add("array_int", new int[] { 1, 2, 3, 4, 5 });
            dictionary.Add("array_string", new string[] { "item1", "item2", "item3", "item4" });
            dictionary.Add("current_language", Application.systemLanguage.ToString());

            print("Track an Event.");

            SensorsDataAPI.Track("track_test1", dictionary);
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
                SensorsDataAPI.SetiOSMaxCacheSize(12000);
            }

            GUILayout.Space(20);
            if (GUILayout.Button("set pc max cache size"))
            {
                print("set pc max cache size.");
                SensorsDataAPI.SetPCMaxCacheSize(12000);
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
            if (GUILayout.Button("Identify"))
            {
                var random = new System.Random();
                int randomId = random.Next(100000, 999999);
                print($"identity test, anonymousId = identify_{randomId}");
                SensorsDataAPI.Identify($"identify_{randomId}");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("Login"))
            {
                print("Login test.");
                SensorsDataAPI.Login("login_test123");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("Logout"))
            {
                print("logout test.");
                SensorsDataAPI.Logout();
            }

            GUILayout.Space(20);
            if (GUILayout.Button("distinctId"))
            {
                string distinctId = SensorsDataAPI.DistinctId();
                print("get distinctId: " + distinctId);

                //Dictionary<string, object> dictionary = new Dictionary<string, object>();
                //dictionary.Add("distinctId", distinctId);
                //SensorsDataAPI.Track("track_distinctId", dictionary);
            }

            GUILayout.Space(20);
            if (GUILayout.Button("loginId"))
            {
                string loginId = SensorsDataAPI.LoginId();
                print("get loginId: " + loginId);

                //Dictionary<string, object> dictionary = new Dictionary<string, object>();
                //dictionary.Add("loginId", loginId);
                //SensorsDataAPI.Track("track_loginId", dictionary);
            }

            GUILayout.Space(20);
            if (GUILayout.Button("anonymousId"))
            {
                string anonymousId = SensorsDataAPI.AnonymousId();
                print("get anonymousId: " + anonymousId);
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
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                print($"start timer: {dateTime}");
                SensorsDataAPI.TrackTimerStart("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("pause timer"))
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                print("pause timer: " + dateTime);
                SensorsDataAPI.TrackTimerPause("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("resume timer"))
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                print("resume timer: " + dateTime);
                SensorsDataAPI.TrackTimerResume("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("end timer"))
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                print("end timer: " + dateTime);
                SensorsDataAPI.TrackTimerEnd("BuyGoods");
            }
            GUILayout.Space(20);
            if (GUILayout.Button("clear timer"))
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                print("clear timer: " + dateTime);
                SensorsDataAPI.ClearTrackTimer();
            }
            GUILayout.Space(20);
            if (GUILayout.Button("remove timer"))
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                print("remove timer: " + dateTime);
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
