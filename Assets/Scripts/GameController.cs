using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public InputField inputField;
    private string savedName;

    public void SubmitName()
    {
        if (inputField.GetComponentInChildren<Text>().text != string.Empty)
        {
            string playerName = inputField.GetComponentInChildren<Text>().text;
            savedName = playerName;
            if(!CheckForExistence())
            {
                Debug.Log("This name is valid!");
            }
        }
        else
            Debug.Log("InputField is empty");
      
    }

    public bool CheckForExistence()
    {
        if (ScoreController.scoreController.VerifyName(savedName)) {
            Debug.Log("Name already exists!");
            return true;
        }
        else
            return false;
        
    }
}
