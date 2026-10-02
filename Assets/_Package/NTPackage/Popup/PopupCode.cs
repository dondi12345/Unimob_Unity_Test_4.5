using System;
using UnityEngine;

namespace NTPackage.UI
{
    public class PopupCodeParser
    {
        public static PopupCode FromString(string name)
        {
            //name = name.ToLower();W
            return (PopupCode)Enum.Parse(typeof(PopupCode), name);
        }
    }

    [System.Serializable]
    public enum PopupCode
    {
        Unknown = 0,
        ElementalChemicalInputUI = 1,
        BoxChemicalInputUI = 2,
        BoxChemicalUI = 3,
        MachineChemicalUI = 4,
        MessagePanel = 5,
        ChemicalGraphUI = 6,
        BoxChemicalPushPullUI = 7,
        LoadingPanel = 8,
        HistoryStepUI = 9,
        AccessTokenUI = 10,
        TooltipUI = 11,
        GuessChemicalUI = 12,
        HyperlinkOutletLinkageUI = 13,
        HyperlinkFeedStageLinkageUI = 14,
        HyperlinkElementDetailPopup = 15,
        ListLinkagePopup = 16,
        HyperlinkOutletLinkageAQUI = 17,
        HyperlinkOutletLinkageORGUI = 18,
        HyperlinkOutletLinkageDualUI = 19,
        HyperlinkFeedLinkageAqUI = 20,
        HyperlinkFeedLinkageOrgUI = 21,
        HyperlinkHorizontalAqUI = 22,
        HyperlinkHorizontalOrgUI = 23,
        HyperlinkHorizontalBothUI = 24,
        HyperlinkFeedLinkageAqABUI = 25,
        HyperlinkFeedLinkageOrgABUI = 26,
        DynamicHInputUI = 27,
        DynamicHSelectAQ_ORGPopupUI = 28,
        DynamicHMachineUI = 29,
        MessageOptionPanel = 30,
        StrippingInputUI = 31,
        StrippingInputSingleUI = 32,
        UITooltip_v1 = 33,
        DynamicHKInputUI = 34,
        DynamicHKMachineUI = 35,
        GuessFeedNoneChemicalUI = 36,
        GuessFeedNoneChemicalInputUI = 37,
        SeparationTargetSelectInputUI = 38,
        ResepaBasicCalculation2EUI = 39,
        GenerateTokenUI = 40,
        VertifyTokenUI = 41,
        FeedBoxInputUI = 42,
        RareEarthBoxInputUI = 43,
        WorldConnectionInputUI = 44,
        DynamicHKMachineUIMap = 45,
        WorldDynamicInputUI = 46,
        RareEarthBoxInfoUI = 47,
        FeedBoxInfoUI = 48,
        InputBoxInfoUI = 49,
        DynamicHBasicInputUI = 50,
    }
}
