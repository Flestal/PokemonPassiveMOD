using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579002:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대 시작시 이 패시브와 전투책장을 지우고 무작위 적의 패시브와 전투책장을 가져옴";
            }
        }
        public override void OnWaveStart()
        {
            foreach (BattleUnitModel t in BattleObjectManager.instance.GetAliveList())
                if (t.passiveDetail.HasPassive<PassiveAbility_16579000>())
                {
                    this.owner.passiveDetail.DestroyPassive(this);
                }
            //무작위 상대 선정
            BattleUnitModel target = BattleObjectManager.instance.GetAliveList_random((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy, 1)[0];
            //List<PassiveAbilityBase> passivelist = base.owner.passiveDetail.PassiveList;
            //패시브 가져오기
            //상대 패시브 복사
            List<PassiveAbilityBase> passivelist_target = target.passiveDetail.PassiveList;
            List<PassiveAbilityBase> passivelist_origin = this.owner.passiveDetail.PassiveList;
            
            
            foreach(PassiveAbilityBase passive in passivelist_target)
            {
                this.owner.passiveDetail.AddPassive(passive);
            }
            //전투책장 가져오기
            base.owner.allyCardDetail.ExhaustAllCards();
            List<BattleDiceCardModel> targetdeck=target.allyCardDetail.GetAllDeck();
            foreach (BattleDiceCardModel battleDiceCardModel in targetdeck)
            {
                battleDiceCardModel.owner = base.owner;
            }
            base.owner.allyCardDetail.AddCardToDeck(targetdeck);
            base.owner.allyCardDetail.DrawCards(4);
            try
            {
                //this.owner.passiveDetail.RemovePassive();
                /*foreach (PassiveAbilityBase passive in this.owner.passiveDetail.PassiveList)
                {
                    this.owner.passiveDetail.DestroyPassive(passive);
                }*/
                this.owner.passiveDetail.PassiveList.Remove(this);
            }
            catch(Exception ex)
            {
                //File.WriteAllText(Application.dataPath + "/BaseMods/error.txt", ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
