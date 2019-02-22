using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonSetup : MonoBehaviour {
    public int index;
    public string sceneName;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandlePressedButton);

    }

    private void HandlePressedButton(){
        // Create a object in Controller Class
        var controller = new MainGameController();
        // Call function press in MainGameController
        controller.handlePressedButton(index, sceneName);
    }



}
