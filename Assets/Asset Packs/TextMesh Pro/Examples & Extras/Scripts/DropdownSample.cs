using TMPro;
using UnityEngine;

public class DropdownSample : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownWithoutPlaceholder;

    [SerializeField] private TMP_Dropdown dropdownWithPlaceholder;

    [SerializeField] private TextMeshProUGUI text;

    public void OnButtonClick()
    {
        text.text = dropdownWithPlaceholder.value > -1
            ? "Selected values:\n" + dropdownWithoutPlaceholder.value + " - " + dropdownWithPlaceholder.value
            : "Error: Please make a selection";
    }
}