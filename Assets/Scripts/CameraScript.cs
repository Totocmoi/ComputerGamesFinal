using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject gameMenu;
    private float x, z, speed = 50f;
    private bool isMenu= false;
    private PhysicsRaycaster raycaster;

    void Start()
    {
        raycaster = GetComponent<PhysicsRaycaster>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameMenu.SetActive(isMenu);
            raycaster.enabled = isMenu;
            isMenu = !isMenu;
            menu.SetActive(isMenu);
        }
        if (!isMenu)
        {
            z = Input.GetAxis("Vertical");
            x = Input.GetAxis("Horizontal");
            Vector3 newpos = transform.position + new Vector3(x, 0, z) * Time.deltaTime * speed;
            if (newpos.z < -30) newpos.z = -30;
            if (newpos.x < -50) newpos.x = -50;
            if (newpos.z > 30) newpos.z = 30;
            if (newpos.x > 50) newpos.x = 50;
            transform.position = newpos;
        }
    }
}
