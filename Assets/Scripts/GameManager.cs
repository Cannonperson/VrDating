using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text text;
    public Slider slider;
    public Image drinkSlider;

    public bool nod;
    public bool shake;
    public bool willDrink = false;
    public float irritation = 0;
    public float irritationsMultiplier;

    private Date date;

	void Start () {
        date = GameObject.FindGameObjectWithTag("Date").GetComponent<Date>();

        //TEST-CODE
        DialogueTree tree = new DialogueTree
        {
            rootNode = new DialogueNode
            {
                message = "Hey there, you doing good?",
                yesOption = new DialogueNode
                {
                    message = "Good, I take it you're here for the speed-dating thing?"
                },
                noOption = new DialogueNode
                {
                    message = "Aw shucks, that's too bad. How about a drink?"
                }
            }
        };
        date.dialogueTree = tree;
        date.StartDate();
        //TEST-CODE
    }
	
	void Update ()
    {
        HandleIrritation();
        if (transform.rotation.eulerAngles.x > 20f && transform.rotation.eulerAngles.x < 90f)
        {
            StartCoroutine("Nodding");
        }
        if (transform.rotation.eulerAngles.y > 10f && transform.rotation.eulerAngles.y < 90f)
        {
            StartCoroutine("Shaking");
        }
    }

    public void StartDrinking()
    {
        willDrink = true;
        StartCoroutine("DrinkBeer");
    }
    
    public void StopDrinking()
    {
        willDrink = false;
    }

    IEnumerator DrinkBeer()
    {
        float timestamp = Time.time;
        while (willDrink == true)
        {
            yield return null;
            drinkSlider.fillAmount = (Time.time - timestamp) / 2;
            if (Time.time > timestamp + 2)
            {
                irritation -= 10;
                irritationsMultiplier += 0.5f;
                timestamp = 0f;
            }
        }
        drinkSlider.fillAmount = 0;
    }

    void HandleIrritation()
    {
        irritation += irritationsMultiplier * Time.deltaTime;
        slider.value = irritation;
        slider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, irritation/100);
    }

    IEnumerator Nodding()
    {
        float timeStamp = Time.time;
        while(Time.time < timeStamp + 0.6f)
        {
            yield return null;
            if(transform.rotation.eulerAngles.x > 300f && transform.rotation.eulerAngles.x < 355f)
            {
                nod = true;
                text.text = "Yes";
                date.SetAnswer(true);
                break;
            }
        }
        yield return new WaitForSeconds(3);
        nod = false;
    }

    IEnumerator Shaking()
    {
        float timeStamp = Time.time;
        while (Time.time < timeStamp + 0.6f)
        {
            yield return null;
            if (transform.rotation.eulerAngles.y > 300f && transform.rotation.eulerAngles.y < 350f)
            {
                shake = true;
                text.text = "No";
                date.SetAnswer(false);
                break;
            }
        }
        yield return new WaitForSeconds(3);
        shake = false;
    }
}
