using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreenController : MonoBehaviour
{
	public Button StartGameButton;
	public Button StartRemoteGame;
	public Button JoinRemoteGame;

	// Temp
	public Button ExitGameButton;
	public Text PlayerName;
	public Text[] CurrencyValues;

	private bool _nameSet;

	[Flags]
	public enum AssetLoadingFlags
	{
		None = 1 << 0,
		LoadedMochibis = 1 << 2,
		LoadedCharacters = 1 << 3,
	}

	public void Awake()
	{
		
	}


	public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValue)
	{
		if (inState == UIController.VisualState.Shown)
		{
			InitMainMenu();
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	private void InitMainMenu()
	{
		gameObject.SetActive(true);
        var setName = PlayerPrefs.GetInt("SetName", 0);
		if (setName == 0)
		{
			UIManager.Instance.PushUIController(UIManager.UIControllerID.SocialDialog);
		}

	}

	public void Update()
	{
        if (!_nameSet)
        {
            var setName = PlayerPrefs.GetInt("SetName", 0);
        }
    }
}