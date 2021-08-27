using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDummy : MonoBehaviour
{
    [SerializeField] public InputField inputField;
    [SerializeField] public Text outputField;
    List<string> sl = new List<string>();

    void Awake()
    {
        sl.Add("All compiler errors have to be fixed before you can enter playmode!");
        sl.Add("NullReferenceException: object reference not set to an instance of an object");
        sl.Add("SyntaxError: invalid syntax");
        sl.Add("Error : Index was outside the bounds of the array.");
        sl.Add("FormatException: Input string was not in a correct format.");
        sl.Add("OverflowError: Python int too large to convert to C long");
        sl.Add("overflowerror cannot serialize a bytes object larger than 4 gib");
        sl.Add("syntaxerror cannot use import statement outside a module");
    }

    public void OnClickEnter()
    {
        if (inputField.text == "EXIT" || inputField.text == "exit")
        {
            SceneLoader.Instance.LoadScene("Tdong6");
        }
        else
        {
            outputField.text = sl[new System.Random().Next(0, sl.Count)];
        }

    }
}
