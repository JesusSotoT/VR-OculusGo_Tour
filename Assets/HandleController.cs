using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandleController : MonoBehaviour {

    public Material skybox1;
    public Material skybox2;

    public GameObject btn1;
    public GameObject btn2;

    public GameObject mainCamera;
    float m_FieldOfView;

    bool isSkybox1;

    private void Start()
    {
        // Set default FOV
        m_FieldOfView = 60f;
    }
    // Update is called once per frame
    void Update () {
        mainCamera.GetComponent<Camera>().fieldOfView = m_FieldOfView;
    }
    public void changeScene(){
        if(isSkybox1){
            RenderSettings.skybox = skybox2;
        }else{
            RenderSettings.skybox = skybox1;
        }
        isSkybox1 = !isSkybox1;
       
    }

}
