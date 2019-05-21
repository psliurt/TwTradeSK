namespace TwTradeSK
{
    partial class MainTradeForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtPassCode = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnQuoteTick = new System.Windows.Forms.Button();
            this.BtnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtPassCode
            // 
            this.TxtPassCode.Location = new System.Drawing.Point(12, 12);
            this.TxtPassCode.Name = "TxtPassCode";
            this.TxtPassCode.PasswordChar = '#';
            this.TxtPassCode.Size = new System.Drawing.Size(100, 22);
            this.TxtPassCode.TabIndex = 0;
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(118, 10);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(75, 23);
            this.BtnLogin.TabIndex = 1;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnQuoteTick
            // 
            this.BtnQuoteTick.Location = new System.Drawing.Point(211, 10);
            this.BtnQuoteTick.Name = "BtnQuoteTick";
            this.BtnQuoteTick.Size = new System.Drawing.Size(75, 23);
            this.BtnQuoteTick.TabIndex = 2;
            this.BtnQuoteTick.Text = "Quote";
            this.BtnQuoteTick.UseVisualStyleBackColor = true;
            this.BtnQuoteTick.Click += new System.EventHandler(this.BtnQuoteTick_Click);
            // 
            // BtnTest
            // 
            this.BtnTest.Location = new System.Drawing.Point(309, 10);
            this.BtnTest.Name = "BtnTest";
            this.BtnTest.Size = new System.Drawing.Size(75, 23);
            this.BtnTest.TabIndex = 3;
            this.BtnTest.Text = "Test";
            this.BtnTest.UseVisualStyleBackColor = true;
            this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // MainTradeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnTest);
            this.Controls.Add(this.BtnQuoteTick);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.TxtPassCode);
            this.Name = "MainTradeForm";
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainTradeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPassCode;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button BtnQuoteTick;
        private System.Windows.Forms.Button BtnTest;
    }
}

