using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInteract : MonoBehaviour
{
    public Camera mainCamera;
    private Outline _LastOutlineObj;//подсветка
    public float counterMousScroll=1;
    private float minimumScroll = 0;
    public TMP_Text textscreen;
    public TMP_Text Vtextscreen;
    public TMP_Text Atextscreen;
    private double R =1000;
    private double P = 400; 
    public GameObject center;
    public Vector3 AxisRotation;  
    void Update()
        
        //игрок движиться по локации и каждый кадр проверяет нет ли под курсором чего интересного
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //метод ниже проверяет не коснулся ли часом луч, обьекта у которого имеется OutLine
        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.transform.gameObject.CompareTag("Item"))
            {
                if (_LastOutlineObj != null)
                {
                    _LastOutlineObj.OutlineWidth = 0; 
                }
                _LastOutlineObj =
                hit.transform.gameObject.GetComponent<Outline>();
               _LastOutlineObj.OutlineWidth = 6;
                //если луч коснулся обьекта у которого есть оутлайн то увеличить ширину оутлайн
                float mw = Input.GetAxis("Mouse ScrollWheel")*10;
                
                if ((counterMousScroll + mw) > minimumScroll)
                {
                    counterMousScroll += mw;
                }
                
                if (counterMousScroll > 5) counterMousScroll = 5;
                if (counterMousScroll == 5)
                {
                    //метод вращения вокруг обьекта именуемого центром
                    hit.transform.RotateAround(center.transform.position,new Vector3(0,10,0),(mw-counterMousScroll)*10f);
                    counterMousScroll = mw;
                }  
            }
            else if (_LastOutlineObj != null)
            {
                _LastOutlineObj.OutlineWidth = 0;
                _LastOutlineObj = null;
            }
          //ниже метод расчета показаний мультиметра, в зависимости от позиции вращения  
        }
        if (hit.transform.rotation.eulerAngles.y > 10 && hit.transform.rotation.eulerAngles.y<40)
        {
            double VV = P/R;
            double V = Math.Sqrt(VV);
            textscreen.text = V.ToString("0.###");
            Vtextscreen.text = V.ToString("0.###");
            print("V");
        }
        if (hit.transform.rotation.eulerAngles.y > 50 && hit.transform.rotation.eulerAngles.y < 100)
        {
            textscreen.text = "0.01";
            print("-V");
        }
        if (hit.transform.rotation.eulerAngles.y >200 && hit.transform.rotation.eulerAngles.y < 260)
        {
            textscreen.text = R.ToString("#");
            print("om");
        }
        if (hit.transform.rotation.eulerAngles.y >180 && hit.transform.rotation.eulerAngles.y < 220)
        {
          
          double  AA = P*R;
            double A = Math.Sqrt( AA);
            
            textscreen.text =A.ToString("#.##");
            Atextscreen.text = A.ToString("#.##");
            print("A");
        }
    }
   
}
