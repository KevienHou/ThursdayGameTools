using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ThursdayTool.UI
{
    public class TipComponent : MonoBehaviour
    {
        public TextMeshProUGUI t_tip;
        public CanvasGroup cg;
        public float counter;
        private Tweener fadeOut;
        private Tweener fadeIn;

        public string text
        {
            set => t_tip.text = value;
            get => t_tip.text;
        }

        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
            t_tip = GetComponentInChildren<TextMeshProUGUI>();
        }

        internal void Init(RectTransform rectTransform)
        {
            cg = rectTransform.GetComponent<CanvasGroup>();
            t_tip = rectTransform.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Show(string content)
        {
            cg.SetActive(true);
            cg.alpha = 0;
            t_tip.text = content;
            counter = 2;
            fadeOut.Kill();
            fadeIn.Kill();
            fadeIn = cg.DOFade(1, 0.25f);
            isChanging = false;
            fadeIn.onComplete = () => { isChanging = true; };
        }

        private bool isChanging = false;

        private void Update()
        {
            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }
            else if (isChanging)
            {
                isChanging = false;
                fadeOut = cg.DOFade(0, 0.25f);
                fadeOut.onComplete = () => { cg.SetActive(false); };
            }
        }
    }
}