using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    PlayerFeedBack script;

    DragonController dragonController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        dragonController = FindObjectOfType<DragonController>();
        if (dragonController != null )
        {
            if (dragonController.getCurrentHealth() <= 0)
            {
                script = FindObjectOfType<PlayerFeedBack>();
                script.Victory();
            }
        }
    }
}
