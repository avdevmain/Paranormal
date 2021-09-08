using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text textComp;

   // [SyncVar]
    public int maxSeconds = 5;

    private int currSeconds;

   // [SyncVar (hook = nameof(OnCountdownStart))]
    //public bool countdownStarted = false; 

    void Awake() {
        currSeconds = maxSeconds;
    }
    public void StartCoundown()
    {
        StartCoroutine(ProcessCountdown());
    }

    public int SecondsLeft()
    {
        return currSeconds;
    }
    private IEnumerator ProcessCountdown()
    {
        while (currSeconds>0)
        {
            yield return new WaitForSecondsRealtime(1f);
            currSeconds -= 1;

            int minutes = currSeconds/60;
            int seconds = currSeconds - minutes * 60;
            if (seconds >= 10)
                textComp.text = minutes.ToString() + ":" + seconds.ToString(); 
            else 
             textComp.text = minutes.ToString() + ":0" + seconds.ToString(); 

        }

        yield return null;
    }
}
