using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour {

    private Image content;

    [SerializeField]
    private Text statValue;

    [SerializeField]
    private float LerpSpeed;

    private float currentFill;

    public  float MyMaxValue { get; set; }

    private float currentValue;

    public float myCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            // so health never goes above max value
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0 )
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
            currentFill = currentValue / MyMaxValue;

            statValue.text = currentValue + "/" + MyMaxValue;
        }

    }

    // Use this for initialization
	void Start ()
    {
        content = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount,currentFill, Time.deltaTime * LerpSpeed);
        }
        
	}

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        myCurrentValue = currentValue;
    }
}
