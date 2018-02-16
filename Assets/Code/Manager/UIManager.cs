using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIManager : BaseManager
{
    public enum UILocation
    {
        Invalid,
        Splash,
        Loading,
        MainMenu,
        MainGame,
        BattleHud,
        NumLocations,
        MatchLoading,
        Inventory,
        VendingMachine,
    }

    public enum UIControllerID : int
    {
        None = 0,
        Splash = 1,
        Loading = 2,
        MainMenu = 3,
        LoadingGame = 4,
        BattleHud = 5,
        BoardSelection = 6,
        HeroSelection = 7,
        DialogBox = 8,
        JoinGameSelection = 9,
        SocialDialog = 10,
        CardPreview = 11,
        MatchLoading = 12,
        Swapping = 13,
        Inventory = 14,
        VendingMachine = 15,
    }

    public static UIManager Instance { get; private set; }

    public UILocation CurrentLocation { get; private set; }
    public GameObject UIRootGameObject;
    public GameObject UICamera;
    public GameManager EventSystem;

    private List<UIController> mControllers = new List<UIController>();

    public override void Init()
    {
        if (Instance == null)
            Instance = this;

        // we initialize to invalid 
        CurrentLocation = UILocation.Invalid;

        // we grab the UI Root and we set it not to destroy so when we load different scenes
        // we can change objects and such :P 
        UIRootGameObject = GameObject.FindGameObjectWithTag("UIRoot");
        UnityEngine.Object.DontDestroyOnLoad(UIRootGameObject);


        var controllers = UIRootGameObject.GetComponentsInChildren<UIController>(true);
        foreach (var item in controllers)
        {
            mControllers.Add(item);
        }
    }

    public override void Begin()
    {
        base.Begin();

        // we hook into this event so we can know when a game state changes. 
        GameManager.Instance.OnApplicationStateChanged += OnApplicationStateChange;
    }

    public void OnApplicationStateChange(ApplicationState toState, ApplicationState fromState)
    {
        if (toState == fromState) return;

        var oldUILocation = CurrentLocation;
        switch (toState)
        {
            case ApplicationState.Invalid:
                break;
            case ApplicationState.Splash:
                PushUIController(UIControllerID.Splash);
                break;
            case ApplicationState.Loading:
                PushUIController(UIControllerID.Loading);
                break;
            case ApplicationState.MainMenu:
                PushUIController(UIControllerID.MainMenu);
                break;
            case ApplicationState.Inventory:
                PushUIController(UIControllerID.Inventory);
                break;
            case ApplicationState.VendingMachine:
                PushUIController(UIControllerID.VendingMachine);
                break;
            case ApplicationState.SetupGame:
                PushUIController(UIControllerID.BoardSelection, true);
                break;
            case ApplicationState.JoinGame:
                PushUIController(UIControllerID.JoinGameSelection, true);
                break;
            case ApplicationState.CharacterSelection:
                PushUIController(UIControllerID.HeroSelection, true);
                break;
            case ApplicationState.LoadingGame:
                PushUIController(UIControllerID.LoadingGame);
                break;
            case ApplicationState.MatchLoading:
                PushUIController(UIControllerID.MatchLoading);
                break;
            case ApplicationState.InGame:
                PushUIController(UIControllerID.BattleHud);
                break;
            case ApplicationState.EndResults:
                break;
            case ApplicationState.EndGame:
                PushUIController(UIControllerID.LoadingGame);
                break;
            default:
                break;
        }
    }

    public List<UIController> GetUIControllers()
    {
        return mControllers;
    }

    public UIController GetUIController(UIControllerID inID)
    {
        try
        {
            var controller = mControllers.Find(x => x.ID == inID);
            return controller;
        }
        catch (System.Exception)
        {
            UnityEngine.Debug.LogError("Can't find ID " + inID.ToString());
        }

        return null;
    }

    public void PushUIController(UIControllerID inID, bool bringToFront = false)
    {
        var controller = GetUIController(inID);
        if (controller != null)
        {
            controller.Show();

            if (bringToFront)
            {
                // this will brint it to the front of the UI! 
                UIUtils.BringToFront(controller.gameObject);
            }
        }
    }

    public void PopUIController(UIControllerID inID)
    {
        var controller = GetUIController(inID);
        if (controller != null)
        {
            controller.Hide();
        }
    }
}