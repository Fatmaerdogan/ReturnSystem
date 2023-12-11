using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicAxis : MonoBehaviour
{
    public Text Label;
    public Text TimeLabel;
    public Text SectionLabel;
    public Transform PlotterCurvePoint;

    public Transform PositionPoint;
    public Transform PositionPointVertical;
    public Transform RotationPoint;
    public Transform ScalePoint;

    public virtual void SetLabel(string newLabel)
    {
        Label.text = newLabel;
    }
    public virtual void SetSectionLabel(string newSectionLabel)
    {
        SectionLabel.text = newSectionLabel;
    }
}
