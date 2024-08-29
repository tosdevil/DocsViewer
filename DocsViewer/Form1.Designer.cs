namespace DocsViewer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_add_doc = new System.Windows.Forms.Button();
            this.button_edit_doc = new System.Windows.Forms.Button();
            this.button_del_doc = new System.Windows.Forms.Button();
            this.button_del_spec = new System.Windows.Forms.Button();
            this.button_edit_spec = new System.Windows.Forms.Button();
            this.button_add_spec = new System.Windows.Forms.Button();
            this.textBox_doc_number = new System.Windows.Forms.TextBox();
            this.label_doc_number = new System.Windows.Forms.Label();
            this.label_doc_date = new System.Windows.Forms.Label();
            this.dateTimePicker_doc = new System.Windows.Forms.DateTimePicker();
            this.textBox_doc_note = new System.Windows.Forms.TextBox();
            this.label_doc_note = new System.Windows.Forms.Label();
            this.button_doc_edit_proceed = new System.Windows.Forms.Button();
            this.label_spec_name = new System.Windows.Forms.Label();
            this.textBox_spec_name = new System.Windows.Forms.TextBox();
            this.textBox_spec_amount = new System.Windows.Forms.TextBox();
            this.label_spec_amount = new System.Windows.Forms.Label();
            this.button_doc_add_proceed = new System.Windows.Forms.Button();
            this.listBox_docs = new System.Windows.Forms.ListBox();
            this.listBox_specs = new System.Windows.Forms.ListBox();
            this.label_doc_amount = new System.Windows.Forms.Label();
            this.button_specs_proceed = new System.Windows.Forms.Button();
            this.button_spec_add_proceed = new System.Windows.Forms.Button();
            this.button_refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Docs:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(397, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Specifications:";
            // 
            // button_add_doc
            // 
            this.button_add_doc.Location = new System.Drawing.Point(108, 281);
            this.button_add_doc.Name = "button_add_doc";
            this.button_add_doc.Size = new System.Drawing.Size(58, 27);
            this.button_add_doc.TabIndex = 4;
            this.button_add_doc.Text = "Add";
            this.button_add_doc.UseVisualStyleBackColor = true;
            this.button_add_doc.Click += new System.EventHandler(this.button_add_doc_Click);
            // 
            // button_edit_doc
            // 
            this.button_edit_doc.Location = new System.Drawing.Point(172, 281);
            this.button_edit_doc.Name = "button_edit_doc";
            this.button_edit_doc.Size = new System.Drawing.Size(58, 27);
            this.button_edit_doc.TabIndex = 5;
            this.button_edit_doc.Text = "Edit";
            this.button_edit_doc.UseVisualStyleBackColor = true;
            this.button_edit_doc.Click += new System.EventHandler(this.button_edit_doc_Click);
            // 
            // button_del_doc
            // 
            this.button_del_doc.Location = new System.Drawing.Point(236, 281);
            this.button_del_doc.Name = "button_del_doc";
            this.button_del_doc.Size = new System.Drawing.Size(58, 27);
            this.button_del_doc.TabIndex = 6;
            this.button_del_doc.Text = "Delete";
            this.button_del_doc.UseVisualStyleBackColor = true;
            this.button_del_doc.Click += new System.EventHandler(this.button_del_doc_Click);
            // 
            // button_del_spec
            // 
            this.button_del_spec.Location = new System.Drawing.Point(602, 281);
            this.button_del_spec.Name = "button_del_spec";
            this.button_del_spec.Size = new System.Drawing.Size(58, 27);
            this.button_del_spec.TabIndex = 9;
            this.button_del_spec.Text = "Delete";
            this.button_del_spec.UseVisualStyleBackColor = true;
            this.button_del_spec.Click += new System.EventHandler(this.button_del_spec_Click);
            // 
            // button_edit_spec
            // 
            this.button_edit_spec.Location = new System.Drawing.Point(538, 281);
            this.button_edit_spec.Name = "button_edit_spec";
            this.button_edit_spec.Size = new System.Drawing.Size(58, 27);
            this.button_edit_spec.TabIndex = 8;
            this.button_edit_spec.Text = "Edit";
            this.button_edit_spec.UseVisualStyleBackColor = true;
            this.button_edit_spec.Click += new System.EventHandler(this.button_edit_spec_Click);
            // 
            // button_add_spec
            // 
            this.button_add_spec.Location = new System.Drawing.Point(474, 281);
            this.button_add_spec.Name = "button_add_spec";
            this.button_add_spec.Size = new System.Drawing.Size(58, 27);
            this.button_add_spec.TabIndex = 7;
            this.button_add_spec.Text = "Add";
            this.button_add_spec.UseVisualStyleBackColor = true;
            this.button_add_spec.Click += new System.EventHandler(this.button_add_spec_Click);
            // 
            // textBox_doc_number
            // 
            this.textBox_doc_number.Location = new System.Drawing.Point(91, 312);
            this.textBox_doc_number.Name = "textBox_doc_number";
            this.textBox_doc_number.Size = new System.Drawing.Size(283, 20);
            this.textBox_doc_number.TabIndex = 13;
            this.textBox_doc_number.Visible = false;
            this.textBox_doc_number.TextChanged += new System.EventHandler(this.textBox_doc_number_TextChanged);
            // 
            // label_doc_number
            // 
            this.label_doc_number.AutoSize = true;
            this.label_doc_number.Location = new System.Drawing.Point(40, 315);
            this.label_doc_number.Name = "label_doc_number";
            this.label_doc_number.Size = new System.Drawing.Size(47, 13);
            this.label_doc_number.TabIndex = 12;
            this.label_doc_number.Text = "Number:";
            // 
            // label_doc_date
            // 
            this.label_doc_date.AutoSize = true;
            this.label_doc_date.Location = new System.Drawing.Point(40, 341);
            this.label_doc_date.Name = "label_doc_date";
            this.label_doc_date.Size = new System.Drawing.Size(33, 13);
            this.label_doc_date.TabIndex = 14;
            this.label_doc_date.Text = "Date:";
            // 
            // dateTimePicker_doc
            // 
            this.dateTimePicker_doc.Location = new System.Drawing.Point(91, 338);
            this.dateTimePicker_doc.Name = "dateTimePicker_doc";
            this.dateTimePicker_doc.Size = new System.Drawing.Size(283, 20);
            this.dateTimePicker_doc.TabIndex = 16;
            this.dateTimePicker_doc.Visible = false;
            // 
            // textBox_doc_note
            // 
            this.textBox_doc_note.Location = new System.Drawing.Point(91, 364);
            this.textBox_doc_note.Name = "textBox_doc_note";
            this.textBox_doc_note.Size = new System.Drawing.Size(283, 20);
            this.textBox_doc_note.TabIndex = 18;
            this.textBox_doc_note.Visible = false;
            // 
            // label_doc_note
            // 
            this.label_doc_note.AutoSize = true;
            this.label_doc_note.Location = new System.Drawing.Point(40, 367);
            this.label_doc_note.Name = "label_doc_note";
            this.label_doc_note.Size = new System.Drawing.Size(33, 13);
            this.label_doc_note.TabIndex = 17;
            this.label_doc_note.Text = "Note:";
            // 
            // button_doc_edit_proceed
            // 
            this.button_doc_edit_proceed.Location = new System.Drawing.Point(172, 390);
            this.button_doc_edit_proceed.Name = "button_doc_edit_proceed";
            this.button_doc_edit_proceed.Size = new System.Drawing.Size(58, 26);
            this.button_doc_edit_proceed.TabIndex = 19;
            this.button_doc_edit_proceed.Text = "Proceed";
            this.button_doc_edit_proceed.UseVisualStyleBackColor = true;
            this.button_doc_edit_proceed.Visible = false;
            this.button_doc_edit_proceed.Click += new System.EventHandler(this.button_doc_edit_proceed_Click);
            // 
            // label_spec_name
            // 
            this.label_spec_name.AutoSize = true;
            this.label_spec_name.Location = new System.Drawing.Point(397, 318);
            this.label_spec_name.Name = "label_spec_name";
            this.label_spec_name.Size = new System.Drawing.Size(38, 13);
            this.label_spec_name.TabIndex = 10;
            this.label_spec_name.Text = "Name:";
            // 
            // textBox_spec_name
            // 
            this.textBox_spec_name.Location = new System.Drawing.Point(448, 315);
            this.textBox_spec_name.Name = "textBox_spec_name";
            this.textBox_spec_name.Size = new System.Drawing.Size(283, 20);
            this.textBox_spec_name.TabIndex = 11;
            this.textBox_spec_name.Visible = false;
            // 
            // textBox_spec_amount
            // 
            this.textBox_spec_amount.Location = new System.Drawing.Point(448, 338);
            this.textBox_spec_amount.Name = "textBox_spec_amount";
            this.textBox_spec_amount.Size = new System.Drawing.Size(283, 20);
            this.textBox_spec_amount.TabIndex = 21;
            this.textBox_spec_amount.Visible = false;
            // 
            // label_spec_amount
            // 
            this.label_spec_amount.AutoSize = true;
            this.label_spec_amount.Location = new System.Drawing.Point(397, 341);
            this.label_spec_amount.Name = "label_spec_amount";
            this.label_spec_amount.Size = new System.Drawing.Size(46, 13);
            this.label_spec_amount.TabIndex = 20;
            this.label_spec_amount.Text = "Amount:";
            // 
            // button_doc_add_proceed
            // 
            this.button_doc_add_proceed.Location = new System.Drawing.Point(172, 390);
            this.button_doc_add_proceed.Name = "button_doc_add_proceed";
            this.button_doc_add_proceed.Size = new System.Drawing.Size(58, 26);
            this.button_doc_add_proceed.TabIndex = 22;
            this.button_doc_add_proceed.Text = "Add";
            this.button_doc_add_proceed.UseVisualStyleBackColor = true;
            this.button_doc_add_proceed.Visible = false;
            this.button_doc_add_proceed.Click += new System.EventHandler(this.button_doc_add_proceed_Click);
            // 
            // listBox_docs
            // 
            this.listBox_docs.FormattingEnabled = true;
            this.listBox_docs.Location = new System.Drawing.Point(43, 32);
            this.listBox_docs.Name = "listBox_docs";
            this.listBox_docs.Size = new System.Drawing.Size(331, 238);
            this.listBox_docs.TabIndex = 23;
            this.listBox_docs.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox_specs
            // 
            this.listBox_specs.FormattingEnabled = true;
            this.listBox_specs.Location = new System.Drawing.Point(400, 32);
            this.listBox_specs.Name = "listBox_specs";
            this.listBox_specs.Size = new System.Drawing.Size(331, 238);
            this.listBox_specs.TabIndex = 24;
            this.listBox_specs.SelectedIndexChanged += new System.EventHandler(this.listBox_specs_SelectedIndexChanged);
            // 
            // label_doc_amount
            // 
            this.label_doc_amount.AutoSize = true;
            this.label_doc_amount.Location = new System.Drawing.Point(40, 390);
            this.label_doc_amount.Name = "label_doc_amount";
            this.label_doc_amount.Size = new System.Drawing.Size(33, 13);
            this.label_doc_amount.TabIndex = 25;
            this.label_doc_amount.Text = "Note:";
            // 
            // button_specs_proceed
            // 
            this.button_specs_proceed.Location = new System.Drawing.Point(538, 377);
            this.button_specs_proceed.Name = "button_specs_proceed";
            this.button_specs_proceed.Size = new System.Drawing.Size(58, 26);
            this.button_specs_proceed.TabIndex = 27;
            this.button_specs_proceed.Text = "Proceed";
            this.button_specs_proceed.UseVisualStyleBackColor = true;
            this.button_specs_proceed.Visible = false;
            this.button_specs_proceed.Click += new System.EventHandler(this.button_specs_proceed_Click);
            // 
            // button_spec_add_proceed
            // 
            this.button_spec_add_proceed.Location = new System.Drawing.Point(538, 377);
            this.button_spec_add_proceed.Name = "button_spec_add_proceed";
            this.button_spec_add_proceed.Size = new System.Drawing.Size(58, 26);
            this.button_spec_add_proceed.TabIndex = 26;
            this.button_spec_add_proceed.Text = "Add";
            this.button_spec_add_proceed.UseVisualStyleBackColor = true;
            this.button_spec_add_proceed.Visible = false;
            this.button_spec_add_proceed.Click += new System.EventHandler(this.button_spec_add_proceed_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(301, 4);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(72, 24);
            this.button_refresh.TabIndex = 28;
            this.button_refresh.Text = "Refresh DB";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 461);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.button_specs_proceed);
            this.Controls.Add(this.button_spec_add_proceed);
            this.Controls.Add(this.label_doc_amount);
            this.Controls.Add(this.listBox_specs);
            this.Controls.Add(this.listBox_docs);
            this.Controls.Add(this.button_doc_add_proceed);
            this.Controls.Add(this.textBox_spec_amount);
            this.Controls.Add(this.label_spec_amount);
            this.Controls.Add(this.button_doc_edit_proceed);
            this.Controls.Add(this.textBox_doc_note);
            this.Controls.Add(this.label_doc_note);
            this.Controls.Add(this.dateTimePicker_doc);
            this.Controls.Add(this.label_doc_date);
            this.Controls.Add(this.textBox_doc_number);
            this.Controls.Add(this.label_doc_number);
            this.Controls.Add(this.textBox_spec_name);
            this.Controls.Add(this.label_spec_name);
            this.Controls.Add(this.button_del_spec);
            this.Controls.Add(this.button_edit_spec);
            this.Controls.Add(this.button_add_spec);
            this.Controls.Add(this.button_del_doc);
            this.Controls.Add(this.button_edit_doc);
            this.Controls.Add(this.button_add_doc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "DocsViewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_add_doc;
        private System.Windows.Forms.Button button_edit_doc;
        private System.Windows.Forms.Button button_del_doc;
        private System.Windows.Forms.Button button_del_spec;
        private System.Windows.Forms.Button button_edit_spec;
        private System.Windows.Forms.Button button_add_spec;
        private System.Windows.Forms.TextBox textBox_doc_number;
        private System.Windows.Forms.Label label_doc_number;
        private System.Windows.Forms.Label label_doc_date;
        private System.Windows.Forms.DateTimePicker dateTimePicker_doc;
        private System.Windows.Forms.TextBox textBox_doc_note;
        private System.Windows.Forms.Label label_doc_note;
        private System.Windows.Forms.Button button_doc_edit_proceed;
        private System.Windows.Forms.Label label_spec_name;
        private System.Windows.Forms.TextBox textBox_spec_name;
        private System.Windows.Forms.TextBox textBox_spec_amount;
        private System.Windows.Forms.Label label_spec_amount;
        private System.Windows.Forms.Button button_doc_add_proceed;
        private System.Windows.Forms.ListBox listBox_docs;
        private System.Windows.Forms.ListBox listBox_specs;
        private System.Windows.Forms.Label label_doc_amount;
        private System.Windows.Forms.Button button_specs_proceed;
        private System.Windows.Forms.Button button_spec_add_proceed;
        private System.Windows.Forms.Button button_refresh;
    }
}

