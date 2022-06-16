using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace ThursdayTool.UI.Custem
{
    [Serializable]
    public class BtnEvent : UnityEvent
    {
    }

    [RequireComponent(typeof(Image))]
    public class UI_Btn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private bool interactive = true;
        public Image graphic;

        [Space(10)] public Sprite normalSprite;
        public Sprite highlightSprite;
        public Sprite selectedSprite;

        [Space(10)] public UI_BtnGroup btnGroup;

        [Space(10)] [SerializeField] private BtnEvent onClickEvent;

        public BtnEvent OnClick
        {
            get => onClickEvent;
            set => onClickEvent = value;
        }

        private bool isSelected = false;

        private void Reset()
        {
            graphic = GetComponent<Image>();
        }

        private bool init = false;

        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            // ReSharper disable once Unity.NoNullPropagation
            btnGroup?.AddSelf(this);
            isSelected = false;
            SetImg(ImgType.Nor); 
            if (interactive)
            {
                graphic.color = new Color(1, 1, 1, 1);
            }
            else
            {
                graphic.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }


        private void Start()
        {
            Init();
        }


        public bool Interactive
        {
            set
            {
                interactive = value;
                if (value)
                {
                    isSelected = false;
                    graphic.color = new Color(1, 1, 1, 1);
                    SetImg(ImgType.Dis);
                }
                else
                {
                    isSelected = false;
                    graphic.color = new Color(0.5f, 0.5f, 0.5f, 1);
                    SetImg(ImgType.Nor);
                }
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isSelected || !interactive) return;
            SetImg(ImgType.High);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isSelected || !interactive) return;
            SetImg(ImgType.Sel);
            TiggerEvent();
            // ReSharper disable once Unity.NoNullPropagation
            btnGroup?.SetImg(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isSelected || !interactive) return;
            SetImg(ImgType.Nor);
        }


        internal void TiggerEvent()
        {
            isSelected = true;
            SetImg(ImgType.Sel);
            onClickEvent?.Invoke();
        }

        public void SetNormal()
        {
            SetImg(ImgType.Nor);
        }

        private void SetImg(ImgType type1)
        {
            switch (type1)
            {
                case ImgType.Nor:
                    isSelected = false;
                    graphic.sprite = normalSprite;
                    break;
                case ImgType.High:
                    graphic.sprite = highlightSprite;
                    break;
                case ImgType.Sel:
                    graphic.sprite = selectedSprite;
                    break;
                case ImgType.Dis:
                    graphic.sprite = selectedSprite;
                    break;
            }
        }


        private enum ImgType
        {
            Nor,
            High,
            Sel,
            Dis
        }
    }
}