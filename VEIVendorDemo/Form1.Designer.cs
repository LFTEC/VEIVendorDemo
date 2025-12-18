namespace VEIVendorDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            dataGridView1 = new DataGridView();
            materialDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            materialDescDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            longTextDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            purLongTextDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            deletedDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            quantity = new DataGridViewTextBoxColumn();
            offset = new DataGridViewTextBoxColumn();
            lockQty = new DataGridViewTextBoxColumn();
            unitDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            matTypeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bindingSource1 = new BindingSource(components);
            tabPage2 = new TabPage();
            dataGridView2 = new DataGridView();
            msgIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            msgTypeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            sourceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            versionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            traceIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bodyDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            timestampDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            routingKeyDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bindingSource2 = new BindingSource(components);
            tabPage3 = new TabPage();
            richTextBox1 = new RichTextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            tableLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource2).BeginInit();
            tabPage3.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tabControl1, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(2, 2, 2, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(888, 577);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(2, 44);
            tabControl1.Margin = new Padding(2, 2, 2, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(884, 531);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(2, 2, 2, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(2, 2, 2, 2);
            tabPage1.Size = new Size(876, 501);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "库存";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { materialDataGridViewTextBoxColumn, materialDescDataGridViewTextBoxColumn, longTextDataGridViewTextBoxColumn, purLongTextDataGridViewTextBoxColumn, deletedDataGridViewCheckBoxColumn, quantity, offset, lockQty, unitDataGridViewTextBoxColumn, matTypeDataGridViewTextBoxColumn });
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(2, 2);
            dataGridView1.Margin = new Padding(2, 2, 2, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(872, 497);
            dataGridView1.TabIndex = 0;
            // 
            // materialDataGridViewTextBoxColumn
            // 
            materialDataGridViewTextBoxColumn.DataPropertyName = "material";
            materialDataGridViewTextBoxColumn.HeaderText = "物料";
            materialDataGridViewTextBoxColumn.MinimumWidth = 8;
            materialDataGridViewTextBoxColumn.Name = "materialDataGridViewTextBoxColumn";
            materialDataGridViewTextBoxColumn.ReadOnly = true;
            materialDataGridViewTextBoxColumn.Width = 150;
            // 
            // materialDescDataGridViewTextBoxColumn
            // 
            materialDescDataGridViewTextBoxColumn.DataPropertyName = "materialDesc";
            materialDescDataGridViewTextBoxColumn.HeaderText = "物料描述";
            materialDescDataGridViewTextBoxColumn.MinimumWidth = 8;
            materialDescDataGridViewTextBoxColumn.Name = "materialDescDataGridViewTextBoxColumn";
            materialDescDataGridViewTextBoxColumn.ReadOnly = true;
            materialDescDataGridViewTextBoxColumn.Width = 150;
            // 
            // longTextDataGridViewTextBoxColumn
            // 
            longTextDataGridViewTextBoxColumn.DataPropertyName = "longText";
            longTextDataGridViewTextBoxColumn.HeaderText = "长文本";
            longTextDataGridViewTextBoxColumn.MinimumWidth = 8;
            longTextDataGridViewTextBoxColumn.Name = "longTextDataGridViewTextBoxColumn";
            longTextDataGridViewTextBoxColumn.ReadOnly = true;
            longTextDataGridViewTextBoxColumn.Width = 150;
            // 
            // purLongTextDataGridViewTextBoxColumn
            // 
            purLongTextDataGridViewTextBoxColumn.DataPropertyName = "purLongText";
            purLongTextDataGridViewTextBoxColumn.HeaderText = "采购长文本";
            purLongTextDataGridViewTextBoxColumn.MinimumWidth = 8;
            purLongTextDataGridViewTextBoxColumn.Name = "purLongTextDataGridViewTextBoxColumn";
            purLongTextDataGridViewTextBoxColumn.ReadOnly = true;
            purLongTextDataGridViewTextBoxColumn.Width = 150;
            // 
            // deletedDataGridViewCheckBoxColumn
            // 
            deletedDataGridViewCheckBoxColumn.DataPropertyName = "deleted";
            deletedDataGridViewCheckBoxColumn.HeaderText = "已删除";
            deletedDataGridViewCheckBoxColumn.MinimumWidth = 8;
            deletedDataGridViewCheckBoxColumn.Name = "deletedDataGridViewCheckBoxColumn";
            deletedDataGridViewCheckBoxColumn.ReadOnly = true;
            deletedDataGridViewCheckBoxColumn.Width = 150;
            // 
            // quantity
            // 
            quantity.DataPropertyName = "quantity";
            quantity.HeaderText = "总库存";
            quantity.MinimumWidth = 8;
            quantity.Name = "quantity";
            quantity.Width = 150;
            // 
            // offset
            // 
            offset.DataPropertyName = "offset";
            offset.HeaderText = "差量";
            offset.MinimumWidth = 8;
            offset.Name = "offset";
            offset.Width = 150;
            // 
            // lockQty
            // 
            lockQty.DataPropertyName = "lockQty";
            lockQty.HeaderText = "锁定库存";
            lockQty.MinimumWidth = 8;
            lockQty.Name = "lockQty";
            lockQty.ReadOnly = true;
            lockQty.Width = 150;
            // 
            // unitDataGridViewTextBoxColumn
            // 
            unitDataGridViewTextBoxColumn.DataPropertyName = "unit";
            unitDataGridViewTextBoxColumn.HeaderText = "单位";
            unitDataGridViewTextBoxColumn.MinimumWidth = 8;
            unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            unitDataGridViewTextBoxColumn.ReadOnly = true;
            unitDataGridViewTextBoxColumn.Width = 150;
            // 
            // matTypeDataGridViewTextBoxColumn
            // 
            matTypeDataGridViewTextBoxColumn.DataPropertyName = "matType";
            matTypeDataGridViewTextBoxColumn.HeaderText = "物料类型";
            matTypeDataGridViewTextBoxColumn.MinimumWidth = 8;
            matTypeDataGridViewTextBoxColumn.Name = "matTypeDataGridViewTextBoxColumn";
            matTypeDataGridViewTextBoxColumn.ReadOnly = true;
            matTypeDataGridViewTextBoxColumn.Width = 150;
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(Stock);
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView2);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Margin = new Padding(2, 2, 2, 2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(2, 2, 2, 2);
            tabPage2.Size = new Size(876, 501);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "日志";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { msgIdDataGridViewTextBoxColumn, msgTypeDataGridViewTextBoxColumn, sourceDataGridViewTextBoxColumn, versionDataGridViewTextBoxColumn, traceIdDataGridViewTextBoxColumn, bodyDataGridViewTextBoxColumn, timestampDataGridViewTextBoxColumn, routingKeyDataGridViewTextBoxColumn });
            dataGridView2.DataSource = bindingSource2;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(2, 2);
            dataGridView2.Margin = new Padding(2, 2, 2, 2);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 62;
            dataGridView2.Size = new Size(872, 497);
            dataGridView2.TabIndex = 0;
            // 
            // msgIdDataGridViewTextBoxColumn
            // 
            msgIdDataGridViewTextBoxColumn.DataPropertyName = "msgId";
            msgIdDataGridViewTextBoxColumn.HeaderText = "msgId";
            msgIdDataGridViewTextBoxColumn.MinimumWidth = 8;
            msgIdDataGridViewTextBoxColumn.Name = "msgIdDataGridViewTextBoxColumn";
            msgIdDataGridViewTextBoxColumn.Width = 150;
            // 
            // msgTypeDataGridViewTextBoxColumn
            // 
            msgTypeDataGridViewTextBoxColumn.DataPropertyName = "msgType";
            msgTypeDataGridViewTextBoxColumn.HeaderText = "msgType";
            msgTypeDataGridViewTextBoxColumn.MinimumWidth = 8;
            msgTypeDataGridViewTextBoxColumn.Name = "msgTypeDataGridViewTextBoxColumn";
            msgTypeDataGridViewTextBoxColumn.Width = 150;
            // 
            // sourceDataGridViewTextBoxColumn
            // 
            sourceDataGridViewTextBoxColumn.DataPropertyName = "source";
            sourceDataGridViewTextBoxColumn.HeaderText = "source";
            sourceDataGridViewTextBoxColumn.MinimumWidth = 8;
            sourceDataGridViewTextBoxColumn.Name = "sourceDataGridViewTextBoxColumn";
            sourceDataGridViewTextBoxColumn.Width = 150;
            // 
            // versionDataGridViewTextBoxColumn
            // 
            versionDataGridViewTextBoxColumn.DataPropertyName = "version";
            versionDataGridViewTextBoxColumn.HeaderText = "version";
            versionDataGridViewTextBoxColumn.MinimumWidth = 8;
            versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            versionDataGridViewTextBoxColumn.Width = 150;
            // 
            // traceIdDataGridViewTextBoxColumn
            // 
            traceIdDataGridViewTextBoxColumn.DataPropertyName = "traceId";
            traceIdDataGridViewTextBoxColumn.HeaderText = "traceId";
            traceIdDataGridViewTextBoxColumn.MinimumWidth = 8;
            traceIdDataGridViewTextBoxColumn.Name = "traceIdDataGridViewTextBoxColumn";
            traceIdDataGridViewTextBoxColumn.Width = 150;
            // 
            // bodyDataGridViewTextBoxColumn
            // 
            bodyDataGridViewTextBoxColumn.DataPropertyName = "body";
            bodyDataGridViewTextBoxColumn.HeaderText = "body";
            bodyDataGridViewTextBoxColumn.MinimumWidth = 8;
            bodyDataGridViewTextBoxColumn.Name = "bodyDataGridViewTextBoxColumn";
            bodyDataGridViewTextBoxColumn.Width = 150;
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            timestampDataGridViewTextBoxColumn.DataPropertyName = "timestamp";
            timestampDataGridViewTextBoxColumn.HeaderText = "timestamp";
            timestampDataGridViewTextBoxColumn.MinimumWidth = 8;
            timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            timestampDataGridViewTextBoxColumn.Width = 150;
            // 
            // routingKeyDataGridViewTextBoxColumn
            // 
            routingKeyDataGridViewTextBoxColumn.DataPropertyName = "routingKey";
            routingKeyDataGridViewTextBoxColumn.HeaderText = "routingKey";
            routingKeyDataGridViewTextBoxColumn.MinimumWidth = 8;
            routingKeyDataGridViewTextBoxColumn.Name = "routingKeyDataGridViewTextBoxColumn";
            routingKeyDataGridViewTextBoxColumn.Width = 150;
            // 
            // bindingSource2
            // 
            bindingSource2.DataSource = typeof(Log);
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(richTextBox1);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Margin = new Padding(2, 2, 2, 2);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(876, 501);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "文本Log";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Margin = new Padding(2, 2, 2, 2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(876, 501);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(textBox1);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Controls.Add(button3);
            flowLayoutPanel1.Location = new Point(2, 2);
            flowLayoutPanel1.Margin = new Padding(2, 2, 2, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0, 7, 0, 0);
            flowLayoutPanel1.Size = new Size(398, 35);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(2, 12);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "绑定供应商";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(74, 9);
            textBox1.Margin = new Padding(2, 2, 2, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(97, 23);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(175, 9);
            button1.Margin = new Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new Size(71, 24);
            button1.TabIndex = 2;
            button1.Text = "绑定";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(250, 9);
            button2.Margin = new Padding(2, 2, 2, 2);
            button2.Name = "button2";
            button2.Size = new Size(71, 24);
            button2.TabIndex = 3;
            button2.Text = "上传库存";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(325, 9);
            button3.Margin = new Padding(2, 2, 2, 2);
            button3.Name = "button3";
            button3.Size = new Size(71, 24);
            button3.TabIndex = 4;
            button3.Text = "上传差量";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(888, 577);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource2).EndInit();
            tabPage3.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private BindingSource bindingSource1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private BindingSource bindingSource2;
        private TabPage tabPage3;
        private RichTextBox richTextBox1;
        private DataGridViewTextBoxColumn materialDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn materialDescDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn longTextDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn purLongTextDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn deletedDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn quantity;
        private DataGridViewTextBoxColumn offset;
        private DataGridViewTextBoxColumn lockQty;
        private DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn matTypeDataGridViewTextBoxColumn;
        private Button button2;
        private DataGridViewTextBoxColumn msgIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn msgTypeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sourceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn traceIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bodyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn routingKeyDataGridViewTextBoxColumn;
        private Button button3;
    }
}
