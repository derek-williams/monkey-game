using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[DisallowMultipleComponent, ExecuteInEditMode]
public class UIController : MonoBehaviour
{
	public enum VisualState
	{
		Shown,
		Hidden
	}

	[SerializeField]
	private UIManager.UIControllerID _controllerId = UIManager.UIControllerID.None;
	public UIManager.UIControllerID ID
	{
		get { return _controllerId; }
		//set { }
	}

	[Serializable]
	public class VisualStateEvent : UnityEvent<UIController, VisualState, bool> { }

	public VisualStateEvent onVisualStateChange = new VisualStateEvent();

	private VisualState _currentVisualState = VisualState.Hidden;


	protected virtual bool IsActive()
	{
		return (this.enabled && this.gameObject.activeInHierarchy);
	}

	public void OnSelect(BaseEventData eventData)
	{
		//throw new NotImplementedException();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//throw new NotImplementedException();
	}

	public virtual void Show()
	{
		if (!this.IsActive())
			return;

		if (_currentVisualState == VisualState.Shown)
			return;

		_currentVisualState = VisualState.Shown;
		onVisualStateChange.Invoke(this, _currentVisualState, true);
	}

	public virtual void Hide()
	{
		if (!this.IsActive())
			return;

		if (_currentVisualState == VisualState.Hidden)
			return;

		_currentVisualState = VisualState.Hidden;
		onVisualStateChange.Invoke(this, _currentVisualState, false);
	}
}