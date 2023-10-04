using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ItemListDisplayer : MonoBehaviour
{
    #region Properties
    [SerializeField] private TMP_Text titleText = null;
    [SerializeField] private RectTransform titleBackgroundRtrsf = null;
    [SerializeField] private VerticalLayoutGroup layoutGroup = null;
    [SerializeField] private ItemDisplayer itemDisplayerPrefab = null;

    private Pool<ItemDisplayer> itemDisplayerPool = null;

    #endregion

    #region ItemListDisplayer Methods
    internal void SetData(CVData.ItemList a_data)
    {
        //Init
        itemDisplayerPrefab.gameObject.SetActive(false);
        if (itemDisplayerPool == null) { itemDisplayerPool = new Pool<ItemDisplayer>(itemDisplayerPrefab, itemDisplayerPrefab.transform.parent); };

        //Set Data
        titleText.text = a_data.title;
        titleBackgroundRtrsf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, titleText.GetPreferredValues().x + 40f);

        itemDisplayerPool.SetPoolSize(a_data.items.Count, false);
        ItemDisplayer itemDisplayer = null;
        for (int i = 0; i < a_data.items.Count; ++i)
        {
            itemDisplayer = itemDisplayerPool[i];
            itemDisplayer.SetData(a_data.items[i]);
            itemDisplayer.gameObject.SetActive(true);
        }

        layoutGroup.spacing = a_data.spacing;
    }
    #endregion
}
