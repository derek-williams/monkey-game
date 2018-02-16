using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }
		public static DateTime StartupTime { get; private set; }

		public bool UseWebForData = false;
		public bool UseIAServiceNetwork = false;
		public string TEMPWebURL = "http://localhost:1234/bundles/";
		public string SERVICEURL = "http://director-dev.ignitedartists.com/v1/service/";
		public string GAMESERVERURL = "http://10.26.14.73:8080";
    public string LocalPlayerId = "aaaaaa";
		public string PlayerName = "HelloWorld";

		private ApplicationState _currentApplicationState;
		private ApplicationState _previousApplicationState;
		public ApplicationState CurrentApplicationState
		{
			get { return _currentApplicationState; }
			set
			{
				_previousApplicationState = _currentApplicationState;
				_currentApplicationState = value;

				if (OnApplicationStateChanged != null)
				{
					OnApplicationStateChanged(_currentApplicationState, _previousApplicationState);
				}
			}
		}

		public System.Action<ApplicationState, ApplicationState> OnApplicationStateChanged;

		public bool HasDoneInitialNetCheck = false;
		private bool _isOnlineState = false;
		public bool IsOnlineState
		{
			get { return _isOnlineState; }
			set
			{
				bool previousState = _isOnlineState;
				_isOnlineState = value;

				if (OnInternetStateChange != null)
				{
					OnInternetStateChange(previousState, _isOnlineState);
				}
			}
		}
		public System.Action<bool, bool> OnInternetStateChange;

		private BaseManager[] managers = new BaseManager[0];

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;

				_currentApplicationState = ApplicationState.Invalid;

				DontDestroyOnLoad(gameObject);

				StartupTime = DateTime.Now;

				SetupManagers();

				if (!Application.isEditor)
				{
					// we for this to true if we are not in the editor! We should always try and get to the web
					// we should likely not do it this way but it will help for now!
					UseWebForData = true;
				}
			}
		}

		private void SetupManagers()
		{
			List<BaseManager> mm = new List<BaseManager>();

			mm.Add(new UIManager());

			managers = mm.ToArray();
			foreach (BaseManager c in managers)
			{
				c.Init();
			}
		}

		private bool started = false;
		private void Start()
		{
			if (!started)
			{
				started = true;
				foreach (BaseManager c in managers)
				{
					c.Begin();
				}
			}

			StartCoroutine(StartGame());
		}

		private IEnumerator StartGame()
		{
			yield return new WaitForSeconds(.1f);

			GameManager.Instance.CurrentApplicationState = ApplicationState.Splash;
		}

		private void Update()
		{
			float time = Time.time;
			float dt = Time.deltaTime;

			foreach (BaseManager c in managers)
			{
				c.Update(time, dt);
			}
		}

		private void OnDestroy()
		{
			foreach (BaseManager c in managers)
			{
				c.Destroy();
			}
		}

		void OnApplicationQuit()
		{

		}

		void OnApplicationPause(bool inPause)
		{
			StartInternetCheck();
		}

		public void StartInternetCheck()
		{
			StartCoroutine(CheckInternetConnection());
		}

		IEnumerator CheckInternetConnection()
		{
			// so we make a web request to a know website! 
			// right now we are going to use a google website!
			var rand = new System.Random();

			var urlTestString = "http://check.ignitedartists.com/?d=" + rand.Next();
			var request = new WWW(urlTestString);

			yield return request;

			if (string.IsNullOrEmpty(request.error))
			{
				var status = request.responseHeaders["STATUS"];
				Debug.Log(status);

				if (!status.Contains("200"))
				{
					// its very likely then we have no Internet connection ? 
					GameManager.Instance.IsOnlineState = false; // tell the whoever cares we are no longer online! ? 
				}
				else
				{
					GameManager.Instance.IsOnlineState = true;
				}
			}
			else
			{
				Debug.LogError(request.error);
				GameManager.Instance.IsOnlineState = false; // tell the whoever cares we are no longer online! ? 
			}

			GameManager.Instance.HasDoneInitialNetCheck = true;
		}
	}