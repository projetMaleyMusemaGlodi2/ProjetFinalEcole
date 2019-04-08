namespace Ecole.Formulaire
{
    partial class FormPalmaress
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPalmaress));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.txtcomboAnnee = new System.Windows.Forms.TextBox();
            this.cmbAnnee = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GridStatistique = new System.Windows.Forms.DataGridView();
            this.classeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codesectionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeoptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anneeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreInscriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewStatistiqueInscriptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetChatBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetChat = new Ecole.report.DataSetChat();
            this.viewStatistiqueInscriptionTableAdapter = new Ecole.report.DataSetChatTableAdapters.viewStatistiqueInscriptionTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridStatistique)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewStatistiqueInscriptionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetChatBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetChat)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chart1);
            this.groupBox1.Controls.Add(this.simpleButton2);
            this.groupBox1.Controls.Add(this.txtcomboAnnee);
            this.groupBox1.Controls.Add(this.cmbAnnee);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 487);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "POUR LES INSCRIPTION";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(22, 19);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Nombre";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(414, 296);
            this.chart1.TabIndex = 14;
            this.chart1.Text = "chart1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton2.Location = new System.Drawing.Point(51, 411);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(179, 55);
            this.simpleButton2.TabIndex = 13;
            this.simpleButton2.Text = "Chargerment";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // txtcomboAnnee
            // 
            this.txtcomboAnnee.Enabled = false;
            this.txtcomboAnnee.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcomboAnnee.Location = new System.Drawing.Point(329, 353);
            this.txtcomboAnnee.Name = "txtcomboAnnee";
            this.txtcomboAnnee.Size = new System.Drawing.Size(92, 26);
            this.txtcomboAnnee.TabIndex = 12;
            // 
            // cmbAnnee
            // 
            this.cmbAnnee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnnee.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAnnee.FormattingEnabled = true;
            this.cmbAnnee.Location = new System.Drawing.Point(117, 353);
            this.cmbAnnee.Name = "cmbAnnee";
            this.cmbAnnee.Size = new System.Drawing.Size(206, 26);
            this.cmbAnnee.TabIndex = 11;
            this.cmbAnnee.SelectedIndexChanged += new System.EventHandler(this.cmbAnnee_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Annee :";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton1.Location = new System.Drawing.Point(242, 411);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(179, 55);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Statistique";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.GridStatistique);
            this.groupBox2.Location = new System.Drawing.Point(488, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(578, 487);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // GridStatistique
            // 
            this.GridStatistique.AutoGenerateColumns = false;
            this.GridStatistique.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridStatistique.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.classeDataGridViewTextBoxColumn,
            this.codesectionDataGridViewTextBoxColumn,
            this.codeoptionDataGridViewTextBoxColumn,
            this.anneeDataGridViewTextBoxColumn,
            this.nombreInscriptionDataGridViewTextBoxColumn});
            this.GridStatistique.DataSource = this.viewStatistiqueInscriptionBindingSource;
            this.GridStatistique.Location = new System.Drawing.Point(14, 48);
            this.GridStatistique.Name = "GridStatistique";
            this.GridStatistique.Size = new System.Drawing.Size(549, 418);
            this.GridStatistique.TabIndex = 0;
            // 
            // classeDataGridViewTextBoxColumn
            // 
            this.classeDataGridViewTextBoxColumn.DataPropertyName = "Classe";
            this.classeDataGridViewTextBoxColumn.HeaderText = "Classe";
            this.classeDataGridViewTextBoxColumn.Name = "classeDataGridViewTextBoxColumn";
            // 
            // codesectionDataGridViewTextBoxColumn
            // 
            this.codesectionDataGridViewTextBoxColumn.DataPropertyName = "codesection";
            this.codesectionDataGridViewTextBoxColumn.HeaderText = "codesection";
            this.codesectionDataGridViewTextBoxColumn.Name = "codesectionDataGridViewTextBoxColumn";
            // 
            // codeoptionDataGridViewTextBoxColumn
            // 
            this.codeoptionDataGridViewTextBoxColumn.DataPropertyName = "codeoption";
            this.codeoptionDataGridViewTextBoxColumn.HeaderText = "codeoption";
            this.codeoptionDataGridViewTextBoxColumn.Name = "codeoptionDataGridViewTextBoxColumn";
            // 
            // anneeDataGridViewTextBoxColumn
            // 
            this.anneeDataGridViewTextBoxColumn.DataPropertyName = "Annee";
            this.anneeDataGridViewTextBoxColumn.HeaderText = "Annee";
            this.anneeDataGridViewTextBoxColumn.Name = "anneeDataGridViewTextBoxColumn";
            // 
            // nombreInscriptionDataGridViewTextBoxColumn
            // 
            this.nombreInscriptionDataGridViewTextBoxColumn.DataPropertyName = "NombreInscription";
            this.nombreInscriptionDataGridViewTextBoxColumn.HeaderText = "NombreInscription";
            this.nombreInscriptionDataGridViewTextBoxColumn.Name = "nombreInscriptionDataGridViewTextBoxColumn";
            // 
            // viewStatistiqueInscriptionBindingSource
            // 
            this.viewStatistiqueInscriptionBindingSource.DataMember = "viewStatistiqueInscription";
            this.viewStatistiqueInscriptionBindingSource.DataSource = this.dataSetChatBindingSource;
            // 
            // dataSetChatBindingSource
            // 
            this.dataSetChatBindingSource.DataSource = this.dataSetChat;
            this.dataSetChatBindingSource.Position = 0;
            // 
            // dataSetChat
            // 
            this.dataSetChat.DataSetName = "DataSetChat";
            this.dataSetChat.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // viewStatistiqueInscriptionTableAdapter
            // 
            this.viewStatistiqueInscriptionTableAdapter.ClearBeforeFill = true;
            // 
            // FormPalmaress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 520);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormPalmaress";
            this.Text = "FormPalmaress";
            this.Load += new System.EventHandler(this.FormPalmaress_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridStatistique)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewStatistiqueInscriptionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetChatBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetChat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.TextBox txtcomboAnnee;
        private System.Windows.Forms.ComboBox cmbAnnee;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView GridStatistique;
        private System.Windows.Forms.BindingSource dataSetChatBindingSource;
        private report.DataSetChat dataSetChat;
        private System.Windows.Forms.BindingSource viewStatistiqueInscriptionBindingSource;
        private report.DataSetChatTableAdapters.viewStatistiqueInscriptionTableAdapter viewStatistiqueInscriptionTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn classeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codesectionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeoptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn anneeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreInscriptionDataGridViewTextBoxColumn;
    }
}