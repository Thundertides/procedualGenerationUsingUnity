using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int counter;
    public int tier;
    public GameObject Manager;
    public Button generateButton;

    // Consistantly counts up and changes the seed based off tier and count.
    void tierCounter()
    {
        if(counter == 0)
        {
            Manager.GetComponent<MaterialObject>().tier = tier;

            Manager.GetComponent<MaterialObject>().seed+=counter*tier;

            Manager.GetComponent<MaterialObject>().generate();
            
        }
        else
        {

        Manager.GetComponent<MaterialObject>().seed+=counter*tier;

        Manager.GetComponent<MaterialObject>().generate();

        }
        
        if(counter == 4)
           {
                  counter = 0;
                  tier++;
           }
        else
           {
                  counter++;
           }
    }

    void Start()
    {
        tier = 1;
        counter = 0;

       Button btn = generateButton.GetComponent<Button>();
       btn.onClick.AddListener(tierCounter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
