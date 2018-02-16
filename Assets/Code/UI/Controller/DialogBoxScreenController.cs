using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LibGameClient.UI.Controller
{
	public class DialogBoxScreenController : MonoBehaviour
	{
		public enum DialogType
		{
			None,
			OkCancel,
			ContinueConnect,
			Ok,
		}
		public static Action PositiveAction;
		public static Action NegativeAction;
		public static string Title;
		public static DialogType DialogSetupType;
		

		public Button PositiveButton;
		public Button NegativeButton;
		public Text TitleText;

		public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValue)
		{
			if (inState == UIController.VisualState.Shown)
			{
				InitDialog();
			}
			else
			{
				PositiveAction = null;
				NegativeAction = null;
				Title = "";
				DialogSetupType = DialogType.None;

				gameObject.SetActive(false);
			}
		}

		public void OnPositiveButtonClick()
		{
			if (PositiveAction != null)
			{
				PositiveAction();
			}
		}

		public void OnNegativeButtonClick()
		{
			if (NegativeAction != null)
			{
				NegativeAction();
			}
		}

		public static void SetupDialog(string inTitle, DialogType inType, Action inPositiveAction, Action inNegativeAction)
		{
			Title = inTitle;
			DialogSetupType = inType;
			PositiveAction = inPositiveAction;
			NegativeAction = inNegativeAction;
		}

		private void InitDialog()
		{
			gameObject.SetActive(true);

			TitleText.text = Title;
			switch (DialogSetupType)
			{
				case DialogType.None:
					// we should cancel out here! 
				break;
				case DialogType.OkCancel:
					if (PositiveButton != null)
					{
						PositiveButton.GetComponentInChildren<Text>().text = "OK";
					}
					if (NegativeButton != null)
					{
						NegativeButton.gameObject.SetActive(true);
						NegativeButton.GetComponentInChildren<Text>().text = "Cancel";
					}
				break;
				case DialogType.Ok:
				if (PositiveButton != null)
				{
					PositiveButton.GetComponentInChildren<Text>().text = "OK";
				}
				if (NegativeButton != null)
				{
					NegativeButton.gameObject.SetActive(false);
				}
				break;
				case DialogType.ContinueConnect:
					if (PositiveButton != null)
					{
						PositiveButton.GetComponentInChildren<Text>().text = "Continue";
					}
					if (NegativeButton != null)
					{
						NegativeButton.gameObject.SetActive(true);
						NegativeButton.GetComponentInChildren<Text>().text = "Reconnect";
					}
				break;
			}
		}
	}
}
