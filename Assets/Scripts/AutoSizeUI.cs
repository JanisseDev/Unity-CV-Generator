using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class AutoSizeUI : UIBehaviour, ILayoutSelfController
{
    #region Properties
    [SerializeField] private bool autoHeight = false;
    [SerializeField] private bool autoWidth = false;
    [SerializeField] private Vector2 margin = Vector2.zero;
    [SerializeField] private RectTransform[] verticalRtrsfs = null;
    [SerializeField] private RectTransform[] horizontalRtrsfs = null;
    #endregion

    #region AutoSizeUI Methods
    public void SetLayout()
    {
        SetLayoutHorizontal();
        SetLayoutVertical();
    }

    public void SetLayoutHorizontal()
    {
        if (autoWidth)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            float totalWidth = margin.x;
            foreach (RectTransform r in horizontalRtrsfs)
            {
                if(r.gameObject.activeSelf)
                {
                    totalWidth += r.rect.width;
                }
            }
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalWidth);
        }
    }

    public void SetLayoutVertical()
    {
        if(autoHeight)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            float totalHeight = margin.y;
            foreach (RectTransform r in verticalRtrsfs)
            {
                if (r.gameObject.activeSelf)
                {
                    totalHeight += r.rect.height;
                }
            }
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);
        }
    }

#if UNITY_EDITOR
    private void LateUpdate()
    {
        SetLayout();
    }
#endif
    #endregion
}
