using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogBox : MonoBehaviour
{
    public Text headerText;
    public Image headerImage;
    public Text messageText;
    public Transform buttonContainer;
    public GameObject CustomButton;

    private void Awake()
    {
        // Disable both headerText and headerImage by default
        if (headerText != null)
        {
            headerText.gameObject.SetActive(false);
        }
        if (headerImage != null)
        {
            headerImage.gameObject.SetActive(false);
        }
    }

    public void SetHeader(Sprite customHeaderImage = null, string customHeaderText = "", Font headerFont = null, int headerFontSize = 12)
    {
        if (headerImage != null && customHeaderImage != null)
        {
            // Display the custom header image
            headerImage.gameObject.SetActive(true);
            headerImage.sprite = customHeaderImage;
        }
        else if (headerText != null)
        {
            // Display custom header text with font and font size
            headerText.gameObject.SetActive(true);
            headerText.text = customHeaderText;
            headerText.font = headerFont;
            headerText.fontSize = headerFontSize;
        }
    }

    public void SetMessage(string message)
    {
        if (messageText != null)
        {
            // Set the message text
            messageText.text = message;
        }
    }

    public ButtonViewModel AddButton(GameObject customButton)
    {
        ButtonViewModel buttonView = null;

        if (buttonContainer != null && customButton != null)
        {
            // Create an instance of the custom button prefab
            GameObject buttonInstance = Instantiate(customButton);

            // Get the ButtonViewModel component of the custom button prefab
            buttonView = buttonInstance.GetComponent<ButtonViewModel>();

            // Set the button's parent to the buttonContainer
            buttonInstance.transform.SetParent(buttonContainer, false);
        }

        return buttonView;
    }




    public void CloseDialog()
    {
        // Close the dialog by deactivating its GameObject
        gameObject.SetActive(false);
        // You can add additional logic here, such as cleaning up resources or handling dialog closure animation.
    }
}
