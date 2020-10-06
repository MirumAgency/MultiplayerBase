using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons2DGroup : MonoBehaviour {

    [SerializeField]
    private bool allowOnlyOne = true;
    [SerializeField]
    private List<VRButton2d> vRButton2Ds;

    private void Start()
    {
        foreach (Transform child in transform)
            if (!vRButton2Ds.Exists(x => x == child.transform.GetComponentInChildren<VRButton2d>()))
                vRButton2Ds.Add(child.transform.GetComponentInChildren<VRButton2d>());
        foreach (var bton in vRButton2Ds)
            bton.Btonsgroup = this;
    }

    public void ResetPositions(VRButton2d actual)
    {
        if (allowOnlyOne)
            foreach (var bton in vRButton2Ds)
            {
                if (bton != actual)
                {
                    bton.ResetPositionButton();
                }
            }

    }
}
