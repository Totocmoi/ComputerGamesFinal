using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentButtonScript : MonoBehaviour
{
    public GameObject component;
    private Button button;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        button = GetComponent<Button>();
        button.onClick.AddListener(NewObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewObject()
    {
        Instantiate(component, cam.transform.position + new Vector3(0,0,20), new Quaternion());
    }
}
