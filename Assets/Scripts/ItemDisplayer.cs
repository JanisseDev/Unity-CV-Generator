using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class ItemDisplayer : MonoBehaviour
{
    #region Properties
    [SerializeField] private TMP_Text leftText = null;
    [SerializeField] private TMP_Text titleText = null;
    [SerializeField] private TMP_Text descText = null;

    private float descOffset = 0f;
    #endregion

    #region ItemDisplayer Methods
    internal void SetData(CVData.ItemList.Item a_data)
    {
        if (descOffset == 0f) { descOffset = descText.rectTransform.offsetMax.y; }

        int caseId = (string.IsNullOrEmpty(a_data.primaryTitle) ? 0 : 1) + (string.IsNullOrEmpty(a_data.secondaryTitle) ? 0 : 2);
        switch(caseId)
        {
            case 0: //None
                titleText.text = "";
                break;
            case 1: //Only primaryTitle
                titleText.text = a_data.primaryTitle;
                break;
            case 2: //Only secondaryTitle
                titleText.text = string.Format("<i><size=20>{0}</size></i>", a_data.secondaryTitle);
                break;
            case 3: //All
                titleText.text = string.Format("{0} - <i><size=20>{1}</size></i>", a_data.primaryTitle, a_data.secondaryTitle);
                break;
        }
        
        leftText.text = string.Format("{0}\n<size=13>{1}</size>", a_data.leftPrimaryText, a_data.leftSecondaryText);
        descText.text = a_data.desc;

        if (titleText.text == "")
        {
            titleText.gameObject.SetActive(false);
            descText.rectTransform.offsetMax = new Vector2(descText.rectTransform.offsetMax.x, 0f);
        }
        else
        {
            titleText.gameObject.SetActive(true);
            descText.rectTransform.offsetMax = new Vector2(descText.rectTransform.offsetMax.x, descOffset);
        }
    }
    #endregion
}
