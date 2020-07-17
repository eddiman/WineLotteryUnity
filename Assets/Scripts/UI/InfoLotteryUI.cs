using System.Linq;
using Models;
using TMPro;
using UnityEngine;

public class InfoLotteryUI : MonoBehaviour
{

    public GameObject lotteryController;
    public Lottery currentLottery;
    public GameObject ScrollView;
    public GameObject StartTimeTmp;
    public GameObject DescriptionTmp;
    public GameObject NoOfDrawsTmp;
    public GameObject NoOfTicketsTmp;
    public GameObject LotteryWinners;
    public GameObject LotteryWinnersTmp;
    public GameObject LotteryParticipants;
    public GameObject LotteryParticipantsTmp;


    public void PopulateInfoAboutLottery()
    {
        float winnersYOffset = LotteryWinnersTmp.GetComponent<RectTransform>().localPosition.y;
        float participantYOffset = LotteryParticipantsTmp.GetComponent<RectTransform>().localPosition.y;
        currentLottery = lotteryController.GetComponent<LotteryController>().currentLottery;
        System.DateTime dateTime = System.DateTime.Parse(currentLottery.dateTime);
        StartTimeTmp.GetComponent<TextMeshProUGUI>().text = "Lotteriet starter: " + dateTime.ToString("HH:mm dd.MM.yy");
        DescriptionTmp.GetComponent<TextMeshProUGUI>().text = "Beskrivelse: " + currentLottery.description;
        NoOfDrawsTmp.GetComponent<TextMeshProUGUI>().text = "Antall trekninger: " + currentLottery.numberOfDraws;
        NoOfTicketsTmp.GetComponent<TextMeshProUGUI>().text = "Antall samlet lodd: " + currentLottery.numberOfTickets;

        //Adds height to the "Content" object of the crollview of the scroll view to display all participants
        var contentViewRectTransform = ScrollView.transform.GetChild(0).Find("Content").GetComponent<RectTransform>();
        var currentSizeDeltaHeight = contentViewRectTransform.sizeDelta.y;
        contentViewRectTransform.sizeDelta = new Vector2(contentViewRectTransform.sizeDelta.x, currentSizeDeltaHeight + currentLottery.participants.Count * 50);
        foreach (var participant in currentLottery.participants)
        {

            var go = Instantiate(LotteryParticipantsTmp, LotteryParticipants.transform);
            go.SetActive(true);
            var goRectTransf = go.GetComponent<RectTransform>();
            goRectTransf.localPosition = new Vector3(goRectTransf.localPosition.x, participantYOffset);
            participantYOffset += (goRectTransf.rect.height * -1);
            go.GetComponent<TextMeshProUGUI>().text = participant.name + ": " + participant.numberOfTickets + " lodd";

        }

        //If atleast on draw has already been started, list out the winners
        if (!currentLottery.draws[0].started) return;
        LotteryWinners.SetActive(true);

        foreach (var draw in currentLottery.draws)
        {
            if (!draw.started) return;
            var go = Instantiate(LotteryWinnersTmp, LotteryWinners.transform);
            go.SetActive(true);
            var goRectTransf = go.GetComponent<RectTransform>();
            goRectTransf.localPosition = new Vector3(goRectTransf.localPosition.x, winnersYOffset);
            winnersYOffset += (goRectTransf.rect.height * -1);
            go.GetComponent<TextMeshProUGUI>().text = draw.winner;
        }


    }
}
