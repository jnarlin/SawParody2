using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public bool inReach;
    private bool isOpen = false;
    private bool isLocked = false;

    void Start()
    {
        inReach = false;
        door.SetBool("open", isOpen);
        door.SetBool("closed", !isOpen);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach" && isLocked == false)
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            isLocked = true;
            Invoke("DoorCloses", 20f);
            DoorCloses();
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            if(door.GetBool("closed") && !isLocked)
                DoorOpens();
            else
                DoorCloses();
        }
    }

    void LockDoor()
    {
        isLocked = true;
    }
    void DoorOpens ()
    {
        door.SetBool("open", true);
        door.SetBool("closed", false);
        isOpen = true;

    }

    void DoorCloses()
    {
        door.SetBool("open", false);
        door.SetBool("closed", true);
        isOpen = false;
    }

}
