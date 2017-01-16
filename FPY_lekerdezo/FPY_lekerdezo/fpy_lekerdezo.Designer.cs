namespace FPY_lekerdezo
{
    partial class fpy_lekerdezo
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
            this.muvelet_checklist = new System.Windows.Forms.CheckedListBox();
            this.lekerdez_but = new System.Windows.Forms.Button();
            this.datepicker_from = new System.Windows.Forms.DateTimePicker();
            this.datepicker_to = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tipus_combobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.eredmenyek_seged_datagridview = new System.Windows.Forms.DataGridView();
            this.result_dgv = new System.Windows.Forms.DataGridView();
            this.final_dgv = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Workstation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operation_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operation_Msg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Limit_Min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Limit_Max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.valos_dgv = new System.Windows.Forms.DataGridView();
            this.valos_seged_dgv = new System.Windows.Forms.DataGridView();
            this.IDs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Workstations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OP_Msg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Results = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Values = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lim_Min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lim_Max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operators = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.eredmenyek_seged_datagridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.result_dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.final_dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valos_dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valos_seged_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // muvelet_checklist
            // 
            this.muvelet_checklist.FormattingEnabled = true;
            this.muvelet_checklist.Location = new System.Drawing.Point(12, 51);
            this.muvelet_checklist.Name = "muvelet_checklist";
            this.muvelet_checklist.Size = new System.Drawing.Size(349, 169);
            this.muvelet_checklist.TabIndex = 0;
            // 
            // lekerdez_but
            // 
            this.lekerdez_but.Location = new System.Drawing.Point(12, 226);
            this.lekerdez_but.Name = "lekerdez_but";
            this.lekerdez_but.Size = new System.Drawing.Size(75, 23);
            this.lekerdez_but.TabIndex = 1;
            this.lekerdez_but.Text = "Lekérdez";
            this.lekerdez_but.UseVisualStyleBackColor = true;
            this.lekerdez_but.Click += new System.EventHandler(this.lekerdez_but_Click);
            // 
            // datepicker_from
            // 
            this.datepicker_from.Location = new System.Drawing.Point(72, 12);
            this.datepicker_from.Name = "datepicker_from";
            this.datepicker_from.Size = new System.Drawing.Size(168, 20);
            this.datepicker_from.TabIndex = 2;
            this.datepicker_from.Value = new System.DateTime(2017, 1, 13, 8, 20, 53, 0);
            // 
            // datepicker_to
            // 
            this.datepicker_to.Location = new System.Drawing.Point(262, 12);
            this.datepicker_to.Name = "datepicker_to";
            this.datepicker_to.Size = new System.Drawing.Size(168, 20);
            this.datepicker_to.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Dátum:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "-";
            // 
            // tipus_combobox
            // 
            this.tipus_combobox.FormattingEnabled = true;
            this.tipus_combobox.Location = new System.Drawing.Point(480, 12);
            this.tipus_combobox.Name = "tipus_combobox";
            this.tipus_combobox.Size = new System.Drawing.Size(114, 21);
            this.tipus_combobox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Típus:";
            // 
            // eredmenyek_seged_datagridview
            // 
            this.eredmenyek_seged_datagridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eredmenyek_seged_datagridview.Location = new System.Drawing.Point(93, 231);
            this.eredmenyek_seged_datagridview.Name = "eredmenyek_seged_datagridview";
            this.eredmenyek_seged_datagridview.Size = new System.Drawing.Size(10, 10);
            this.eredmenyek_seged_datagridview.TabIndex = 10;
            this.eredmenyek_seged_datagridview.Visible = false;
            // 
            // result_dgv
            // 
            this.result_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.result_dgv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.result_dgv.Location = new System.Drawing.Point(0, 255);
            this.result_dgv.Name = "result_dgv";
            this.result_dgv.Size = new System.Drawing.Size(606, 218);
            this.result_dgv.TabIndex = 11;
            // 
            // final_dgv
            // 
            this.final_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.final_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Workstation,
            this.Operation_ID,
            this.Operation_Msg,
            this.Result,
            this.Value,
            this.Limit_Min,
            this.Limit_Max,
            this.Date,
            this.Operator,
            this.Note});
            this.final_dgv.Location = new System.Drawing.Point(93, 239);
            this.final_dgv.Name = "final_dgv";
            this.final_dgv.Size = new System.Drawing.Size(10, 10);
            this.final_dgv.TabIndex = 12;
            this.final_dgv.Visible = false;
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            // 
            // Workstation
            // 
            this.Workstation.Frozen = true;
            this.Workstation.HeaderText = "Workstation";
            this.Workstation.Name = "Workstation";
            // 
            // Operation_ID
            // 
            this.Operation_ID.Frozen = true;
            this.Operation_ID.HeaderText = "Operation_ID";
            this.Operation_ID.Name = "Operation_ID";
            // 
            // Operation_Msg
            // 
            this.Operation_Msg.Frozen = true;
            this.Operation_Msg.HeaderText = "Operation_Msg";
            this.Operation_Msg.Name = "Operation_Msg";
            // 
            // Result
            // 
            this.Result.Frozen = true;
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            // 
            // Value
            // 
            this.Value.Frozen = true;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // Limit_Min
            // 
            this.Limit_Min.Frozen = true;
            this.Limit_Min.HeaderText = "Limit_Min";
            this.Limit_Min.Name = "Limit_Min";
            // 
            // Limit_Max
            // 
            this.Limit_Max.Frozen = true;
            this.Limit_Max.HeaderText = "Limit_Max";
            this.Limit_Max.Name = "Limit_Max";
            // 
            // Date
            // 
            this.Date.Frozen = true;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // Operator
            // 
            this.Operator.Frozen = true;
            this.Operator.HeaderText = "Operator";
            this.Operator.Name = "Operator";
            // 
            // Note
            // 
            this.Note.Frozen = true;
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(502, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 39);
            this.button1.TabIndex = 13;
            this.button1.Text = "Egygombos lekérdezés";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // valos_dgv
            // 
            this.valos_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.valos_dgv.Location = new System.Drawing.Point(109, 231);
            this.valos_dgv.Name = "valos_dgv";
            this.valos_dgv.Size = new System.Drawing.Size(10, 10);
            this.valos_dgv.TabIndex = 14;
            this.valos_dgv.Visible = false;
            // 
            // valos_seged_dgv
            // 
            this.valos_seged_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.valos_seged_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDs,
            this.Workstations,
            this.OP_ID,
            this.OP_Msg,
            this.Results,
            this.Values,
            this.Lim_Min,
            this.Lim_Max,
            this.Dates,
            this.Operators,
            this.Notes});
            this.valos_seged_dgv.Location = new System.Drawing.Point(109, 239);
            this.valos_seged_dgv.Name = "valos_seged_dgv";
            this.valos_seged_dgv.Size = new System.Drawing.Size(10, 10);
            this.valos_seged_dgv.TabIndex = 15;
            this.valos_seged_dgv.Visible = false;
            // 
            // IDs
            // 
            this.IDs.Frozen = true;
            this.IDs.HeaderText = "IDs";
            this.IDs.Name = "IDs";
            // 
            // Workstations
            // 
            this.Workstations.Frozen = true;
            this.Workstations.HeaderText = "Workstations";
            this.Workstations.Name = "Workstations";
            // 
            // OP_ID
            // 
            this.OP_ID.Frozen = true;
            this.OP_ID.HeaderText = "OP_ID";
            this.OP_ID.Name = "OP_ID";
            // 
            // OP_Msg
            // 
            this.OP_Msg.Frozen = true;
            this.OP_Msg.HeaderText = "OP_Msg";
            this.OP_Msg.Name = "OP_Msg";
            // 
            // Results
            // 
            this.Results.Frozen = true;
            this.Results.HeaderText = "Results";
            this.Results.Name = "Results";
            // 
            // Values
            // 
            this.Values.Frozen = true;
            this.Values.HeaderText = "Values";
            this.Values.Name = "Values";
            // 
            // Lim_Min
            // 
            this.Lim_Min.Frozen = true;
            this.Lim_Min.HeaderText = "Lim_Min";
            this.Lim_Min.Name = "Lim_Min";
            // 
            // Lim_Max
            // 
            this.Lim_Max.Frozen = true;
            this.Lim_Max.HeaderText = "Lim_Max";
            this.Lim_Max.Name = "Lim_Max";
            // 
            // Dates
            // 
            this.Dates.Frozen = true;
            this.Dates.HeaderText = "Dates";
            this.Dates.Name = "Dates";
            // 
            // Operators
            // 
            this.Operators.Frozen = true;
            this.Operators.HeaderText = "Operators";
            this.Operators.Name = "Operators";
            // 
            // Notes
            // 
            this.Notes.Frozen = true;
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            // 
            // fpy_lekerdezo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 473);
            this.Controls.Add(this.valos_seged_dgv);
            this.Controls.Add(this.valos_dgv);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.final_dgv);
            this.Controls.Add(this.result_dgv);
            this.Controls.Add(this.eredmenyek_seged_datagridview);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tipus_combobox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datepicker_to);
            this.Controls.Add(this.datepicker_from);
            this.Controls.Add(this.lekerdez_but);
            this.Controls.Add(this.muvelet_checklist);
            this.Name = "fpy_lekerdezo";
            this.Text = "FPY Lekérdező";
            this.Load += new System.EventHandler(this.fpy_lekerdezo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eredmenyek_seged_datagridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.result_dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.final_dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valos_dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valos_seged_dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox muvelet_checklist;
        private System.Windows.Forms.Button lekerdez_but;
        private System.Windows.Forms.DateTimePicker datepicker_from;
        private System.Windows.Forms.DateTimePicker datepicker_to;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tipus_combobox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView eredmenyek_seged_datagridview;
        private System.Windows.Forms.DataGridView result_dgv;
        private System.Windows.Forms.DataGridView final_dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Workstation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operation_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operation_Msg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Limit_Min;
        private System.Windows.Forms.DataGridViewTextBoxColumn Limit_Max;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView valos_dgv;
        private System.Windows.Forms.DataGridView valos_seged_dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Workstations;
        private System.Windows.Forms.DataGridViewTextBoxColumn OP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OP_Msg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Results;
        private System.Windows.Forms.DataGridViewTextBoxColumn Values;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lim_Min;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lim_Max;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dates;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operators;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
    }
}

