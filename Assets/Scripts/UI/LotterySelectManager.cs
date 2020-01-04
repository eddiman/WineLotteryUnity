using System;
using Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class LotterySelectManager : MonoBehaviour
    {
        [SerializeField]
        public List<Lottery> lotteryList = new List<Lottery>();

        public Transform lotteryScrollView;
        public Transform lotteryButton;
        public Lottery selectedLottery;


        public void SetLotteryList(List<Lottery> list)
        {
            lotteryList = list;
        }

        public void PopulateScrollView()
        {

            float btnYOffset = 0;
            GameObject content = lotteryScrollView.Find("Viewport").Find("Content").gameObject;

            foreach (var lottery in lotteryList)
            {
                //Finds the Content-child
                if (lotteryButton == null) continue;
                Transform btn = Instantiate(lotteryButton, content.transform, false);
                var rectTrans = btn.GetComponent<RectTransform>();
                btn.GetComponent<LotterySelectButton>().SetLotteryButton(lottery);
                rectTrans.localPosition = new Vector3(rectTrans.rect.width / 2, btnYOffset * -1);
                btnYOffset += rectTrans.rect.height;
            }

            var contentRect = content.GetComponent<RectTransform>().rect;
            contentRect.size = new Vector2(contentRect.width, btnYOffset);
        }
    }
}
