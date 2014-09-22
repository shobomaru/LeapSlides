namespace LeapSlides
{
    partial class RibbonDesign : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// デザイナー変数が必要です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonDesign()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.groupLeapSlides = this.Factory.CreateRibbonGroup();
            this.toggleButtonOnOff = this.Factory.CreateRibbonToggleButton();
            this.tab1.SuspendLayout();
            this.groupLeapSlides.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.groupLeapSlides);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // groupLeapSlides
            // 
            this.groupLeapSlides.Items.Add(this.toggleButtonOnOff);
            this.groupLeapSlides.Label = "LeapSlides";
            this.groupLeapSlides.Name = "groupLeapSlides";
            // 
            // toggleButtonOnOff
            // 
            this.toggleButtonOnOff.Description = "Power";
            this.toggleButtonOnOff.Label = "On / Off";
            this.toggleButtonOnOff.Name = "toggleButtonOnOff";
            this.toggleButtonOnOff.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonOnOff_Click);
            // 
            // RibbonDesign
            // 
            this.Name = "RibbonDesign";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonDesign_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.groupLeapSlides.ResumeLayout(false);
            this.groupLeapSlides.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupLeapSlides;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonOnOff;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonDesign RibbonDesign
        {
            get { return this.GetRibbon<RibbonDesign>(); }
        }
    }
}
