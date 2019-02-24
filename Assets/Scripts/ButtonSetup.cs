using UnityEngine;
using UnityEngine.UI;
public class ButtonSetup : MonoBehaviour {
    public int index;
    public string sceneName;

    private void Start()
    {
        //Set Event for onclick 
        gameObject.GetComponent<Button>().onClick.AddListener(HandlePressedButton);

    }

    public void HandlePressedButton(){
        // Call function press in MainGameController
        GameObject.FindGameObjectWithTag("MainControllerPlayer").GetComponent<MainGameController>().handlePressedButton(index, sceneName);
    }



}
