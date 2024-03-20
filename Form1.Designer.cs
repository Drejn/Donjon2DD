namespace DonjonImporter
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
            btn_load = new Button();
            txt_file_path = new TextBox();
            label1 = new Label();
            label2 = new Label();
            lbl_map_width = new Label();
            lbl_map_height = new Label();
            openFileDialog1 = new OpenFileDialog();
            lbl_map_cell_count = new Label();
            label4 = new Label();
            label3 = new Label();
            lbl_rooms = new Label();
            btn_generate = new Button();
            SuspendLayout();
            // 
            // btn_load
            // 
            btn_load.Location = new Point(26, 12);
            btn_load.Name = "btn_load";
            btn_load.Size = new Size(94, 38);
            btn_load.TabIndex = 1;
            btn_load.Text = "Load";
            btn_load.UseVisualStyleBackColor = true;
            btn_load.Click += btn_load_Click;
            // 
            // txt_file_path
            // 
            txt_file_path.Location = new Point(150, 18);
            txt_file_path.Name = "txt_file_path";
            txt_file_path.ReadOnly = true;
            txt_file_path.Size = new Size(261, 27);
            txt_file_path.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 88);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 3;
            label1.Text = "Map Width";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 124);
            label2.Name = "label2";
            label2.Size = new Size(88, 20);
            label2.TabIndex = 4;
            label2.Text = "Map Height";
            // 
            // lbl_map_width
            // 
            lbl_map_width.AutoSize = true;
            lbl_map_width.Location = new Point(124, 88);
            lbl_map_width.Name = "lbl_map_width";
            lbl_map_width.Size = new Size(17, 20);
            lbl_map_width.TabIndex = 5;
            lbl_map_width.Text = "0";
            // 
            // lbl_map_height
            // 
            lbl_map_height.AutoSize = true;
            lbl_map_height.Location = new Point(124, 124);
            lbl_map_height.Name = "lbl_map_height";
            lbl_map_height.Size = new Size(17, 20);
            lbl_map_height.TabIndex = 6;
            lbl_map_height.Text = "0";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbl_map_cell_count
            // 
            lbl_map_cell_count.AutoSize = true;
            lbl_map_cell_count.Location = new Point(124, 154);
            lbl_map_cell_count.Name = "lbl_map_cell_count";
            lbl_map_cell_count.Size = new Size(17, 20);
            lbl_map_cell_count.TabIndex = 8;
            lbl_map_cell_count.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 154);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 7;
            label4.Text = "Cell Count";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 187);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 9;
            label3.Text = "Rooms";
            // 
            // lbl_rooms
            // 
            lbl_rooms.AutoSize = true;
            lbl_rooms.Location = new Point(124, 187);
            lbl_rooms.Name = "lbl_rooms";
            lbl_rooms.Size = new Size(17, 20);
            lbl_rooms.TabIndex = 10;
            lbl_rooms.Text = "0";
            // 
            // btn_generate
            // 
            btn_generate.Location = new Point(422, 12);
            btn_generate.Name = "btn_generate";
            btn_generate.Size = new Size(94, 38);
            btn_generate.TabIndex = 11;
            btn_generate.Text = "Generate";
            btn_generate.UseVisualStyleBackColor = true;
            btn_generate.Click += btn_generate_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(528, 450);
            Controls.Add(btn_generate);
            Controls.Add(lbl_rooms);
            Controls.Add(label3);
            Controls.Add(lbl_map_cell_count);
            Controls.Add(label4);
            Controls.Add(lbl_map_height);
            Controls.Add(lbl_map_width);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_file_path);
            Controls.Add(btn_load);
            Name = "Form1";
            Text = "Donjon2DD";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_load;
        private TextBox txt_file_path;
        private Label label1;
        private Label label2;
        private Label lbl_map_width;
        private Label lbl_map_height;
        private OpenFileDialog openFileDialog1;
        private Label lbl_map_cell_count;
        private Label label4;
        private Label label3;
        private Label lbl_rooms;
        private Button btn_generate;
    }
}