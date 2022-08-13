using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int killedMonsterCount = 0;
    public TextMeshProUGUI killCountText;
    void Start()
    {
        killCountText.text = killedMonsterCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateKillCount()
    {
        killedMonsterCount++;
        killCountText.text = killedMonsterCount.ToString();
    }
}
