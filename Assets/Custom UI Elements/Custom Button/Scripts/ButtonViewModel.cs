using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class ButtonCustomAction : UnityEvent { }

#region ____________________ENUMS____________________
public enum ButtonTask
{
    None,
    LoadScene,
    DisplayUIElement,
    HideUIElement,
    DisplayAndHideUIElements,
    OpenDialogBox,
    CloseDialogBox,
    UnpauseGame,
    QuitGame
}
#endregion ____________________ENUMS____________________

public class ButtonViewModel : MonoBehaviour
{
    #region ____________________Text and Icon____________________

    [Header("Text and Icon")]
    public string buttonText = "Button Text";
    public Sprite buttonIcon;

    #endregion ____________________Text and Icon____________________

    #region ____________________Interactivity____________________

    [Header("Interactivity")]
    public bool isInteractable = true;

    #endregion ____________________Interactivity____________________

    #region ___________________UI Elements To Display List____________________

    public List<GameObject> uiElementsToDisplay = new List<GameObject>();

    #endregion ___________________UI Elements To Display List____________________


    #region ____________________Hide UI Elements List____________________

    [Header("Hide UI Elements")]
    public List<GameObject> uiElementsToHide = new List<GameObject>();

    #endregion ____________________Hide UI Elements List____________________

    #region ____________________Dialog Box____________________

    [Header("Open Dialog Box")]
    public GameObject dialogBoxPrefab;

    #endregion

    #region ____________________Tooltip____________________

    // Tooltip fields
    public bool useCustomTooltip = false;
    public string customTooltip = "Custom Tooltip";

    [HideInInspector] public string tooltipText = "";

    #endregion ____________________Tooltip____________________

    #region ____________________Sounds____________________

    [Header("Sounds")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

    #endregion ____________________Sounds____________________

    #region ____________________Colors____________________

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public Color pressedColor = Color.black;

    #endregion ____________________Colors____________________

    #region ____________________Custom Actions____________________

    [Header("Custom Actions")]
    public ButtonCustomAction onButtonClick;

    #endregion ____________________Custom Actions____________________

    #region ____________________Button Task____________________

    [Header("Button Task")]
    public ButtonTask buttonTask;

    #endregion ____________________Button Task____________________

    #region ____________________Scene Selection____________________

    [Header("Scene Selection")]
    public List<string> sceneNames = new List<string>();
    public int selectedSceneIndex;

    #endregion ____________________Scene Selection____________________

    #region ____________________Button Text____________________

    // Add this field to the ButtonViewModel script
    [Header("Button Text")]
    public bool useCustomButtonText = false; // Toggle to use custom button text
    public string customButtonText = "Custom Button Text"; // Custom button text

    #endregion ____________________Button Text____________________

    #region ____________________Dialog Box Customization____________________

    [Header("Dialog Box Customization")]
    public bool useCustomHeader = false;
    public bool useCustomHeaderImage = false;
    public Sprite customHeaderImage;
    public string customHeaderText = "Custom Header Text";
    public Font headerFont;
    public int headerFontSize = 24;
    public string messageText = "Message Text";

    [Header("Button Prefab")]
    public GameObject CustomButton;

    public enum ButtonCount
    {
        NoButtons,
        OneButton,
        TwoButtons
    }
    public ButtonCount buttonCount = ButtonCount.NoButtons;


    #endregion ____________________Dialog Box Customization____________________



    #region ____________________Tooltip Methods____________________

    public void UpdateTooltipText()
    {
        tooltipText = useCustomTooltip ? customTooltip : GetDefaultTooltipText();
    }

    private string GetDefaultTooltipText()
    {
        switch (buttonTask)
        {
            case ButtonTask.None:
                return "No action selected.";
            case ButtonTask.LoadScene:
                return "Load a scene.";
            case ButtonTask.DisplayUIElement:
                return "Display a UI element.";
            case ButtonTask.HideUIElement:
                return "Hide a UI element.";
            case ButtonTask.DisplayAndHideUIElements:
                return "Will show a UI element and will hide a UI element.";
            case ButtonTask.OpenDialogBox:
                return "Open a dialog box.";
            case ButtonTask.CloseDialogBox:
                return "Close a dialog box.";
            case ButtonTask.UnpauseGame:
                return "Unpause the game.";
            case ButtonTask.QuitGame:
                return "Quit the game.";
            default:
                return "No action selected.";
        }
    }

    #endregion ____________________Tooltip Methods____________________

    #region ____________________Button Text Methods____________________

    public void UpdateButtonText()
    {
        buttonText = useCustomButtonText ? customButtonText : GetDefaultButtonText();
    }

    private string GetDefaultButtonText()
    {
        switch (buttonTask)
        {
            case ButtonTask.None:
                return "Button Text";
            case ButtonTask.LoadScene:
                return "Load Scene";
            case ButtonTask.DisplayUIElement:
                return "Display UI Element";
            case ButtonTask.HideUIElement:
                return "Hide UI Element";
            case ButtonTask.DisplayAndHideUIElements:
                return "Show and Hide UI Elements";
            case ButtonTask.OpenDialogBox:
                return "Open Dialog Box";
            case ButtonTask.CloseDialogBox:
                return "Close Dialog Box";
            case ButtonTask.UnpauseGame:
                return "Unpause Game";
            case ButtonTask.QuitGame:
                return "Quit Game";
            default:
                return "Button Text";
        }
    }

    #endregion ____________________Button Text Methods____________________

    #region ____________________Validation and Execution____________________

    private void OnValidate()
    {
        UpdateTooltipText();
        UpdateButtonText();
    }

    public void PerformButtonTask()
    {
        switch (buttonTask)
        {


            case ButtonTask.LoadScene:
                // Check if a scene is selected
                if (selectedSceneIndex >= 0 && selectedSceneIndex < sceneNames.Count)
                {
                    // Load the selected scene
                    SceneManager.LoadScene(sceneNames[selectedSceneIndex]);
                }
                break;


            case ButtonTask.DisplayUIElement:
                // Activate the specified UI elements
                foreach (var uiElement in uiElementsToDisplay)
                {
                    uiElement.SetActive(true);
                }
                break;


            case ButtonTask.HideUIElement:
                // Deactivate the specified UI elements
                foreach (var uiElement in uiElementsToDisplay)
                {
                    uiElement.SetActive(false);
                }
                break;


            case ButtonTask.DisplayAndHideUIElements:
                // Show UI elements
                foreach (var uiElement in uiElementsToDisplay)
                {
                    uiElement.SetActive(true);
                }

                // Hide UI elements
                foreach (var uiElement in uiElementsToHide)
                {
                    uiElement.SetActive(false);
                }
                break;


            case ButtonTask.OpenDialogBox:
                // Check if the dialog box prefab is set
                if (dialogBoxPrefab != null)
                {
                    // Create an instance of the dialog box prefab
                    GameObject dialogInstance = Instantiate(dialogBoxPrefab);

                    // Get the DialogBox component from the instance
                    DialogBox dialogBox = dialogInstance.GetComponent<DialogBox>();

                    // Customize the dialog box based on user choices
                    if (useCustomHeader)
                    {
                        if (useCustomHeaderImage == false)
                        {
                           
                            Debug.Log("Setting Header: " + customHeaderText); 
                            // Use custom header text
                            dialogBox.SetHeader(null, customHeaderText, headerFont, headerFontSize);

                        }
                        else if(useCustomHeaderImage)
                        {
                            Debug.Log("Setting Header image." );
                            // Use custom header image
                            dialogBox.SetHeader(customHeaderImage, customHeaderText, headerFont, headerFontSize);
                        }
                    }

                    Debug.Log("Setting Message" + messageText);
                    // Set the message text
                    dialogBox.SetMessage(messageText);

                    // Add buttons based on the ButtonCount enum
                    switch (buttonCount)
                    {
                        case ButtonCount.OneButton:
                            Debug.Log("One Button");
                            dialogBox.AddButton(CustomButton);
                            break;

                        case ButtonCount.TwoButtons:
                            // Add two buttons with different actions
                            Debug.Log("Two Buttons.");
                            dialogBox.AddButton(CustomButton);
                            dialogBox.AddButton(CustomButton);
                            break;

                        default:
                            // No buttons
                            Debug.Log("No Button.");
                            break;
                    }
                }
                else
                {
                    Debug.Log("No Prefab Set for Dialog Box.");
                }
                break;




            case ButtonTask.CloseDialogBox:
                // Check if there's an open dialog box
                DialogBox openDialog = FindObjectOfType<DialogBox>();
                if (openDialog != null)
                {
                    // Close the dialog box
                    openDialog.CloseDialog();
                }
                break;


            case ButtonTask.UnpauseGame:
                // Implement unpausing game logic here
                Time.timeScale = 1f; // Set the time scale to 1 to unpause the game
                break;


            case ButtonTask.QuitGame:
                // Quit the game when the button is clicked
                Application.Quit();
                break;


            default:
                break;
        }

        // Invoke custom actions
        onButtonClick.Invoke();
    }

    #endregion ____________________Validation and Execution____________________
}
