using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579030:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대 시작 시 모든 상대의 모든 전투 책장의 필요 빛 +1";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
                if (target.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList((this.owner.faction == Faction.Player) ? Faction.Enemy : Faction.Player);
            foreach(BattleUnitModel target in aliveList)
            {
                List<BattleDiceCardModel> cards = target.allyCardDetail.GetAllDeck();
                foreach(BattleDiceCardModel card in cards)
                {
                    if (card.GetCost() <= card.GetOriginCost())
                    {
                        card.AddCost(1);
                    }
                }
            }
        }
    }
}
