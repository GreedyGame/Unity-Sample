using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GreedyGame.Runtime;
using GreedyGame.Platform;
using GreedyGame.Runtime.Units;
using GreedyGame.Commons;

public class GreedyCampaignLoader : SingletoneBase<GreedyCampaignLoader>
{

    public List<string> unitList;

    public bool AdmobMediation = false;

    GGAdConfig adConfig = new GGAdConfig();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (RuntimePlatform.Android == Application.platform)
        {
            adConfig.setListener(new GreedyAgentListener());
            adConfig.enableAdmobMediation(AdmobMediation);
            adConfig.addUnitList(unitList);
            Debug.Log("GDPRGG START");
        }
        else
        {
            moveToNextScene();
        }
    }

    private static void moveToNextScene()
    {
        if (Application.loadedLevel == 0)
        {
            Application.LoadLevel(1);
        }
    }

    public class GreedyAgentListener : IAgentListener
    {

        public void onAvailable(string campaignId)
        {
            /**
         * TODO: New campaign is available and ready to use for the next scene.
         **/

        }

        public void onUnavailable()
        {
            /**
         * TODO: No campaign is available, proceed with normal flow of the game.
         **/
        }

        public void onFound()
        {
            /**
         * TODO: Campaign is found. Starting download of assets. This will be followed by onAvailable callback once download completes successfully.
         **/
        }

        public void onError(string error)
        {
            /**
         * TODO: No Campaign will be served since the initialization resulted in an error. 
         * If device api level is below 15 this callback is invoked.
         **/
        }

    }

    public static void showFloat(String f_id)
    {
        Debug.Log(String.Format("Fetching FloatUnit {0}", f_id));
        GreedyGameAgent.Instance.fetchFloatUnit(f_id);
    }

    public static void removeFloatAd(string FloatUnit)
    {
        Debug.Log(String.Format("Remove FloatUnit"));
        GreedyGameAgent.Instance.removeFloatUnit(FloatUnit);
    }

    public static void removeAllFloatAds()
    {
        Debug.Log(String.Format("Remove AllFloatUnits"));
        GreedyGameAgent.Instance.removeAllFloatUnits();

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 4, Screen.height -100, 100, 30), "Start Game"))
        {
            Debug.Log("GG[ButtonLoaderScene] Start Game ");
            GreedyGameAgent.Instance.init(adConfig);
            moveToNextScene();

        }

        if (GUI.Button(new Rect(Screen.width / 2, Screen.height -100, 100, 30), "Personalized"))
        {
            Debug.Log("GG[ButtonLoaderScene] Start Game ");
            adConfig.enforceGgNpa(true);
        }



    }

}
