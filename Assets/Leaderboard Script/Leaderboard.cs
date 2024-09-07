using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public static  Leaderboard Instance;
    [SerializeField]
    private List<TextMeshProUGUI>  names=new List<TextMeshProUGUI>();
    [SerializeField]
    private List<TextMeshProUGUI> score;
    [SerializeField]
    private GameObject noInternet;
    [SerializeField]
    private GameObject loading;

    private string leaderBoardPublicKey = "bbfd99bf84fbc2d4171345f35cad3bca5409eac8cb3a158475f68047c39cc455";
    private void Awake()
    {
        Instance = this;   
    }
    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(leaderBoardPublicKey, ((msg) =>{
            int loopLength=(msg.Length < names.Count)?msg.Length:names.Count;
            for(int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                score[i].text = msg[i].Score.ToString();
            }
            loading.SetActive(false);
            noInternet.SetActive(false);
        }));
    }

    public void SetLeaderBoardEntry(string userName,int score)
    {
        LeaderboardCreator.UploadNewEntry(leaderBoardPublicKey,userName, score, ((msg) =>
        {
            GetLeaderBoard();
        }));
    }

    public void OnLeaderBoad()
    {
        loading.SetActive (true);
        noInternet.SetActive (false);
        ResetEntryies();
        if (Application.internetReachability != NetworkReachability.NotReachable ||
               Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
               Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork
               )
        {
            print("net");
            SetLeaderBoardEntry(PlayerPrefs.GetString("PlayerName"), managerdata.manager.getmuving());
        }
        else
        {
            print("No internrt");
            loading.SetActive(false );
            noInternet.SetActive(true);
        }
    }
    void ResetEntryies()
    {
        foreach(TextMeshProUGUI text in names)
        {
            text.text = "";
        }
        foreach (TextMeshProUGUI text in score)
        {
            text.text = "";
        }
    }
}
