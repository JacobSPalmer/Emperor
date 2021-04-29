using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leaderboardInterScript : MonoBehaviour
{
    public GameObject EmperorDBObj;
    public List<Text> Rank1;
    public List<Text> Rank2;
    public List<Text> Rank3;
    public List<Text> Rank4;
    public List<Text> Rank5;

    public List<Text>[] Ranks;
    // Start is called before the first frame update
    void Start()
    {
        Ranks = new List<Text>[] {Rank1,Rank2,Rank3,Rank4,Rank5};
        List<User> rankedUsers = EmperorDBObj.GetComponent<EmperorDB>().generateLeaderboard();
        int i = 0;
        foreach(User u in rankedUsers){
            setRankStatus(i, u);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRankStatus(int rank, User user){
        Ranks[rank][0].text = user.UserName;
        Ranks[rank][1].text = user.UserScore.ToString();
        Ranks[rank][2].text = user.UserMoves.ToString();
        Ranks[rank][3].text = user.UserTime;
    }

}
