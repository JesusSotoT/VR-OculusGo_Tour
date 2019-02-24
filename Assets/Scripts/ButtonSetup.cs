using UnityEngine;
using UnityEngine.UI;
public class ButtonSetup : MonoBehaviour {
    public int index;
    public string sceneName;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandlePressedButton);

    }

    public void HandlePressedButton(){
        // Create a object in Controller Class
        //var controller = new MainGameController();
        // Call function press in MainGameController
        GameObject.FindGameObjectWithTag("MainControllerPlayer").GetComponent<MainGameController>().handlePressedButton(index, sceneName);
    }



}
