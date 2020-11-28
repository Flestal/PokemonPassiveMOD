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
    class PassiveAbility_16579038:PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "이 패시브 보유자가 살아 있는 동안 부여되는 모든 부식, 충전 수치가 3배가 됨";
            }
        }
        public override void OnRoundStart()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
            {
                Buf_AlphaSapphire.AddBuf(target);
            }
        }
        public override void OnDie()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
            {
                Buf_AlphaSapphire.DestroyBuf(target);
            }
        }
        public class Buf_AlphaSapphire : BattleUnitBuf
        {
            protected override string keywordId
            {
                get { return "buf_Alphasapphire"; }
            }
            public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
            {
                if (cardBuf.bufType == KeywordBuf.Decay||cardBuf.bufType==KeywordBuf.WarpCharge)
                {
                    return stack*2;
                }
                return 0;
            }
            public static int GetBuf(BattleUnitModel model)
            {
                List<BattleUnitBuf> activatedBufList = model.bufListDetail.GetActivatedBufList();
                Buf_AlphaSapphire buf = activatedBufList.Find((BattleUnitBuf x) => x is Buf_AlphaSapphire) as Buf_AlphaSapphire;
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
                Buf_AlphaSapphire buf = activatedBufList.Find((BattleUnitBuf x) => x is Buf_AlphaSapphire) as Buf_AlphaSapphire;
                bool flag = buf == null;
                if (flag)
                {
                    buf = new Buf_AlphaSapphire(model);
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
                Buf_AlphaSapphire buf = activatedBufList.Find((BattleUnitBuf x) => x is Buf_AlphaSapphire) as Buf_AlphaSapphire;
                bool flag = buf == null;
                if (!flag)
                {
                    buf.Destroy();
                }
            }
            public Buf_AlphaSapphire(BattleUnitModel model)
            {
                this._owner = model;
                try
                {
                    typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all).SetValue(this, Harmony_Patch.ArtWorks["Kyogre"]);
                    typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all).SetValue(this, true);
                }
                catch (Exception ex)
                {
                    File.WriteAllText(Application.dataPath + "/BaseMods/PokemonPassiveMOD/BufAlphaSapphireerror.txt", ex.Message + Environment.NewLine + ex.StackTrace);
                }
                this.stack = 0;
            }
        }
    }
}
