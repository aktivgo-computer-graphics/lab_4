namespace task
{
    partial class MainForm
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
            this.button = new System.Windows.Forms.Button();
            this.taskSwitch = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button.Location = new System.Drawing.Point(418, 9);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(140, 40);
            this.button.TabIndex = 0;
            this.button.Text = "Построить";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // taskSwitch
            // 
            this.taskSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.taskSwitch.FormattingEnabled = true;
            this.taskSwitch.ItemHeight = 25;
            this.taskSwitch.Items.AddRange(new object[] { "Задание 1", "Задание 2", "Задание 3", "Задание 4", "Задание 5", "Задание 8", "Задание 9", "Задание 11a", "Задание 11б", "Задание 11в", "Задание 11г" });
            this.taskSwitch.Location = new System.Drawing.Point(12, 12);
            this.taskSwitch.Name = "taskSwitch";
            this.taskSwitch.Size = new System.Drawing.Size(400, 33);
            this.taskSwitch.TabIndex = 1;
            this.taskSwitch.Text = "Выберите задание";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.taskSwitch);
            this.Controls.Add(this.button);
            this.Name = "MainForm";
            this.Text = "Линейчатые графические фигуры";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox taskSwitch;

        private System.Windows.Forms.Button button;

        #endregion
    }
}