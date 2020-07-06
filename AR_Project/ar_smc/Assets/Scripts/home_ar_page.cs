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
File Name: home_ar_page.cs

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

public class home_ar_page : MonoBehaviour
{
    // -------------------- Button -------------------- //
    public Button reset_CP, info_CP, end_IP, info_CP2, end_IP2,  webSite_CP, webSite_CP2, document_w3d, end_DP, vid_PB, end_VD;
    // -------------------- TextMeshProUGUI -------------------- //
    public TextMeshProUGUI infoPanel_txt;
    // -------------------- Image -------------------- //
    public Image information_panelI, information_panelI2, document_panelD, video_panel;
    // -------------------- RectTransform -------------------- //
    public RectTransform panel_rect;
    // -------------------- Float -------------------- //
    public float[] obj_pos = new float[3];
    private float ex_param = 100f;
    // -------------------- String -------------------- //
    public string obj_name;
    public string URL1, URL2;
    // -------------------- Bool -------------------- //
    private bool reset_cpB, info_cpB, info_cpB2, vid_cpB, doc_w3d;


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
        // Information panel no. 1
        information_panelI.transform.localPosition = new Vector3(1515f, 0f, 0f);
        // Information panel no. 2
        information_panelI2.transform.localPosition = new Vector3(2221, 0f, 0f);
        // Document Panel {PDF}
        document_panelD.transform.localPosition = new Vector3(1515f, 550f, 0f);
        // Video Panel
        video_panel.transform.localPosition = new Vector3(2221, 550f, 0f);

    }

    // ------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------ MAIN FUNCTION {Cyclic} ------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------ //
    void Update()
    {
        // ------------------------ Button ------------------------//
        // Info btn. {1,2} (move panel -> main window)
        info_CP.onClick.AddListener(TaskOnClick_InfoBTN);
        info_CP2.onClick.AddListener(TaskOnClick_Info2BTN);
        // Exit info panel {1, 2}
        end_IP.onClick.AddListener(TaskOnClick_EndBTN);
        end_IP2.onClick.AddListener(TaskOnClick_End2BTN);
        // Document Panel (move panel -> main window)
        document_w3d.onClick.AddListener(TaskOnClick_DocBTN);
        // Exit document panel
        end_DP.onClick.AddListener(TaskOnClick_EndBTNDP);
        // Video Panel (move panel -> main window)
        vid_PB.onClick.AddListener(TaskOnClick_VidSBTN);
        // Exit video panel
        end_VD.onClick.AddListener(TaskOnClick_VidEdnBTN);

        // ------------------------ RESET ALL 3D View ------------------------//
        // Reset all (go to initialization position of each panels, rect, etc.)
        reset_CP.onClick.AddListener(TaskOnClick_ResetBTN);
        // Reset Condition
        if (reset_cpB == true)
        {
            // ------------------------ Initialization Object ------------------------//
            // GameObject {Main}
            GameObject.Find(obj_name).transform.localPosition = new Vector3(obj_pos[0], obj_pos[1], obj_pos[2]);
            GameObject.Find(obj_name).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            GameObject.Find(obj_name).transform.localScale = new Vector3(1f, 1f, 1f);

            // Information panel no. 1
            information_panelI.transform.localPosition = new Vector3(1515f, 0f, 0f);
            // Information panel no. 2
            information_panelI2.transform.localPosition = new Vector3(2221, 0f, 0f);
            // Document Panel {PDF}
            document_panelD.transform.localPosition = new Vector3(1515f, 550f, 0f);
            // Video Panel
            video_panel.transform.localPosition = new Vector3(2221, 550f, 0f);

            // reset -> false (edge control)
            reset_cpB = false;
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
    }

    // -------------------- Visible Information {1} Panel -------------------- //
    void TaskOnClick_InfoBTN()
    {
        // Set information panel {1} variable -> True and Initialization panel position to visible -> ON
        info_cpB  = true;
        information_panelI.transform.localPosition = new Vector3(-550f, 0f, 0f);
        // Other panels -> visible = OFF, variable = False
        // information panel {2}
        info_cpB2 = false;
        information_panelI2.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
        // document panel
        doc_w3d = false;
        document_panelD.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
        // video panel
        vid_cpB = false;
        video_panel.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
    }
    // -------------------- Exit Information {1} Panel -------------------- //
    void TaskOnClick_EndBTN()
    {
        // panel visible -> OFF
        information_panelI.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
    }

    // -------------------- Visible Information {2} Panel -------------------- //
    void TaskOnClick_Info2BTN()
    {
        // Set information panel {2} variable -> True and Initialization panel position to visible -> ON
        info_cpB2 = true;
        information_panelI2.transform.localPosition = new Vector3(-550f, 0f, 0f);
        // Other panels -> visible = OFF, variable = False
        // information panel {1}
        info_cpB = false;
        information_panelI.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
        // document panel
        doc_w3d = false;
        document_panelD.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
        // video panel
        vid_cpB = false;
        video_panel.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
    }

    // -------------------- Exit Information {2} Panel  -------------------- //
    void TaskOnClick_End2BTN()
    {
        // panel visible -> OFF
        information_panelI2.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
    }

    // -------------------- Visible Video Panel -------------------- //
    void TaskOnClick_VidSBTN()
    {
        // Set video panel variable -> True and Initialization panel position to visible -> ON
        vid_cpB = true;
        video_panel.transform.localPosition = new Vector3(-550f, 0f, 0f);
        // Other panels -> visible = OFF, variable = False
        // information panel {1}
        info_cpB = false;
        information_panelI.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
        // information panel {2}
        info_cpB2 = false;
        information_panelI2.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
        // document panel
        doc_w3d = false;
        document_panelD.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
    }

    // -------------------- Exit Video Panel  -------------------- //
    void TaskOnClick_VidEdnBTN()
    {
        // panel visible -> OFF
        video_panel.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
    }

    // -------------------- Visible Document Panel -------------------- //
    void TaskOnClick_DocBTN()
    {
        // Set document panel variable -> True and Initialization panel position to visible -> ON
        doc_w3d = true;
        document_panelD.transform.localPosition = new Vector3(-550f, 0f, 0f);
        // Other panels -> visible = OFF, variable = False
        // information panel {1}
        info_cpB = false;
        information_panelI.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
        // information panel {2}
        info_cpB2 = false;
        information_panelI2.transform.localPosition = new Vector3(515f + (ex_param * 10), 0f, 0f);
        // video panel
        vid_cpB = false;
        video_panel.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
    }

    // -------------------- Exit Document Panel  -------------------- //
    void TaskOnClick_EndBTNDP()
    {
        // panel visible -> OFF
        document_panelD.transform.localPosition = new Vector3(515f + (ex_param * 10), 550f, 0f);
    }

    // -------------------- Open WebSite {URL no. 1} -------------------- //
    public void TaskOnClick_WebSiteBTN()
    {
        // used to open specific web pages - defined in GameObject Inspector {script properties}
        Application.OpenURL(URL1);
    }

    // -------------------- Open WebSite {URL no. 2} -------------------- //
    public void TaskOnClick_WebSiteBTN2()
    {
        // used to open specific web pages - defined in GameObject Inspector {script properties}
        Application.OpenURL(URL2);
    }

}
