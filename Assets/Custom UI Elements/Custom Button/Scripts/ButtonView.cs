using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    #region ____________________Fields____________________

    public Text buttonText;
    public Image background;
    public float animationDuration = 0.2f;
    public ButtonViewModel viewModel;
    public Text tooltipText;
    public GameObject tooltipPanel;

    private bool isHovered = false;
    private bool isPressed = false;

    private AudioSource audioSource;

    #endregion ____________________Fields____________________

    #region ____________________Initialization____________________

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeButton();
    }

    private void InitializeButton()
    {
        UpdateButtonState();
        UpdateInteractivity();

        // Bind the button text to the ViewModel's buttonText property
        buttonText.text = viewModel.buttonText;

        // Set the tooltip text
        tooltipText.text = viewModel.tooltipText;
        tooltipPanel.SetActive(false);
    }

    #endregion ____________________Initialization____________________

    #region ____________________EventHandlers____________________

    public void OnButtonClick()
    {
        if (viewModel.isInteractable)
        {
            // Play a click sound if available
            if (viewModel.clickSound != null)
            {
                audioSource.PlayOneShot(viewModel.clickSound);
            }

            // Call the PerformButtonTask method to execute the selected task
            viewModel.PerformButtonTask();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        UpdateButtonState();

        // Play a hover sound if available
        if (viewModel.hoverSound != null)
        {
            audioSource.PlayOneShot(viewModel.hoverSound);
        }

        // Show tooltip
        if (!string.IsNullOrEmpty(viewModel.tooltipText))
        {
            tooltipPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        isPressed = false;
        UpdateButtonState();

        // Hide tooltip
        tooltipPanel.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (viewModel.isInteractable)
        {
            isPressed = true;
            UpdateButtonState();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (viewModel.isInteractable)
        {
            isPressed = false;
            UpdateButtonState();
        }
    }

    #endregion ____________________EventHandlers____________________

    #region ____________________HelperMethods____________________

    public void SetInteractable(bool isInteractable)
    {
        viewModel.isInteractable = isInteractable;
        UpdateInteractivity();
    }

    private void UpdateButtonState()
    {
        if (isPressed)
        {
            AnimateColor(viewModel.pressedColor);
        }
        else if (isHovered)
        {
            AnimateColor(viewModel.hoverColor);
        }
        else
        {
            AnimateColor(viewModel.normalColor);
        }
    }

    private void AnimateColor(Color targetColor)
    {
        //  background.DOColor(targetColor, animationDuration).SetEase(animationEase);
    }

    private void UpdateInteractivity()
    {
        GetComponent<Button>().interactable = viewModel.isInteractable;
    }

    #endregion ____________________HelperMethods____________________
}
