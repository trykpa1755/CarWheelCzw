using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceController : MonoBehaviour
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public Text startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;

    public CheckPointController[] carsController;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startText.gameObject.SetActive(false);
        InvokeRepeating("CountDown", 3, 1);
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];
        for(int i=0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    void LateUpdate()
    {
        int finishedLap = 0;
        foreach (CheckPointController controller in carsController)
        {
            if (controller.lap == totalLaps + 1) finishedLap++;
            if (finishedLap == carsController.Length && racing)
            {
                Debug.Log("FinishRace");
                racing = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CountDown()
    {
        startText.gameObject.SetActive(true);
        if (timer != 0)
        {
            startText.text = timer.ToString();
            audioSource.PlayOneShot(count);
            timer--;
        }
        else
        {
            startText.text = "START!!!";
            audioSource.PlayOneShot(start);
            racing = true;
            CancelInvoke("CountDown");
            Invoke("HideStartText", 1);
        }
    }

    void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }
}
