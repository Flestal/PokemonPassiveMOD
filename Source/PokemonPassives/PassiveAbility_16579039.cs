using BaseMod;
using HarmonyLib;
using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PokemonPassivesMOD
{
    class PassiveAbility_16579039:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "이 패시브 보유자가 살아 있는 동안 모든 아군의 속도 주사위 값 +2, 속도 6 이상인 모든 아군의 수비 주사위 위력 +3";
            }
        }
        public override void OnRoundStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList(this.owner.faction))
            {
                Buf_DeltaStream.AddBuf(target);
            }
        }
        public override void OnDie()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList(this.owner.faction))
            {
                Buf_DeltaStream.DestroyBuf(target);
            }
        }
        public class Buf_DeltaStream : BattleUnitBuf
        {
            protected override string keywordId
            {
                get { return "buf_Deltastream"; }
            }
            public override void OnUseCard(BattlePlayingCardDataInUnitModel card)
            {
                if (card.speedDiceResultValue >= 6)
                {
                    card.ApplyDiceStatBonus(DiceMatch.AllDefenseDice, new DiceStatBonus
                    {
                        power = 3
                    });
                }
            }
            public override int GetSpeedDiceAdder(int speedDiceResult)
            {
                return 2;
            }
            public static int GetBuf(BattleUnitModel model)
            {
                List<BattleUnitBuf> activatedBufList = model.bufListDetail.GetActivatedBufList();
                Buf_DeltaStream buf = activatedBufList.Find((BattleUnitBuf x) => x is Buf_DeltaStream) as Buf_DeltaStream;
                bool flag = buf == null;
                int result;
                if (flag)
                {
                    result = 0;
                }
                else
                {
                    result = buf.stack;
                }
                return result;
            }
            public static void AddBuf(BattleUnitModel model)
            {
                List<BattleUnitBuf> activatedBufList = model.bufListDetail.GetActivatedBufList();
                Buf_DeltaStream buf = activatedBufList.Find((BattleUnitBuf x) => x is Buf_DeltaStream) as Buf_DeltaStream;
                bool flag = buf == null;
                if (flag)
                {
                    buf = new Buf_DeltaStream(model);
                    buf.stack = 1;
                    model.bufListDetail.AddBuf(buf);
                }
                else
                {
                    buf.stack = 1;
                }
            }
            public static void DestroyBuf(BattleUnitModel model)
            {
                List<BattleUnitBuf> activatedBufList = model.bufListDetail.GetActivatedBufList();
                Buf_DeltaStream buf = activatedBufList.Find((BattleUnitBuf x) => x is Buf_DeltaStream) as Buf_DeltaStream;
                bool flag = buf == null;
                if (!flag)
                {
                    buf.Destroy();
                }
            }
            public Buf_DeltaStream(BattleUnitModel model)
            {
                this._owner = model;
                try
                {
                    typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all).SetValue(this, Harmony_Patch.ArtWorks["Rayquaza"]);
                    typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all).SetValue(this, true);
                }
                catch (Exception ex)
                {
                    File.WriteAllText(Application.dataPath + "/BaseMods/PokemonPassiveMOD/BufDeltaStreamerror.txt", ex.Message + Environment.NewLine + ex.StackTrace);
                }
                this.stack = 0;
            }
        }
    }
}
