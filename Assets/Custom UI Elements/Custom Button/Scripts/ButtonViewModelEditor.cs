using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditorInternal;
using Unity.VisualScripting;

[CustomEditor(typeof(ButtonViewModel))]
public class ButtonViewModelEditor : Editor
{
    private SerializedProperty uiElementsToDisplay;
    private ReorderableList uiElementsList;

    public override void OnInspectorGUI()
    {
        ButtonViewModel buttonViewModel = (ButtonViewModel)target;

        serializedObject.Update();
        uiElementsToDisplay = serializedObject.FindProperty("uiElementsToDisplay");

        // Display properties in a user-friendly way with EditorGUI
        buttonViewModel.buttonText = EditorGUILayout.TextField("Button Text", buttonViewModel.buttonText);

        // Button Icon Preview
        EditorGUILayout.LabelField("Button Icon", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        buttonViewModel.buttonIcon = (Sprite)EditorGUILayout.ObjectField(buttonViewModel.buttonIcon, typeof(Sprite), false);
        if (buttonViewModel.buttonIcon != null)
        {
            Rect iconRect = EditorGUILayout.GetControlRect(GUILayout.Width(64), GUILayout.Height(64));
            EditorGUI.DrawPreviewTexture(iconRect, buttonViewModel.buttonIcon.texture);
        }
        EditorGUI.indentLevel--;

        buttonViewModel.isInteractable = EditorGUILayout.Toggle("Is Interactable", buttonViewModel.isInteractable);

        // Custom Tooltip
        buttonViewModel.useCustomTooltip = EditorGUILayout.Toggle("Use Custom Tooltip", buttonViewModel.useCustomTooltip);
        if (buttonViewModel.useCustomTooltip)
        {
            buttonViewModel.customTooltip = EditorGUILayout.TextField("Custom Tooltip", buttonViewModel.customTooltip);
        }
        else
        {
            buttonViewModel.UpdateTooltipText();
        }
        EditorGUILayout.LabelField("Tooltip Text", buttonViewModel.tooltipText);

        EditorGUILayout.Space();

        // Custom Button Text
        buttonViewModel.useCustomButtonText = EditorGUILayout.Toggle("Use Custom Button Text", buttonViewModel.useCustomButtonText);
        if (buttonViewModel.useCustomButtonText)
        {
            buttonViewModel.customButtonText = EditorGUILayout.TextField("Custom Button Text", buttonViewModel.customButtonText);
        }
        else
        {
            buttonViewModel.UpdateButtonText();
        }
        EditorGUILayout.LabelField("Button Text", buttonViewModel.buttonText);

        // Sounds
        EditorGUILayout.LabelField("Sounds", EditorStyles.boldLabel);
        buttonViewModel.hoverSound = (AudioClip)EditorGUILayout.ObjectField("Hover Sound", buttonViewModel.hoverSound, typeof(AudioClip), false);
        buttonViewModel.clickSound = (AudioClip)EditorGUILayout.ObjectField("Click Sound", buttonViewModel.clickSound, typeof(AudioClip), false);

        EditorGUILayout.Space();

        // Colors
        EditorGUILayout.LabelField("Colors", EditorStyles.boldLabel);
        buttonViewModel.normalColor = EditorGUILayout.ColorField("Normal Color", buttonViewModel.normalColor);
        buttonViewModel.hoverColor = EditorGUILayout.ColorField("Hover Color", buttonViewModel.hoverColor);
        buttonViewModel.pressedColor = EditorGUILayout.ColorField("Pressed Color", buttonViewModel.pressedColor);

        EditorGUILayout.Space();

        // Custom Actions
        EditorGUILayout.LabelField("Custom Actions", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onButtonClick"));

        EditorGUILayout.Space();

        // Button Task
        EditorGUILayout.LabelField("Button Task", EditorStyles.boldLabel);
        buttonViewModel.buttonTask = (ButtonTask)EditorGUILayout.EnumPopup("Button Task", buttonViewModel.buttonTask);



        if (buttonViewModel.buttonTask == ButtonTask.DisplayUIElement)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Display UI Elements", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            // Add a help box with a detailed tooltip
            EditorGUILayout.HelpBox("Configure the UI elements to display when the button is clicked. You can also add or remove elements dynamically.", MessageType.Info);

            // Display UI elements list with a foldout
            SerializedProperty uiElementsArray = serializedObject.FindProperty("uiElementsToDisplay");
            uiElementsArray.isExpanded = EditorGUILayout.Foldout(uiElementsArray.isExpanded, "UI Elements To Display");
            if (uiElementsArray.isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(uiElementsArray, true);
                EditorGUI.indentLevel--;
            }

            // Add a horizontal line for separation
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // Buttons for adding and clearing UI elements
            EditorGUILayout.BeginHorizontal();

            // Add a button to add UI elements dynamically with a tooltip
            GUIContent addButtonContent = new GUIContent("Add UI Element", "Click to add a new UI element");
            if (GUILayout.Button(addButtonContent, GUILayout.ExpandWidth(false)))
            {
                uiElementsArray.InsertArrayElementAtIndex(uiElementsArray.arraySize);
            }

            // Add a button to clear the UI elements list with a tooltip
            GUIContent clearButtonContent = new GUIContent("Clear List", "Click to remove all UI elements");
            if (GUILayout.Button(clearButtonContent, GUILayout.ExpandWidth(false)))
            {
                uiElementsArray.ClearArray();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
        }


        if (buttonViewModel.buttonTask == ButtonTask.HideUIElement)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hide UI Elements", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            // Add a help box with a detailed tooltip
            EditorGUILayout.HelpBox("Configure the UI elements to hide when the button is clicked. You can also add or remove elements dynamically.", MessageType.Info);

            // Display UI elements list with a foldout
            SerializedProperty uiElementsArray = serializedObject.FindProperty("uiElementsToHide");
            uiElementsArray.isExpanded = EditorGUILayout.Foldout(uiElementsArray.isExpanded, "UI Elements To Hide");
            if (uiElementsArray.isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(uiElementsArray, true);
                EditorGUI.indentLevel--;
            }

            // Add a horizontal line for separation
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // Buttons for adding and clearing UI elements
            EditorGUILayout.BeginHorizontal();

            // Add a button to add UI elements dynamically with a tooltip
            GUIContent addButtonContent = new GUIContent("Add UI Element", "Click to add a new UI element");
            if (GUILayout.Button(addButtonContent, GUILayout.ExpandWidth(false)))
            {
                uiElementsArray.InsertArrayElementAtIndex(uiElementsArray.arraySize);
            }

            // Add a button to clear the UI elements list with a tooltip
            GUIContent clearButtonContent = new GUIContent("Clear List", "Click to remove all UI elements");
            if (GUILayout.Button(clearButtonContent, GUILayout.ExpandWidth(false)))
            {
                uiElementsArray.ClearArray();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
        }

        if (buttonViewModel.buttonTask == ButtonTask.DisplayAndHideUIElements)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Display and Hide UI Elements", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            // Add a help box with a detailed tooltip
            EditorGUILayout.HelpBox("Configure the UI elements to display and hide when the button is clicked. You can also add or remove elements dynamically.", MessageType.Info);

            // Display UI elements to show list with a foldout
            SerializedProperty showElementsArray = serializedObject.FindProperty("uiElementsToDisplay");
            showElementsArray.isExpanded = EditorGUILayout.Foldout(showElementsArray.isExpanded, "UI Elements To Show");
            if (showElementsArray.isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(showElementsArray, true);
                EditorGUI.indentLevel--;
            }

            // Display UI elements to hide list with a foldout
            SerializedProperty hideElementsArray = serializedObject.FindProperty("uiElementsToHide");
            hideElementsArray.isExpanded = EditorGUILayout.Foldout(hideElementsArray.isExpanded, "UI Elements To Hide");
            if (hideElementsArray.isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(hideElementsArray, true);
                EditorGUI.indentLevel--;
            }

            // Add a horizontal line for separation
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // Buttons for adding and clearing UI elements to show
            EditorGUILayout.BeginHorizontal();
            GUIContent addShowButtonContent = new GUIContent("Add UI Element to Show", "Click to add a new UI element to show");
            if (GUILayout.Button(addShowButtonContent, GUILayout.ExpandWidth(false)))
            {
                showElementsArray.InsertArrayElementAtIndex(showElementsArray.arraySize);
            }
            GUIContent clearShowButtonContent = new GUIContent("Clear Show List", "Click to remove all UI elements to show");
            if (GUILayout.Button(clearShowButtonContent, GUILayout.ExpandWidth(false)))
            {
                showElementsArray.ClearArray();
            }
            EditorGUILayout.EndHorizontal();

            // Buttons for adding and clearing UI elements to hide
            EditorGUILayout.BeginHorizontal();
            GUIContent addHideButtonContent = new GUIContent("Add UI Element to Hide", "Click to add a new UI element to hide");
            if (GUILayout.Button(addHideButtonContent, GUILayout.ExpandWidth(false)))
            {
                hideElementsArray.InsertArrayElementAtIndex(hideElementsArray.arraySize);
            }
            GUIContent clearHideButtonContent = new GUIContent("Clear Hide List", "Click to remove all UI elements to hide");
            if (GUILayout.Button(clearHideButtonContent, GUILayout.ExpandWidth(false)))
            {
                hideElementsArray.ClearArray();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
        }



        if (buttonViewModel.buttonTask == ButtonTask.OpenDialogBox)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Dialog Box Customization", EditorStyles.boldLabel);

            buttonViewModel.dialogBoxPrefab = (GameObject)EditorGUILayout.ObjectField("Dialog Box Prefab", buttonViewModel.dialogBoxPrefab, typeof(GameObject), false);

            buttonViewModel.useCustomHeader = EditorGUILayout.Toggle("Use Custom Header", buttonViewModel.useCustomHeader);

            if (buttonViewModel.useCustomHeader)
            {
                EditorGUI.indentLevel++;

                buttonViewModel.useCustomHeaderImage = EditorGUILayout.Toggle("Use Custom Header Image", buttonViewModel.useCustomHeaderImage);

                if (buttonViewModel.useCustomHeaderImage)
                {
                    buttonViewModel.customHeaderImage = (Sprite)EditorGUILayout.ObjectField("Custom Header Image", buttonViewModel.customHeaderImage, typeof(Sprite), true);
                }
                else
                {
                    buttonViewModel.customHeaderText = EditorGUILayout.TextField("Custom Header Text", buttonViewModel.customHeaderText);
                    buttonViewModel.headerFont = (Font)EditorGUILayout.ObjectField("Header Font", buttonViewModel.headerFont, typeof(Font), false);
                    buttonViewModel.headerFontSize = EditorGUILayout.IntField("Header Font Size", buttonViewModel.headerFontSize);
                }

                EditorGUI.indentLevel--;
            }

            buttonViewModel.messageText = EditorGUILayout.TextField("Message Text", buttonViewModel.messageText);

            buttonViewModel.buttonCount = (ButtonViewModel.ButtonCount)EditorGUILayout.EnumPopup("Button Count", buttonViewModel.buttonCount);

            if (buttonViewModel.buttonCount == ButtonViewModel.ButtonCount.OneButton)
            {

                buttonViewModel.CustomButton = (GameObject)EditorGUILayout.ObjectField("Custom Button Prefab", buttonViewModel.CustomButton, typeof(GameObject), false);
            }
            else if (buttonViewModel.buttonCount == ButtonViewModel.ButtonCount.TwoButtons)
            {
                // Customization for two buttons
                // Add fields to set button text and actions for both buttons here
                buttonViewModel.CustomButton = (GameObject)EditorGUILayout.ObjectField("Custom Button Prefab", buttonViewModel.CustomButton, typeof(GameObject), false);

            }
        }





        // Scene Picker
        if (buttonViewModel.buttonTask == ButtonTask.LoadScene)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Scene Selection", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Select Scene");

            // Get the list of scene names from the build settings
            string[] scenePaths = GetAllScenePaths();
            List<string> sceneNames = new List<string>();
            foreach (string scenePath in scenePaths)
            {
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                sceneNames.Add(sceneName);
            }

            int selectedSceneIndex = EditorGUILayout.Popup(buttonViewModel.selectedSceneIndex, sceneNames.ToArray());
            if (selectedSceneIndex != buttonViewModel.selectedSceneIndex)
            {
                buttonViewModel.selectedSceneIndex = selectedSceneIndex;
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUILayout.EndHorizontal();
        }
        else
        {
            buttonViewModel.sceneNames.Clear(); // Clear the list when the task is not "Load Scene"
        }

        serializedObject.ApplyModifiedProperties();
    }

    // Helper method to get all scene paths in the build settings
    private string[] GetAllScenePaths()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] scenePaths = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }
        return scenePaths;
    }
}
