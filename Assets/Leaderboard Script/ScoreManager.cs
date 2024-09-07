using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    //public SwipeGame game;
    public GameObject userNamePanel, leaderBoardPanel;
    public int score;
    public TMP_InputField userName;
    public TextMeshProUGUI noInternet,noInternetOnComplete;
    bool check;

    private void Awake()
    {
        check = true;
        Instance= this;
        if (!PlayerPrefs.HasKey("GetUserName"))
        {
            PlayerPrefs.SetInt("GetUserName", 0);
        }
        if (!PlayerPrefs.HasKey("UserName"))
        {
            PlayerPrefs.SetString("UserName", "");
        }
    }
    public void GetScore()
    {
        //score=game.score;
    }
    public void panelOn()
    {
        GetScore();
        if (PlayerPrefs.GetInt("GetUserName") == 0)
        {
            leaderBoardPanel.SetActive(false);
            userNamePanel.SetActive(true);
        }
        else
        {
            leaderBoardPanel.SetActive(true);
            userNamePanel.SetActive(false);
            if (Application.internetReachability != NetworkReachability.NotReachable ||
              Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
              Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork
              )
            {
                Leaderboard.Instance.SetLeaderBoardEntry(PlayerPrefs.GetString("UserName"), score);
            }
            else
            {
                noInternetOnComplete.gameObject.SetActive(true);
            }
        }
    }
    public void GetUserName()
    {
        if (PlayerPrefs.GetInt("GetUserName") == 0)
        {
            if (userName.text != null && !HasSpecialChars(userName.text))
            {
                if (PlayerPrefs.GetInt("GetUserName") == 0)
                {
                    PlayerPrefs.SetString("UserName", userName.text);
                    PlayerPrefs.SetInt("GetUserName", 1);
                }
                leaderBoardPanel.SetActive(true);
                userNamePanel.SetActive(false);
                Leaderboard.Instance.SetLeaderBoardEntry(PlayerPrefs.GetString("UserName"), score);
            }
            else
            {
                //invalid name
            }
        }
        else
        {

        }
    }
    public void OnLeaderBoadPanel()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable ||
               Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
               Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork
               )
        {
            //leaderBoardPanel.SetActive(true);
            //Leaderboard.Instance.GetLeaderBoard();
            Leaderboard.Instance.SetLeaderBoardEntry(PlayerPrefs.GetString("PlayerName"), managerdata.manager.getmuving());
        }
        else
        {
            if (check)
            {
                check = false;
                //noInternet.transform.DOScale(1, 2).SetEase(Ease.OutBounce).OnComplete(() =>
                //noInternet.transform.DOScale(0, 1).SetEase(Ease.Linear).OnComplete(() =>
                //check = true));
            }
        }
    }
   
    private bool HasSpecialChars(string yourString)
    {
        return yourString.Any(ch => !char.IsLetterOrDigit(ch));
    }
}
