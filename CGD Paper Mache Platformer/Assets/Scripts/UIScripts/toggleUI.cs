using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleUI : MonoBehaviour
{
    public Animator anim;
    bool ui_on;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ui_on = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("UI") && ui_on == false)
        {
            ui_on = true;
        }
        else if (Input.GetButtonDown("UI") && ui_on == true)
        {
            ui_on = false;
        }

        if (ui_on == true)
        {
            anim.SetBool("UiOn", true);
        }
        else if (ui_on == false)
        {
            anim.SetBool("UiOn", false);
        }

    }
}
