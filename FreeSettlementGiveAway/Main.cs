using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace FreeSettlementGiveAway
{
    public class Main : MBSubModuleBase
    {
        private static bool _isPatched = false;

        protected override void OnSubModuleLoad()
        {
            PatchAll();
        }

        private void PatchAll()
        {
            if (_isPatched) 
                return;
            
            var harmony = new Harmony("FreeSettlementGiveAway");
            harmony.PatchAll();
            _isPatched = true;
        }
    }
}