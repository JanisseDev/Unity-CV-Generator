using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class CVDisplayer : MonoBehaviour
{
    #region Properties
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text jobTitleText = null;
    [SerializeField] private TMP_Text infoText = null;
    [SerializeField] private VerticalLayoutGroup layoutGroup = null;
    [SerializeField] private ItemListDisplayer itemListDisplayerPrefab = null;

    private Pool<ItemListDisplayer> itemListDisplayerPool = null;
    #endregion

    #region CVDisplayer Methods
    internal void SetData(CVData a_data)
    {
        //Init
        itemListDisplayerPrefab.gameObject.SetActive(false);
        if(itemListDisplayerPool == null) { itemListDisplayerPool = new Pool<ItemListDisplayer>(itemListDisplayerPrefab, itemListDisplayerPrefab.transform.parent); };

        //Set Data
        nameText.text = a_data.name;
        jobTitleText.text = a_data.jobTitle;
        infoText.text = a_data.info;

        itemListDisplayerPool.SetPoolSize(a_data.itemLists.Count, false);
        ItemListDisplayer itemListDisplayer = null;
        for (int i=0; i<a_data.itemLists.Count; ++i)
        {
            itemListDisplayer = itemListDisplayerPool[i];
            itemListDisplayer.SetData(a_data.itemLists[i]);
            itemListDisplayer.gameObject.SetActive(true);
        }

        layoutGroup.spacing = a_data.spacing;
    }
    #endregion
}
