using UnityEngine;
public class Tool : MonoBehaviour 
{
    public static Tool CurrentlySelectedTool
    {
        get => _currentlySelectedTool;
        set
        {
            if (_currentlySelectedTool == value)
                return;
            if (_currentlySelectedTool is not null)
                _currentlySelectedTool.IsSelected = false;
            if (value is not null)
                value.IsSelected = true;
        }
    } static Tool _currentlySelectedTool;

    bool IsSelected
    {
        get => _isSelected;
        set
        {
            // Make ourselves visible
            gameObject.SetActive(value);
            _isSelected = value;
        }
    } bool _isSelected;
    
    public Sprite UiIcon;

    public Transform interactPoint;

    void Update()
    {
        if (!IsSelected) 
            return;

        var screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,0.1f));
        foreach (var hit in Physics.RaycastAll(screenToWorldPoint, (screenToWorldPoint - Camera.main.transform.position).normalized))
        {
            if (hit.transform == transform)
                continue;
                
            transform.position = hit.point + (transform.position - interactPoint.position);
            if (Input.GetMouseButtonDown(0))
                Use();
            
            break;
        }
    }

    /// <summary> Inheritors do usage effects </summary>
    protected virtual void Use()
    {
        if (FindObjectsByType<Part>(FindObjectsSortMode.None).GetLowest(_ => Vector3.Distance(_.transform.position, interactPoint.position)) is
            {
                Item2: < 0.3f
            } found)
        {
            Affect(found.Item1);
        }
    }
    
    protected virtual void Affect(Part target) { }
}