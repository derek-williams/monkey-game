using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{
	public GameObject PublisherSplash;
	public GameObject CompanySplash;

	private bool _publisherPlayed = false;
	private bool _companyPlayed = false;

	private void Awake()
	{
		if (PublisherSplash != null)
		{
			PublisherSplash.SetActive(false);
		}

		if (CompanySplash != null)
		{
			CompanySplash.SetActive(false);
		}
	}

	public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValue)
	{
		if (inState == UIController.VisualState.Shown)
		{
			InitSplashScreen();
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void InitSplashScreen()
	{
		gameObject.SetActive(true);

		if (_publisherPlayed || _companyPlayed)
			return;

		if (CompanySplash != null)
		{
			_companyPlayed = true;
			_publisherPlayed = true;
			CompanySplash.SetActive(true);
		}
	}

	public void PumpState(Animator inAnim)
	{
		if (_publisherPlayed && _companyPlayed)
		{
			if (PublisherSplash != null)
			{
				PublisherSplash.SetActive(false);
			}

			if (CompanySplash != null)
			{
				CompanySplash.SetActive(false);
			}

			// We should figure out how to make this work a little better ? 
			UIManager.Instance.PopUIController(UIManager.UIControllerID.Splash);
			GameManager.Instance.CurrentApplicationState = ApplicationState.Loading;

			return;
		}

		if (_publisherPlayed && !_companyPlayed)
		{
			if (PublisherSplash != null)
			{
				PublisherSplash.SetActive(false);
			}

			if (CompanySplash != null)
			{
				_companyPlayed = true;
				CompanySplash.SetActive(true);
			}
		}
	}
}