using UnityEngine;
using System.Collections.Generic;

#region ____________________UICategory Class____________________

[System.Serializable]
public class UICategory
{
    public string name;
    public List<UIReference> references = new List<UIReference>();
}

#endregion ____________________UICategory Class____________________

public class UIManager : MonoBehaviour
{
    #region ____________________Serialized Fields____________________

    [SerializeField]
    private List<UICategory> uiCategories = new List<UICategory>();

    #endregion ____________________Serialized Fields____________________

    #region ____________________Singleton Pattern____________________

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("UIManager");
                    instance = obj.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    #endregion ____________________Singleton Pattern____________________

    #region ____________________UI Reference Management____________________

    // Add a UI reference to a category
    public void AddUIReference(string categoryName, string referenceName, GameObject uiElement)
    {
        UICategory category = uiCategories.Find(cat => cat.name == categoryName);
        if (category == null)
        {
            category = new UICategory { name = categoryName };
            uiCategories.Add(category);
        }

        UIReference reference = new UIReference { name = referenceName, uiElement = uiElement };
        category.references.Add(reference);
    }

    // Remove a UI reference by name and category
    public void RemoveUIReference(string categoryName, string referenceName)
    {
        UICategory category = uiCategories.Find(cat => cat.name == categoryName);
        if (category != null)
        {
            category.references.RemoveAll(reference => reference.name == referenceName);
        }
    }

    // Get a UI reference by name and category
    public GameObject GetUIReference(string categoryName, string referenceName)
    {
        UICategory category = uiCategories.Find(cat => cat.name == categoryName);
        if (category != null)
        {
            var reference = category.references.Find(uiRef => uiRef.name == referenceName);
            return reference != null ? reference.uiElement : null;
        }
        return null;
    }

    // Get all UI categories
    public List<UICategory> GetAllUICategories()
    {
        return uiCategories;
    }

    #endregion ____________________UI Reference Management____________________
}
