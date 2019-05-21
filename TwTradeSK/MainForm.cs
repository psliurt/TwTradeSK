using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapitalSkLib;
using CapitalSkLib.ModuleObject;
using log4net;
namespace TwTradeSK
{
    public partial class MainTradeForm : Form
    {

        private ILog _log = LogManager.GetLogger(typeof(MainTradeForm));
        private QuoteTickManager _quoteMgr { get; set; }

        SkFacade _lib = SkFacade.Instance(SkLibType.Quote);
        public MainTradeForm()
        {
            InitializeComponent();
        }

        private void MainTradeForm_Load(object sender, EventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            _quoteMgr = new QuoteTickManager();
            _lib.Initialize("L122852080", TxtPassCode.Text.Trim());
            _lib.OnTwTick += OnSkTwTick;
        }
        private void OnSkTwTick(TickInfo tick)
        {
            _quoteMgr.AddTwTick(tick);
        }

        private void BtnQuoteTick_Click(object sender, EventArgs e)
        {
            _lib.SubscribeTWTick("TX00");
        }
    }
}
