using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapitalSkLib.ModuleObject;

namespace TwTradeSK
{
    public class QuoteTickManager
    {
        public List<KLineInfo> KLineSeries { get; set; }        
        private KLineInfo _lastKLine { get; set; }

        public int KLineCount { get; set; }

        public QuoteTickManager()
        {
            this.KLineSeries = new List<KLineInfo>();
        }

        public void AddSkKLine(TwKLineData skKLine)
        {

        }

        public void AddTwTick(TickInfo skTick)
        {
            if (this._lastKLine == null)
            {
                DateTime marketStartTime = DateTime.Today.AddHours(8).AddMinutes(45);
                DateTime tmpTime = marketStartTime;
                while (skTick.TickTime > tmpTime)
                {
                    KLineInfo kline = new KLineInfo(tmpTime);
                    KLineSeries.Add(kline);
                    this._lastKLine = kline;
                    tmpTime.AddMinutes(1);
                }

                KLineInfo lastK = new KLineInfo(skTick.TickTime);
                lastK.AddTick(new Tick
                {
                    Close = skTick.Close,
                    Qty = skTick.Qty,
                    TickTime = skTick.TickTime
                });

                this.KLineSeries.Add(lastK);
                this._lastKLine = lastK;
            }
            else
            {
                if (skTick.TickTime > this._lastKLine.EndTime)
                {
                    KLineInfo lastK = new KLineInfo(this._lastKLine.SkKLine, this._lastKLine, skTick.TickTime);
                    lastK.AddTick(new Tick
                    {
                        Close = skTick.Close,
                        Qty = skTick.Qty,
                        TickTime = skTick.TickTime
                    });

                    this.KLineSeries.Add(lastK);
                    this._lastKLine = lastK;
                }
                else
                {
                    this._lastKLine.AddTick(new Tick
                    {
                        Close = skTick.Close,
                        Qty = skTick.Qty,
                        TickTime = skTick.TickTime
                    });
                }
            }

        }
    }

    public class KLineInfo
    {
        public int LineNo { get; set; }
        public int TickCount { get; set; }
        public KLineInfo PrevKLine { get; set; }
        public KLineInfo NextKLine { get; set; }
        public TwKLineData SkKLine { get; set; }
        public TwKLineData PrevSkKLine { get; set; }
        public TwKLineData NextSkKLine { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Tick> TickList { get; set; }
        /// <summary>
        /// K線的量
        /// </summary>
        public int Quantity { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }

        public KLineInfo()
        { }

        public KLineInfo(DateTime start)
        {
            this.StartTime = start;
            this.EndTime = start.AddMinutes(1).AddTicks(-1);
        }

        public KLineInfo(TwKLineData prevTwK, KLineInfo prevK, DateTime start)
        {
            if (TickCount == 0) //因為有可能一分內都沒有tick，所以建構這一分鐘K線的時候，先以上一分的Close作為基礎的開高低收四個價位
            {
                Open = prevK.Close;
                Close = prevK.Close;
                High = prevK.Close;
                Low = prevK.Close;
            }
            this.StartTime = start;
            this.EndTime = start.AddMinutes(1).AddTicks(-1);
            this.PrevSkKLine = prevTwK;
            this.PrevKLine = prevK;
            TickCount += 1;
        }


        public void AddTick(Tick tick)
        {
            if (tick.TickTime < this.StartTime || tick.TickTime > this.EndTime)
            { return; }

            if (TickCount == 0)
            {
                Open = tick.Close;
                Close = tick.Close;
                High = tick.Close;
                Low = tick.Close;
                Quantity = tick.Qty;
            }
            else
            {
                Quantity += tick.Qty;
            }
        }

        public void AddTwKLine(TwKLineData skTwKLine)
        {
            this.SkKLine = skTwKLine;
        }
    }

    public class Tick
    {
        public DateTime TickTime { get; set; }
        public decimal Close { get; set; }
        public int Qty { get; set; }
    }
}
