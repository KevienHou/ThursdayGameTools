using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThursdayTool.UI.Custem
{
    public class UI_BtnGroup : MonoBehaviour
    {
        public List<UI_Btn> allBtns = new List<UI_Btn>();

        public int defaultIndex = 0;

        public void AddSelf(UI_Btn temp)
        {
            if (allBtns.Contains(temp))
            {
                return;
            }

            allBtns.Add(temp);
        }

        private void Start()
        {
            if (allBtns.Count > 0)
            {
                defaultIndex = defaultIndex >= allBtns.Count ? 0 : defaultIndex;
                allBtns[defaultIndex].TiggerEvent();
            }
        }


        internal void SetImg(UI_Btn temp)
        {
            allBtns.ForEach(x =>
            {
                if (!x.Equals(temp))
                {
                    x.SetNormal();
                }
            });
        }
    }
}