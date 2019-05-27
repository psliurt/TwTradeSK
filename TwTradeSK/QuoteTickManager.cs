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
        /// <summary>
        /// 今天的1分K陣列
        /// </summary>
        public List<KLineInfo> KLineSeries { get; set; }  
        /// <summary>
        /// 最近的一分K
        /// </summary>
        private KLineInfo _lastKLine { get; set; }
        /// <summary>
        /// 今天到目前為止的K線數目
        /// </summary>
        public int KLineCount { get; set; }

        public int TwKLineCount { get; set; }

        private DateTime _marketOpenTime { get; set; }
        

        public QuoteTickManager()
        {
            this.KLineSeries = new List<KLineInfo>();
            this.KLineCount = 0;
            this.TwKLineCount = 0;
            this._marketOpenTime = DateTime.Today.AddDays(-1).AddHours(14).AddMinutes(15);
        }

        public void AddSkKLine(TwKLineData skKLine)
        {
            DateTime dt = skKLine.KDate;
            dt = dt.AddHours(skKLine.KTime.Value.Hour).AddMinutes(skKLine.KTime.Value.Minute);//.AddSeconds(skKLine.KTime.Value.Second);
            KLineInfo kline = this.KLineSeries.Where(x => x.StartTime == dt).FirstOrDefault();
            if (kline != null)
            {
                kline.AddTwKLine(skKLine);
                this.TwKLineCount += 1;
            }
            else
            {
                //TODO:如果沒找到，需要做什麼處理嗎?
            }
        }

        public void AddTwTick(TickInfo skTick)
        {
            if (this._lastKLine == null) //系統內還沒有任何K線的情況，這種情況有兩種，第一種，系統開盤後才打開，第二種，系統開盤前打開
            {
                //開市時間為0845
                DateTime tmpTime = this._marketOpenTime;
                while (skTick.TickTime >= tmpTime)
                {
                    KLineInfo kline = new KLineInfo(tmpTime);
                    this.KLineSeries.Add(kline);                    
                    this._lastKLine = kline;
                    tmpTime = tmpTime.AddMinutes(1);
                    this.KLineCount += 1;
                }

                this._lastKLine.AddTick(new Tick
                {
                    Close = skTick.Close,
                    Qty = skTick.Qty,
                    TickTime = skTick.TickTime
                });                
            }
            else
            {
                if (skTick.TickTime > this._lastKLine.EndTime)
                {
                    KLineInfo lastK = new KLineInfo( skTick.TickTime);
                    lastK.AddTick(new Tick
                    {
                        Close = skTick.Close,
                        Qty = skTick.Qty,
                        TickTime = skTick.TickTime
                    });

                    this.KLineSeries.Add(lastK);
                    this._lastKLine = lastK;
                    this.KLineCount += 1;
                }
                else if (skTick.TickTime >= this._lastKLine.StartTime && skTick.TickTime <= this._lastKLine.EndTime)
                {
                    this._lastKLine.AddTick(new Tick
                    {
                        Close = skTick.Close,
                        Qty = skTick.Qty,
                        TickTime = skTick.TickTime
                    });
                }
                else //這個tick屬於過去的K線
                {
                    var historyKLine = this.KLineSeries.Where(x => skTick.TickTime >= x.StartTime && skTick.TickTime <= x.EndTime).FirstOrDefault();
                    if (historyKLine != null)
                    {
                        historyKLine.AddTick(new Tick
                        {
                            Close = skTick.Close,
                            Qty = skTick.Qty,
                            TickTime = skTick.TickTime
                        });
                    }
                }
            }
        }
    }

    /// <summary>
    /// K線資料
    /// </summary>
    public class KLineInfo
    {
        /// <summary>
        /// K線編號
        /// </summary>
        public int LineNo { get; set; }
        public int TickCount { get; set; }
        public KLineInfo PrevKLine { get; set; }
        public KLineInfo NextKLine { get; set; }
        public TwKLineData SkKLine { get; set; }
        //public TwKLineData PrevSkKLine { get; set; }
        //public TwKLineData NextSkKLine { get; set; }
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
            this.StartTime = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 0);
            this.EndTime = start.AddMinutes(1).AddTicks(-1);
            this.TickCount = 0;
            this.TickList = new List<Tick>();
        }

        public KLineInfo( KLineInfo prevK, DateTime start)
        {
            //if (TickCount == 0) //因為有可能一分內都沒有tick，所以建構這一分鐘K線的時候，先以上一分的Close作為基礎的開高低收四個價位
            //{
            //    Open = prevK.Close;
            //    Close = prevK.Close;
            //    High = prevK.Close;
            //    Low = prevK.Close;
            //}
            this.StartTime = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 0);
            this.EndTime = start.AddMinutes(1).AddTicks(-1);
            //this.PrevSkKLine = prevTwK;
            this.PrevKLine = prevK;
            TickCount = 0;
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
                if (tick.Close > this.High)
                {
                    this.High = tick.Close;
                }

                if (tick.Close < this.Low)
                {
                    this.Low = tick.Close;
                }
                this.Close = tick.Close;
                Quantity += tick.Qty;
            }

            this.TickList.Add(tick);
            this.TickCount += 1;
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
