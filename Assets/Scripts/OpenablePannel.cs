﻿using System.Collections;
using UnityEngine;

public class OpenablePannel : MonoBehaviour
{
    public GameObject pannel;
    public GameObject Screw1, Screw2, Screw3, Screw4;
    public bool open;

    public Vector3 screwTranslate;
    public Vector3 pannelTranslate;

    private Vector3 pannelPos, s1pos, s2pos, s3pos, s4pos;

    public float interpolationFactor;

    // Use this for initialization
    private void Start()
    {
        pannelPos = pannel.transform.position;
        s1pos = Screw1.transform.position;
        s2pos = Screw2.transform.position;
        s3pos = Screw3.transform.position;
        s4pos = Screw4.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (open)
        {
            pannel.transform.position = Vector3.Lerp(pannel.transform.position, pannelPos + pannelTranslate, interpolationFactor);
            Screw1.transform.position = Vector3.Lerp(Screw1.transform.position, s1pos + screwTranslate, interpolationFactor);
            Screw2.transform.position = Vector3.Lerp(Screw2.transform.position, s2pos + screwTranslate, interpolationFactor);
            Screw3.transform.position = Vector3.Lerp(Screw3.transform.position, s3pos + screwTranslate, interpolationFactor);
            Screw4.transform.position = Vector3.Lerp(Screw4.transform.position, s4pos + screwTranslate, interpolationFactor);
        }
        else
        {
            pannel.transform.position = Vector3.Lerp(pannel.transform.position, pannelPos, interpolationFactor);
            Screw1.transform.position = Vector3.Lerp(Screw1.transform.position, s1pos, interpolationFactor);
            Screw2.transform.position = Vector3.Lerp(Screw2.transform.position, s2pos, interpolationFactor);
            Screw3.transform.position = Vector3.Lerp(Screw3.transform.position, s3pos, interpolationFactor);
            Screw4.transform.position = Vector3.Lerp(Screw4.transform.position, s4pos, interpolationFactor);
        }
    }
}