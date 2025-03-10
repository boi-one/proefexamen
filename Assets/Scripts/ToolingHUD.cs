using UnityEngine;
using UnityEngine.UI;

public class ToolingHUD : MonoBehaviour
{
    [SerializeField] Transform slotPrefab;
    
    void Awake()
    {
        foreach (Tool tool in FindObjectsByType<Tool>(FindObjectsSortMode.None))
        {
            tool.gameObject.SetActive(false);
            var created = Object.Instantiate(slotPrefab.gameObject, slotPrefab.parent);
            created.SetActive(true);
            created.transform.GetChild(0).GetComponent<Image>().sprite = tool.UiIcon;
            created.GetComponent<Button>().onClick.AddListener(() => { Tool.CurrentlySelectedTool = tool; });
        }
    }
}
