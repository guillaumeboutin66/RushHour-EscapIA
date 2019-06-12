using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customTimer : MonoBehaviour
{
    public Text minuteText;
    public Text secondesText;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("OutputTime", 1f, 1f);  //1s delay, repeat every 1s
    }

    // Update is called once per frame
    void OutputTime()
    {
        int secondeTmp;
        int.TryParse(secondesText.text, out secondeTmp);
        int secondes = secondeTmp - 1;
        if(secondes <= 0){
        	int minuteTmp;
        	int.TryParse(minuteText.text, out minuteTmp);
        	int minutes = minuteTmp - 1;
        	minuteText.text = minutes.ToString ();
        	secondes = 59;
        }
        secondesText.text = secondes.ToString ();
    }
}
