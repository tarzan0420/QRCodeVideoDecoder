
namespace QRCodeVideoDecoder
{
    partial class QRCodeVideoDecoder
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
            this.PreviewPanel = new System.Windows.Forms.Panel();
            this.DecodedDataLabel = new System.Windows.Forms.Label();
            this.DataTextBox = new System.Windows.Forms.TextBox();
            this.DecodeButton = new System.Windows.Forms.Button();
            this.EncodeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PreviewPanel
            // 
            this.PreviewPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewPanel.Location = new System.Drawing.Point(43, 32);
            this.PreviewPanel.Name = "PreviewPanel";
            this.PreviewPanel.Size = new System.Drawing.Size(747, 360);
            this.PreviewPanel.TabIndex = 0;
            // 
            // DecodedDataLabel
            // 
            this.DecodedDataLabel.AutoSize = true;
            this.DecodedDataLabel.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecodedDataLabel.Location = new System.Drawing.Point(40, 410);
            this.DecodedDataLabel.Name = "DecodedDataLabel";
            this.DecodedDataLabel.Size = new System.Drawing.Size(76, 16);
            this.DecodedDataLabel.TabIndex = 1;
            this.DecodedDataLabel.Text = "Decoded Data";
            // 
            // DataTextBox
            // 
            this.DataTextBox.AcceptsReturn = true;
            this.DataTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataTextBox.Location = new System.Drawing.Point(43, 440);
            this.DataTextBox.Multiline = true;
            this.DataTextBox.Name = "DataTextBox";
            this.DataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataTextBox.Size = new System.Drawing.Size(746, 100);
            this.DataTextBox.TabIndex = 2;
            // 
            // DecodeButton
            // 
            this.DecodeButton.Location = new System.Drawing.Point(156, 566);
            this.DecodeButton.Name = "DecodeButton";
            this.DecodeButton.Size = new System.Drawing.Size(184, 43);
            this.DecodeButton.TabIndex = 3;
            this.DecodeButton.Text = "Decode";
            this.DecodeButton.UseVisualStyleBackColor = true;
            this.DecodeButton.Click += new System.EventHandler(this.OnDecodeButtonClick);
            // 
            // EncodeButton
            // 
            this.EncodeButton.Location = new System.Drawing.Point(475, 566);
            this.EncodeButton.Name = "EncodeButton";
            this.EncodeButton.Size = new System.Drawing.Size(184, 43);
            this.EncodeButton.TabIndex = 4;
            this.EncodeButton.Text = "Encode";
            this.EncodeButton.UseVisualStyleBackColor = true;
            this.EncodeButton.Click += new System.EventHandler(this.OnEncodeButtonClick);
            // 
            // QRCodeVideoDecoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(835, 619);
            this.Controls.Add(this.EncodeButton);
            this.Controls.Add(this.DecodeButton);
            this.Controls.Add(this.DataTextBox);
            this.Controls.Add(this.DecodedDataLabel);
            this.Controls.Add(this.PreviewPanel);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "QRCodeVideoDecoder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.Resize += new System.EventHandler(this.OnResize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PreviewPanel;
        private System.Windows.Forms.Label DecodedDataLabel;
        private System.Windows.Forms.TextBox DataTextBox;
        private System.Windows.Forms.Button DecodeButton;
        private System.Windows.Forms.Button EncodeButton;
    }
}

