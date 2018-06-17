namespace UDMF_VertexTools
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFile = new System.Windows.Forms.Button();
            this.OpenFile = new System.Windows.Forms.Button();
            this.CeilingMult = new System.Windows.Forms.NumericUpDown();
            this.labelDistance = new System.Windows.Forms.Label();
            this.ServicedVerticesLabel = new System.Windows.Forms.Label();
            this.FloorOffs = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.FloorMult = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.CeilingOffs = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.RB_NoChange = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.RB_Swap = new System.Windows.Forms.RadioButton();
            this.RB_Copy_FtC = new System.Windows.Forms.RadioButton();
            this.RB_Copy_CtF = new System.Windows.Forms.RadioButton();
            this.SaveFileDIalogV = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CeilingMult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorOffs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorMult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CeilingOffs)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawFPS = true;
            this.openGLControl1.FrameRate = 60;
            resources.ApplyResources(this.openGLControl1, "openGLControl1");
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL3_0;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl1_OpenGLDraw);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // SaveFile
            // 
            this.SaveFile.Image = global::UDMF_VertexTools.Properties.Resources.SaveStatusBar1_16x;
            resources.ApplyResources(this.SaveFile, "SaveFile");
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.UseVisualStyleBackColor = true;
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.Image = global::UDMF_VertexTools.Properties.Resources.Open_16x;
            resources.ApplyResources(this.OpenFile, "OpenFile");
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // CeilingMult
            // 
            resources.ApplyResources(this.CeilingMult, "CeilingMult");
            this.CeilingMult.Maximum = new decimal(new int[] {
            25000,
            0,
            0,
            0});
            this.CeilingMult.Minimum = new decimal(new int[] {
            25000,
            0,
            0,
            -2147483648});
            this.CeilingMult.Name = "CeilingMult";
            this.CeilingMult.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelDistance
            // 
            resources.ApplyResources(this.labelDistance, "labelDistance");
            this.labelDistance.Name = "labelDistance";
            // 
            // ServicedVerticesLabel
            // 
            resources.ApplyResources(this.ServicedVerticesLabel, "ServicedVerticesLabel");
            this.ServicedVerticesLabel.Name = "ServicedVerticesLabel";
            // 
            // FloorOffs
            // 
            resources.ApplyResources(this.FloorOffs, "FloorOffs");
            this.FloorOffs.Maximum = new decimal(new int[] {
            25000,
            0,
            0,
            0});
            this.FloorOffs.Minimum = new decimal(new int[] {
            25000,
            0,
            0,
            -2147483648});
            this.FloorOffs.Name = "FloorOffs";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FloorMult
            // 
            resources.ApplyResources(this.FloorMult, "FloorMult");
            this.FloorMult.Maximum = new decimal(new int[] {
            25000,
            0,
            0,
            0});
            this.FloorMult.Minimum = new decimal(new int[] {
            25000,
            0,
            0,
            -2147483648});
            this.FloorMult.Name = "FloorMult";
            this.FloorMult.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // CeilingOffs
            // 
            resources.ApplyResources(this.CeilingOffs, "CeilingOffs");
            this.CeilingOffs.Maximum = new decimal(new int[] {
            25000,
            0,
            0,
            0});
            this.CeilingOffs.Minimum = new decimal(new int[] {
            25000,
            0,
            0,
            -2147483648});
            this.CeilingOffs.Name = "CeilingOffs";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // RB_NoChange
            // 
            resources.ApplyResources(this.RB_NoChange, "RB_NoChange");
            this.RB_NoChange.Name = "RB_NoChange";
            this.RB_NoChange.TabStop = true;
            this.RB_NoChange.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // RB_Swap
            // 
            resources.ApplyResources(this.RB_Swap, "RB_Swap");
            this.RB_Swap.Name = "RB_Swap";
            this.RB_Swap.TabStop = true;
            this.RB_Swap.UseVisualStyleBackColor = true;
            // 
            // RB_Copy_FtC
            // 
            resources.ApplyResources(this.RB_Copy_FtC, "RB_Copy_FtC");
            this.RB_Copy_FtC.Name = "RB_Copy_FtC";
            this.RB_Copy_FtC.TabStop = true;
            this.RB_Copy_FtC.UseVisualStyleBackColor = true;
            // 
            // RB_Copy_CtF
            // 
            resources.ApplyResources(this.RB_Copy_CtF, "RB_Copy_CtF");
            this.RB_Copy_CtF.Name = "RB_Copy_CtF";
            this.RB_Copy_CtF.TabStop = true;
            this.RB_Copy_CtF.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RB_Copy_CtF);
            this.Controls.Add(this.RB_Copy_FtC);
            this.Controls.Add(this.RB_Swap);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RB_NoChange);
            this.Controls.Add(this.CeilingOffs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FloorMult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FloorOffs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServicedVerticesLabel);
            this.Controls.Add(this.CeilingMult);
            this.Controls.Add(this.labelDistance);
            this.Controls.Add(this.SaveFile);
            this.Controls.Add(this.OpenFile);
            this.Controls.Add(this.openGLControl1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CeilingMult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorOffs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloorMult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CeilingOffs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.Button SaveFile;
        private System.Windows.Forms.Label labelDistance;
        private System.Windows.Forms.Label ServicedVerticesLabel;
        private System.Windows.Forms.NumericUpDown FloorOffs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown CeilingMult;
        private System.Windows.Forms.NumericUpDown FloorMult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown CeilingOffs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton RB_NoChange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton RB_Swap;
        private System.Windows.Forms.RadioButton RB_Copy_FtC;
        private System.Windows.Forms.RadioButton RB_Copy_CtF;
        private System.Windows.Forms.SaveFileDialog SaveFileDIalogV;
    }
}

