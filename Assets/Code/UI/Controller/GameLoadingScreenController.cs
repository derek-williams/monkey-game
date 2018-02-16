
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LibGameClient.UI.Controller
{
	public class GameLoadingScreenController : MonoBehaviour
	{
		public Image Background;
		public Sprite InGameImage;
		public Sprite EndGameImage;

		public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValue)
		{
			if (inState == UIController.VisualState.Shown)
			{
				InitLoading();
			}
			else
			{
				gameObject.SetActive(false);
			}
		}

		private void InitLoading()
		{
            gameObject.SetActive(true);

			if (GameManager.Instance.CurrentApplicationState == ApplicationState.LoadingGame)
			{
				// setup the InGame loading screen!
				if (Background != null && InGameImage != null)
				{
					Background.sprite = InGameImage;
				}
			}
			else if (GameManager.Instance.CurrentApplicationState == ApplicationState.EndGame)
			{
				// setup the InGame loading screen!
				if (Background != null && EndGameImage != null)
				{
					Background.sprite = EndGameImage;
				}
			}
			else
			{
				// we just set it to a white screen 
				if(Background != null)
				{
					Background.color = Color.white;
				}
			}

			gameObject.SetActive(true);

			// TODO: We may need to tell the simulation manager here instead of where it is not for the future
			if (GameManager.Instance.CurrentApplicationState == ApplicationState.LoadingGame)
			{
			}
		}

	}
}
