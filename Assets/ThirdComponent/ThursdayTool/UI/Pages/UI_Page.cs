using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThursdayTool.UI.Custem
{
    public class UI_Page : MonoBehaviour
    {
        private Transform pageBtns;
        private TextMeshProUGUI[] SevenText; //七个页码
        private Button[] SevenBtn; //七个按钮
        private Button btnPriv; //左侧的按钮
        private Button btnNext; //右侧的按钮
        private Button btnJump; //右侧的按钮
        private int allPageCount; //一共有多少页
        private TMP_InputField InputNumber; //输入的页码(要跳转的页码)
        private int AskPage = -1; // 当前请求的是第几页的内容
        private int LastPage = -1; // 上一次是点击的第几页
        private int LastIndex = -1; // 上一次点击的是第几个按钮
        private Color blue = new Color(88 / 255, 176 / 255, 1, 1);
        private Action<int> askPageAction;

        public void Init(Transform transform = null, Action<int> pageCallback = null)
        {
            transform = transform ? transform : this.transform;
            PageComponentInit(transform, pageCallback);
            Refresh();
        }

        public void PageCountRefresh(int allPageCount)
        {
            this.allPageCount = allPageCount;
            Refresh();
        }

        private void PageComponentInit(Transform transform, Action<int> pageCallback = null)
        {
            //this.allPageCount = allPageCount;
            blue = new Color(0.34f, 0.69f, 1, 1);
            askPageAction = pageCallback;
            pageBtns = transform.Find("Pages");
            SevenBtn = pageBtns.GetComponentsInChildren<Button>();
            SevenText = new TextMeshProUGUI[SevenBtn.Length];
            for (int i = 0; i < SevenBtn.Length; i++)
            {
                int index = i;
                SevenText[i] = SevenBtn[i].GetComponentInChildren<TextMeshProUGUI>();
                SevenBtn[i].onClick.AddListener(() => { Btn_ClickBtn(index); });
                //SevenBtn[i].image.color = Color.white;
            }

            btnPriv = transform.Find("Priv").GetComponent<Button>();
            btnNext = transform.Find("Next").GetComponent<Button>();
            btnJump = transform.Find("Jump").GetComponent<Button>();
            InputNumber = transform.Find("JumpInput").GetComponent<TMP_InputField>();

            #region PageDataInit

            AskPage = 1;
            RefreshAgain();

            #endregion

            btnPriv.onClick.AddListener(Btn_Left);
            btnNext.onClick.AddListener(Btn_Right);
            btnJump.onClick.AddListener(Btn_Jump);

            InputNumber.onEndEdit.AddListener(Ipt_End);
        }

        public int GetLastPage
        {
            get => LastPage;
        }

        public void ShowTarPage(int page)
        {
            if (page >= allPageCount)
            {
                Debug.Log("页码越界");
                return;
            }

            LastPage = -1;
            InputNumber.text = page.ToString();
            Btn_Jump();
        }

        private void Ipt_End(string arg0)
        {
            Btn_Jump();
        }

        internal void Refresh()
        {
            if (allPageCount <= 7)
            {
                btnPriv.gameObject.SetActive(false);
                btnNext.gameObject.SetActive(false);
                for (int i = 0; i < SevenBtn.Length; i++)
                {
                    if (i < allPageCount)
                    {
                        SevenBtn[i].gameObject.SetActive(true);
                        SevenText[i].text = (i + 1).ToString();
                    }
                    else
                        SevenBtn[i].gameObject.SetActive(false);
                }

                for (int i = 0; i < SevenBtn.Length; i++)
                {
                    if (SevenText[i].text == AskPage.ToString())
                    {
                        //Debug.Log(" RefreshAgain  " + AskPage + "  " + i);
                        //SevenBtn[i].GetComponent<Image>().color= blue;
                        SevenBtn[i].GetComponent<Image>().enabled = true;
                        //LastIndex = i;
                    }
                    else
                    {
                        //SevenBtn[i].GetComponent<Image>().color= Color.white;
                        SevenBtn[i].GetComponent<Image>().enabled = false;
                    }
                }
            }
            else
            {
                btnPriv.gameObject.SetActive(true);
                btnNext.gameObject.SetActive(true);
                for (int i = 0; i < SevenBtn.Length; i++)
                {
                    SevenBtn[i].gameObject.SetActive(true);
                    SevenText[i].text = (i + 1).ToString();
                    if (i == 5)
                    {
                        SevenText[i].text = "...";
                    }
                    else if (i == 6)
                    {
                        SevenText[i].text = allPageCount.ToString();
                    }
                }
            }
        }

        internal void SetAllPageCount(int count)
        {
            allPageCount = count;
        }

        private void Btn_Jump()
        {
            if (string.IsNullOrEmpty(InputNumber.text))
            {
                //Debug.Log("输入框的内容为空");
                return;
            }

            int inputNum = int.Parse(InputNumber.text);

            if (allPageCount >= inputNum && inputNum >= 1)
            {
                AskPage = inputNum;
            }
            else
            {
                //Debug.Log("当前最大的页码为" + CurrentAllPage);
                return;
            }

            if (LastPage == AskPage)
            {
                return;
            }

            LastPage = AskPage;

            RefreshAgain();
            InputNumber.text = "";
        }

        private void Btn_ClickBtn(int index)
        {
            if (LastIndex == index) return;

            switch (index)
            {
                case 0:
                    //Debug.Log("点击第1个按钮");
                    AskPage = int.Parse(SevenText[index].text);
                    // Debug.Log("当前请求的是第" + SevenText[index].text + "页的内容");
                    // LastPage = AskPage;
                    LastIndex = index;
                    RefreshAgain();
                    break;
                case 1:
                    // Debug.Log("点击第2个按钮");
                    string number1 = SevenText[index].text;
                    if (number1 != "...")
                    {
                        // Debug.Log("当前请求的是第" + SevenText[index].text + "页的内容");
                        AskPage = int.Parse(SevenText[index].text);
                    }
                    else if (index > LastIndex && LastPage + 5 < allPageCount)
                    {
                        AskPage = LastPage + 5;
                        // Debug.Log("当前请求的是第" + LastPage + 5 + "页的内容");
                    }
                    else if (index < LastIndex && LastPage - 5 >= 1)
                    {
                        AskPage = LastPage - 5;
                        // Debug.Log("当前请求的是第" + (LastPage - 5) + "页的内容");
                    }

                    // LastPage = AskPage;
                    LastIndex = index;
                    RefreshAgain();
                    break;
                case 2:
                    // Debug.Log("点击第3个按钮");
                    AskPage = int.Parse(SevenText[index].text);
                    // Debug.Log("当前请求的是第" + SevenText[index].text + "页的内容");

                    // LastPage = AskPage;
                    LastIndex = index;
                    RefreshAgain();
                    break;
                case 3:
                    //Debug.Log("点击第4个按钮");
                    AskPage = int.Parse(SevenText[index].text);
                    // Debug.Log("当前请求的是第" + SevenText[index].text + "页的内容");

                    // LastPage = AskPage;
                    LastIndex = index;
                    RefreshAgain();
                    break;
                case 4:
                    // Debug.Log("点击第5个按钮");
                    AskPage = int.Parse(SevenText[index].text);
                    // Debug.Log("当前请求的是第" + SevenText[index].text + "页的内容");

                    // LastPage = AskPage;
                    LastIndex = index;
                    RefreshAgain();
                    break;
                case 5:
                    // Debug.Log("点击第6个按钮");
                    string number = SevenText[index].text;
                    if (number != "...")
                    {
                        //     Debug.Log("当前请求的是第" + SevenText[index].text + "页的内容");
                        AskPage = int.Parse(SevenText[index].text);
                    }
                    else if (index > LastIndex && LastPage + 5 <= allPageCount)
                    {
                        AskPage = LastPage + 5;
                        //    Debug.Log("当前请求的是第" + AskPage + "页的内容");
                    }
                    else if (index < LastIndex && LastPage - 5 >= 1)
                    {
                        AskPage = LastPage - 5;
                        //    Debug.Log("当前请求的是第" + AskPage + "页的内容");
                    }

                    // LastPage = AskPage;
                    LastIndex = index;
                    RefreshAgain();
                    break;
                case 6:
                    // Debug.Log("点击第7个按钮");
                    AskPage = int.Parse(SevenText[index].text);
                    //  Debug.Log("当前请求的是第" + SevenText[index].text + "页的内容");
                    // LastPage = AskPage;
                    LastIndex = index;
                    RefreshAgain();
                    break;
            }
        }

        private void RefreshAgain()
        {
            LastPage = AskPage;
            //可以单独写个函数，把要请求的当前页传过去，向服务器请求当前页的消息
            askPageAction?.Invoke(AskPage);
            //Debug.Log("目前请求的是第" + AskPage + "页的内容");
            if (allPageCount <= 7)
            {
                //Debug.Log("刷新1");
                btnPriv.gameObject.SetActive(false);
                btnNext.gameObject.SetActive(false);
                for (int i = 0; i < SevenBtn.Length; i++)
                {
                    //Debug.Log(i + " <  " + allPageCount);
                    if (i < allPageCount)
                    {
                        SevenBtn[i].gameObject.SetActive(true);
                        SevenText[i].text = (i + 1).ToString();

                        //Debug.Log(i);
                    }
                    else
                    {
                        SevenBtn[i].gameObject.SetActive(false);
                    }
                }

                for (int i = 0; i < SevenBtn.Length; i++)
                {
                    if (SevenText[i].text == AskPage.ToString())
                    {
                        //Debug.Log(" RefreshAgain  " + AskPage + "  " + i);
                        //SevenBtn[i].GetComponent<Image>().color= blue;
                        SevenBtn[i].GetComponent<Image>().enabled = true;
                        LastIndex = i;
                    }
                    else
                    {
                        //SevenBtn[i].GetComponent<Image>().color= Color.white;
                        SevenBtn[i].GetComponent<Image>().enabled = false;
                    }
                }
            }
            else
            {
                btnPriv.gameObject.SetActive(true);
                btnNext.gameObject.SetActive(true);
                if (AskPage <= 5)
                {
                    //Debug.Log("刷新2");

                    SevenText[0].text = 1.ToString();
                    SevenText[1].text = 2.ToString();
                    SevenText[2].text = 3.ToString();
                    SevenText[3].text = 4.ToString();
                    SevenText[4].text = 5.ToString();
                    SevenText[5].text = "...";
                    SevenText[6].text = allPageCount.ToString();
                }
                else if (AskPage >= allPageCount - 4)
                {
                    //Debug.Log("刷新3");

                    SevenText[0].text = 1.ToString();
                    SevenText[1].text = "...";
                    SevenText[2].text = (allPageCount - 4).ToString();
                    SevenText[3].text = (allPageCount - 3).ToString();
                    SevenText[4].text = (allPageCount - 2).ToString();
                    SevenText[5].text = (allPageCount - 1).ToString();
                    SevenText[6].text = allPageCount.ToString();
                }
                else
                {
                    //Debug.Log("刷新4");
                    SevenText[0].text = 1.ToString();
                    SevenText[1].text = "...";
                    SevenText[2].text = AskPage.ToString();
                    SevenText[3].text = (AskPage + 1).ToString();
                    SevenText[4].text = (AskPage + 2).ToString();
                    SevenText[5].text = "...";
                    SevenText[6].text = allPageCount.ToString();
                }

                for (int i = 0; i < SevenBtn.Length; i++)
                {
                    SevenBtn[i].gameObject.SetActive(true);
                    if (SevenText[i].text == AskPage.ToString())
                    {
                        //SevenBtn[i].GetComponent<Image>().color= blue;
                        SevenBtn[i].GetComponent<Image>().enabled = true;
                        LastIndex = i;
                    }
                    else
                    {
                        //SevenBtn[i].GetComponent<Image>().color= Color.white;
                        SevenBtn[i].GetComponent<Image>().enabled = false;
                    }
                }
            }
        }

        private void Btn_Left()
        {
            if (AskPage == 1)
            {
                //Debug.Log("当前是首页");
                return;
            }

            AskPage--;
            LastPage = AskPage;
            LastIndex--;
            RefreshAgain();
        }

        private void Btn_Right()
        {
            if (AskPage == allPageCount)
            {
                //Debug.Log("当前是最后一页");
                return;
            }

            AskPage++;
            LastPage = AskPage;
            LastIndex--;
            RefreshAgain();
        }
    }
}