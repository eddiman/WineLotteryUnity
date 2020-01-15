using Models;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LotterySelectButton : MonoBehaviour
    {
        public Lottery lottery;
        public GameObject btnText;

        public void SetLotteryButton(Lottery lottery)
        {
            this.lottery = lottery;
            btnText.GetComponent<TextMeshProUGUI>().text = "Lotteri: " + lottery.name;
        }

        public void SetCurrentLottery()
        {
            var lotteryController = GameObject.Find("LotteryController");
                lotteryController.GetComponent<LotteryController>().SetLottery(lottery);
        }
        //TODO: get the fuck rid of this
        public void invokeStateChange()
        {
            var stateManager = GameObject.Find("StateManager");
                stateManager.GetComponent<StateEnumManager>().SetState("DrawReady");
        }
    }
}
