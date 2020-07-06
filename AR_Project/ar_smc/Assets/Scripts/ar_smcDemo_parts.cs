/****************************************************************************

MIT License

Copyright(c) 2020 Roman Parak

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*****************************************************************************

Author   : Roman Parak
Email    : Roman.Parak @outlook.com
Github   : https://github.com/rparak
File Name: smcDemo_parts.cs

****************************************************************************/

// ------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------- LIBRARIES -------------------------------------------------------- //
// ------------------------------------------------------------------------------------------------------------------------ //

// -------------------- System -------------------- //
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Globalization;
// -------------------- Unity -------------------- //
using UnityEngine;
using UnityEngine.UI;
// --------------------- TM ---------------------- //
using TMPro;

public class ar_smcDemo_parts : MonoBehaviour
{
    // -------------------- Button -------------------- //
    public Button reset_CP, info_CP, end_IP, webSite_CP, document_w3d, end_DP;
    public Button btn_startS_CP, btn_stopS_CP;
    // -------------------- Slider -------------------- //
    public Slider sld_scale_CP, sld_posX_CP, sld_posY_CP, sld_posZ_CP, sld_rotX_CP, sld_rotY_CP, sld_rotZ_CP;
    // -------------------- TextMeshProUGUI -------------------- //
    public TextMeshProUGUI infoPanel_txt;
    // -------------------- Image -------------------- //
    public Image information_panelI, document_panelD;
    // -------------------- Animator -------------------- //
    public Animator part_anim;
    // -------------------- RectTransform -------------------- //
    public RectTransform panel_rect;
    // -------------------- String -------------------- //
    public string animS_name;
    public string URL;
    public string obj_name;
    // -------------------- Float -------------------- //
    private float scale_CP, posX_CP, posY_CP, posZ_CP, rotX_CP, rotY_CP, rotZ_CP;
    public float[] obj_pos = new float[3];
    private float ex_param = 100f;
    // -------------------- Bool -------------------- //
    private bool reset_cpB, info_cpB, doc_w3d;
    private bool startSim_CP, stopSim_CP;

    // ------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------ INITIALIZATION {START} ------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------ //
    void Start()
    {
        // ------------------------ Initialization Object ------------------------//
        // GameObject {Main}
        GameObject.Find(obj_name).transform.localPosition = new Vector3(obj_pos[0], obj_pos[1], obj_pos[2]);
        GameObject.Find(obj_name).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        GameObject.Find(obj_name).transform.localScale = new Vector3(1f, 1f, 1f);
        // Information Window {Main Panel}
        panel_rect.localPosition = new Vector3(3f, 28f, 0f);
        // Information panel
        information_panelI.transform.localPosition = new Vector3(1515f, 0f, 0f);
        // Document panel
        document_panelD.transform.localPosition = new Vector3(1515f, 550f, 0f);
        // ----- Slider ----- //
        // Scale
        sld_scale_CP.value = 100f;
        // Position X, Y, Z
        sld_posX_CP.value = sld_posY_CP.value = sld_posZ_CP.value = 0f;
        // Rotation X, Y, Z
        sld_rotX_CP.value = sld_rotY_CP.value = sld_rotZ_CP.value = 0f;
        // Animation -> OFF State
        part_anim.Play("empty_state");
    }

    // ------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------ MAIN FUNCTION {Cyclic} ------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------ //
    void Update()
    {
        // ------------------------ Button ------------------------//
        // Info btn. (move panel -> main window)
        info_CP.onClick.AddListener(TaskOnClick_InfoBTN);
        // Exit info panel
        end_IP.onClick.AddListener(TaskOnClick_EndBTN);
        // Document btn. (move panel -> main window)
        document_w3d.onClick.AddListener(TaskOnClick_DocBTN);
        // Exit document panel
        end_DP.onClick.AddListener(TaskOnClick_EndBTNDP);

        // ------------------------ RESET ALL 3D View ------------------------//
        // Reset all (go to initialization position of each panels, rect, etc.)
        reset_CP.onClick.AddListener(TaskOnClick_ResetBTN);
        // Reset Condition
        if (reset_cpB == true){
            // GameObject {Main}
            GameObject.Find(obj_name).transform.localPosition = new Vector3(obj_pos[0], obj_pos[1], obj_pos[2]);
            GameObject.Find(obj_name).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            GameObject.Find(obj_name).transform.localScale = new Vector3(1f, 1f, 1f);
            // Information panel
            information_panelI.transform.localPosition = new Vector3(1515f, 0f, 0f);
            // Document panel
            document_panelD.transform.localPosition = new Vector3(1515f, 550f, 0f);
            // ----- Slider ----- //
            // Scale
            sld_scale_CP.value = 100f;
            // Position X, Y, Z
            sld_posX_CP.value = sld_posY_CP.value = sld_posZ_CP.value = 0f;
            // Rotation X, Y, Z
            sld_rotX_CP.value = sld_rotY_CP.value = sld_rotZ_CP.value = 0f;
            // Animation -> OFF State
            part_anim.Play("empty_state");

            // reset -> false (edge control)
            reset_cpB = false;
        }
        else
        {
            // ---------- Slider Control ---------- //
            // Scale
            scale_CP = 0f + sld_scale_CP.value / 100;
            // Position X, Y, Z
            posX_CP = obj_pos[0] + sld_posX_CP.value / 100;
            posY_CP = obj_pos[1] + sld_posY_CP.value / 100 + scale_CP / 2;
            posZ_CP = obj_pos[2] + sld_posZ_CP.value / 100;
            // Rotation X, Y, Z
            rotX_CP = 0f + sld_rotX_CP.value;
            rotY_CP = 0f + sld_rotY_CP.value;
            rotZ_CP = 0f + sld_rotZ_CP.value;

            // ---------- Objcet Control {Position, Rotation, Scale} ---------- //
            GameObject.Find(obj_name).transform.localPosition    = new Vector3(posX_CP, posY_CP, posZ_CP);
            GameObject.Find(obj_name).transform.localEulerAngles = new Vector3(rotX_CP, rotY_CP, rotZ_CP);
            GameObject.Find(obj_name).transform.localScale       = new Vector3(scale_CP, scale_CP, scale_CP);
        }

        // ------------------------ Start Animation ------------------------//
        btn_startS_CP.onClick.AddListener(TaskOnClick_StartSimBTN);
        // start animation -> after click on button
        if(startSim_CP == true)
        {
            // Animation -> ON State
            part_anim.Play(animS_name);
        }

        // ------------------------ Stop Animation ------------------------//
        btn_stopS_CP.onClick.AddListener(TaskOnClick_StopSimBTN);
        // stop animation -> after click on button
        if (stopSim_CP == true)
        {
            // Animation -> OFF State
            part_anim.Play("empty_state");
        }
    }

    // ------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------- FUNCTIONS ----------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------ //

    // -------------------- Reset Button -------------------- //
    void TaskOnClick_ResetBTN()
    {
        // change variable -> True {go to initialization state}
        reset_cpB = true;
        // turn off variable for animation
        startSim_CP = false;
        stopSim_CP = false;
    }

    // -------------------- Start Animation Button -------------------- //
    void TaskOnClick_StartSimBTN()
    {
        // Start animation -> change variable
        startSim_CP = true;
        // turn off variable 
        stopSim_CP = false;
        reset_cpB = false;
    }

    // -------------------- Stop Animation Button -------------------- //
    void TaskOnClick_StopSimBTN()
    {
        // Stop animation -> change variable
        stopSim_CP = true;
        // turn off variable 
        startSim_CP = false;
        reset_cpB = false;
    }
    void TaskOnClick_InfoBTN()
    {
        // Set information panel variable -> True and Initialization panel position to visible -> ON
        info_cpB = true;
        information_panelI.transform.localPosition = new Vector3(-550f, 0f, 0f);
        // document panel -> visible = OFF, variable = False
        doc_w3d = false;
        document_panelD.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
    }
    void TaskOnClick_EndBTN()
    {
        // panel visible -> OFF
        information_panelI.transform.localPosition = new Vector3(515f + (ex_param*10), 0f, 0f);
    }
    void TaskOnClick_DocBTN()
    {
        // Set document panel variable -> True and Initialization panel position to visible -> ON
        doc_w3d = true;
        document_panelD.transform.localPosition = new Vector3(-550f, 0f, 0f);
        // information panel -> visible = OFF, variable = False
        info_cpB = false;
        information_panelI.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
    }
    void TaskOnClick_EndBTNDP()
    {
        // panel visible -> OFF
        document_panelD.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
    }

    // -------------------- Open WebSite {URL} -------------------- //
    public void TaskOnClick_WebSiteBTN()
    {
        // used to open specific web pages - defined in GameObject Inspector {script properties}
        Application.OpenURL(URL);
    }

}
