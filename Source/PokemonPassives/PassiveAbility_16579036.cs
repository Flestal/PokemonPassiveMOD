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
    class PassiveAbility_16579036 : PassiveAbilityBase
    {
        public override string debugDesc
        {
            get
            {
                return "무대 시작 시 5막 동안 부여되는 화상 수치가 2배가 됨";
            }
        }
        public override void OnWaveStart()
        {
            foreach(BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
            {
                Passive_16579036_buf.AddBuf(target, 5);
            }
        }
        public class Passive_16579036_buf : BattleUnitBuf
        {
            protected override string keywordId
            {
                get { return "buf_16579036"; }
            }
            protected override string keywordIconId
            {
                get { return "Burn"; }
            }
            public override void OnRoundEndTheLast()
            {
                this.stack--;
                if (this.stack <= 0)
                {
                    this.Destroy();
                }
            }
            public override int OnGiveKeywordBufByCard(BattleUnitBuf cardBuf, int stack, BattleUnitModel target)
            {
                if (cardBuf.bufType == KeywordBuf.Burn)
                {
                    return stack;
                }
                return 0;
            }
            public static int GetBuf(BattleUnitModel model)
            {
                List<BattleUnitBuf> activatedBufList = model.bufListDetail.GetActivatedBufList();
                Passive_16579036_buf buf = activatedBufList.Find((BattleUnitBuf x) => x is Passive_16579036_buf) as Passive_16579036_buf;
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
            public static void AddBuf(BattleUnitModel model, int add)
            {
                List<BattleUnitBuf> activatedBufList = model.bufListDetail.GetActivatedBufList();
                Passive_16579036_buf buf=activatedBufList.Find((BattleUnitBuf x) => x is Passive_16579036_buf) as Passive_16579036_buf;
                bool flag = buf == null;
                if (flag)
                {
                    buf = new Passive_16579036_buf(model);
                    buf.Add(add);
                    model.bufListDetail.AddBuf(buf);
                }
                else
                {
                    buf.Add(add);
                }
            }
            public void Add(int add)
            {
                this.stack += add;
            }
            public Passive_16579036_buf(BattleUnitModel model)
            {
                this._owner = model;
                /*try
                {
                    //typeof(BattleUnitBuf).GetField("_bufIcon", AccessTools.all).SetValue(this, Harmony_Patch.ArtWorks["DarkFogWave"]);
                    typeof(BattleUnitBuf).GetField("_iconInit", AccessTools.all).SetValue(this, true);
                }
                catch (Exception ex)
                {
                    File.WriteAllText(Application.dataPath + "/BaseMods/DarkFogWaveerror.txt", ex.Message + Environment.NewLine + ex.StackTrace);
                }*/
                this.stack = 0;
            }
        }
    }
}
