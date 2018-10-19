namespace Pomodoro
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtTodoLater = new System.Windows.Forms.TextBox();
            this.btnNotes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(209, 204);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Start\r\n5m breaks";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseButtonUp);
            // 
            // txtTodoLater
            // 
            this.txtTodoLater.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtTodoLater.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTodoLater.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTodoLater.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTodoLater.Location = new System.Drawing.Point(0, 0);
            this.txtTodoLater.Multiline = true;
            this.txtTodoLater.Name = "txtTodoLater";
            this.txtTodoLater.Size = new System.Drawing.Size(209, 204);
            this.txtTodoLater.TabIndex = 1;
            this.txtTodoLater.Text = "==For later==";
            this.txtTodoLater.Visible = false;
            // 
            // btnNotes
            // 
            this.btnNotes.Location = new System.Drawing.Point(166, 181);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size(43, 23);
            this.btnNotes.TabIndex = 2;
            this.btnNotes.Text = "4L8r";
            this.btnNotes.UseVisualStyleBackColor = true;
            this.btnNotes.Click += new System.EventHandler(this.OnNoteButtonClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(209, 204);
            this.Controls.Add(this.btnNotes);
            this.Controls.Add(this.txtTodoLater);
            this.Controls.Add(this.lblStatus);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Pomodoro";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtTodoLater;
        private System.Windows.Forms.Button btnNotes;
    }
}

