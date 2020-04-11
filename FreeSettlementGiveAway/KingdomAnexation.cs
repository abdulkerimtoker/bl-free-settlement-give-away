using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.KingdomSettlement;
using TaleWorlds.Core;

namespace FreeSettlementGiveAway
{
    [HarmonyPatch(typeof(KingdomSettlementVM))]
    [HarmonyPatch("ExecuteAnnex")]
    public class KingdomAnexationPatch
    {
        static bool Prefix(KingdomSettlementItemVM ____currentSelectedSettlement)
        {
            if (____currentSelectedSettlement.Settlement.OwnerClan == Clan.PlayerClan)
            {
                var name = ____currentSelectedSettlement.Settlement.Name;
                InformationManager.DisplayMessage(new InformationMessage($"You have given away {name}"));
                ChangeOwnerOfSettlementAction.ApplyBySiege(Clan.PlayerClan.MapFaction.Leader, 
                    null, ____currentSelectedSettlement.Settlement);
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(KingdomSettlementVM))]
    [HarmonyPatch("AnnexCost", MethodType.Setter)]
    public class KingdomAnexationCostPatch
    {
        static void Prefix(KingdomSettlementItemVM ____currentSelectedSettlement, ref int value)
        {
            if (____currentSelectedSettlement != null &&
                ____currentSelectedSettlement.Settlement.OwnerClan == Clan.PlayerClan)
            {
                value = 0;
            }
        }
    }
}