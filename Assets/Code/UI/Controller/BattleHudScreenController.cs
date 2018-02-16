using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleHudScreenController : MonoBehaviour
{
    #region Members
    public GameObject eventTemplate;
    public GameObject actionTemplate;
    public GameObject previewTemplate;
    public GameObject dealTemplate;
    public GameObject parentPanel;
    public GameObject previewPanel;
    public GameObject dealPanel;
    public GameObject cardContainer;
    public GameObject infoPanel;
    public Button submitButton;
    public Text roundCounter;

    private List<int> _activePlayers = new List<int>();
    public float waitTime;
    private bool didSkip;
    private List<GameObject> toDestroy;

    public GameObject eventSystem;
    public GameObject eventSystemCardTemplate;
    public GameObject eventPanel;
    #endregion

    #region Input / Initialization
    public void OnVisualStateChange(UIController inController, UIController.VisualState inState, bool inValue)
    {
        if (inState == UIController.VisualState.Shown)
        {
            InitHud();
        }
        else
        {
            // we clear out the hand first ? 
            ClearHandPanel();

            HideCards();

            gameObject.SetActive(false);

            if (previewPanel != null)
            {
                DoCleanUpCardPreview();
            }

            if (dealPanel != null)
            {
                foreach (Transform child in dealPanel.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            cardContainer.SetActive(false);
        }
    }

    private void InitHud()
    {
        gameObject.SetActive(true);
        toDestroy = new List<GameObject>();
        didSkip = false;
        if (submitButton != null)
        {
            submitButton.onClick.RemoveAllListeners();
        }

        //ShowCards();
    }
    #endregion


    #region Cleanup
    private IEnumerator DoCleanUpDealHand(float delay)
    {

        yield return new WaitForSeconds(delay);
        foreach (Transform child in dealPanel.transform)
        {
            toDestroy.Add(child.gameObject);
        }

        dealPanel.SetActive(false);
        didSkip = false;

        yield return new WaitForSeconds(5f);
        foreach (GameObject g in toDestroy)
        {
            Destroy(g);
        }
        dealPanel.GetComponent<CanvasGroup>().alpha = 1;

    }

    private IEnumerator HideCard(float hideDelay, string hide, GameObject card)
    {
        if (!didSkip)
        {
            if (card != null)
            {
                yield return new WaitForSeconds(hideDelay);
                card.GetComponent<Animation>().Play(hide);
            }
        }
    }

    public void DoCleanUpCardPreview()
    {
        if (previewPanel != null)
        {
            //ShowCards();

            var hg = previewPanel.GetComponentInChildren<HorizontalLayoutGroup>();
            if (hg != null)
            {
                foreach (Transform child in hg.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            UIManager.Instance.PopUIController(UIManager.UIControllerID.CardPreview);
            previewPanel.SetActive(false);

            _activePlayers.Clear();
        }
    }

    public void InfoPanelHide(string hide)
    {
        if (infoPanel != null)
        {
            //unity is being super shitty and not finding the animation even though its in the project
            //infoPanel.GetComponent<Animation>().Play(hide);
            infoPanel.SetActive(false);
        }
    }

    private void ClearHandPanel()
    {
        // clear out the panel! 
        if (parentPanel != null)
        {
            foreach (Transform child in parentPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void HideCards()
    {
        if (cardContainer != null)
        {
            cardContainer.SetActive(false);
        }
    }

    public void CleanUpEventSystem()
    {
        var players = eventSystem.transform.GetChild(2);
        var playerOne = players.transform.GetChild(0);
        var playerTwo = players.transform.GetChild(1);
        var playerThree = players.transform.GetChild(2);

        foreach (Transform t in playerOne)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in playerTwo)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in playerThree)
        {
            Destroy(t.gameObject);
        }
        eventSystem.SetActive(false);
    }
    #endregion
}