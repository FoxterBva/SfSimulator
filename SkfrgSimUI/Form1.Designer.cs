namespace SkfrgSimUI
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
			this.btnNextEvent = new System.Windows.Forms.Button();
			this.rtbEvents = new System.Windows.Forms.RichTextBox();
			this.lblCurrentTimeLabel = new System.Windows.Forms.Label();
			this.lblCurrentTime = new System.Windows.Forms.Label();
			this.lblCombatLogLabel = new System.Windows.Forms.Label();
			this.rtbCombatLog = new System.Windows.Forms.RichTextBox();
			this.lblProcessedEventLabel = new System.Windows.Forms.Label();
			this.lblProcessedEvent = new System.Windows.Forms.Label();
			this.rtbBuffs = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnNextEvent
			// 
			this.btnNextEvent.Location = new System.Drawing.Point(562, 381);
			this.btnNextEvent.Name = "btnNextEvent";
			this.btnNextEvent.Size = new System.Drawing.Size(75, 23);
			this.btnNextEvent.TabIndex = 0;
			this.btnNextEvent.Text = "Next";
			this.btnNextEvent.UseVisualStyleBackColor = true;
			this.btnNextEvent.Click += new System.EventHandler(this.btnNextEvent_Click);
			// 
			// rtbEvents
			// 
			this.rtbEvents.Location = new System.Drawing.Point(562, 44);
			this.rtbEvents.Name = "rtbEvents";
			this.rtbEvents.Size = new System.Drawing.Size(417, 331);
			this.rtbEvents.TabIndex = 1;
			this.rtbEvents.Text = "";
			// 
			// lblCurrentTimeLabel
			// 
			this.lblCurrentTimeLabel.AutoSize = true;
			this.lblCurrentTimeLabel.Location = new System.Drawing.Point(559, 9);
			this.lblCurrentTimeLabel.Name = "lblCurrentTimeLabel";
			this.lblCurrentTimeLabel.Size = new System.Drawing.Size(70, 13);
			this.lblCurrentTimeLabel.TabIndex = 2;
			this.lblCurrentTimeLabel.Text = "Current Time:";
			// 
			// lblCurrentTime
			// 
			this.lblCurrentTime.AutoSize = true;
			this.lblCurrentTime.Location = new System.Drawing.Point(635, 9);
			this.lblCurrentTime.Name = "lblCurrentTime";
			this.lblCurrentTime.Size = new System.Drawing.Size(31, 13);
			this.lblCurrentTime.TabIndex = 3;
			this.lblCurrentTime.Text = "0000";
			// 
			// lblCombatLogLabel
			// 
			this.lblCombatLogLabel.AutoSize = true;
			this.lblCombatLogLabel.Location = new System.Drawing.Point(12, 9);
			this.lblCombatLogLabel.Name = "lblCombatLogLabel";
			this.lblCombatLogLabel.Size = new System.Drawing.Size(67, 13);
			this.lblCombatLogLabel.TabIndex = 4;
			this.lblCombatLogLabel.Text = "Combat Log:";
			// 
			// rtbCombatLog
			// 
			this.rtbCombatLog.Location = new System.Drawing.Point(15, 25);
			this.rtbCombatLog.Name = "rtbCombatLog";
			this.rtbCombatLog.Size = new System.Drawing.Size(513, 379);
			this.rtbCombatLog.TabIndex = 5;
			this.rtbCombatLog.Text = "";
			// 
			// lblProcessedEventLabel
			// 
			this.lblProcessedEventLabel.AutoSize = true;
			this.lblProcessedEventLabel.Location = new System.Drawing.Point(559, 28);
			this.lblProcessedEventLabel.Name = "lblProcessedEventLabel";
			this.lblProcessedEventLabel.Size = new System.Drawing.Size(114, 13);
			this.lblProcessedEventLabel.TabIndex = 6;
			this.lblProcessedEventLabel.Text = "Last Processed Event:";
			// 
			// lblProcessedEvent
			// 
			this.lblProcessedEvent.AutoSize = true;
			this.lblProcessedEvent.Location = new System.Drawing.Point(670, 28);
			this.lblProcessedEvent.Name = "lblProcessedEvent";
			this.lblProcessedEvent.Size = new System.Drawing.Size(16, 13);
			this.lblProcessedEvent.TabIndex = 7;
			this.lblProcessedEvent.Text = "---";
			// 
			// rtbBuffs
			// 
			this.rtbBuffs.Location = new System.Drawing.Point(994, 44);
			this.rtbBuffs.Name = "rtbBuffs";
			this.rtbBuffs.Size = new System.Drawing.Size(271, 331);
			this.rtbBuffs.TabIndex = 8;
			this.rtbBuffs.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(991, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Buffs:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1338, 432);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rtbBuffs);
			this.Controls.Add(this.lblProcessedEvent);
			this.Controls.Add(this.lblProcessedEventLabel);
			this.Controls.Add(this.rtbCombatLog);
			this.Controls.Add(this.lblCombatLogLabel);
			this.Controls.Add(this.lblCurrentTime);
			this.Controls.Add(this.lblCurrentTimeLabel);
			this.Controls.Add(this.rtbEvents);
			this.Controls.Add(this.btnNextEvent);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnNextEvent;
		private System.Windows.Forms.RichTextBox rtbEvents;
		private System.Windows.Forms.Label lblCurrentTimeLabel;
		private System.Windows.Forms.Label lblCurrentTime;
		private System.Windows.Forms.Label lblCombatLogLabel;
		private System.Windows.Forms.RichTextBox rtbCombatLog;
		private System.Windows.Forms.Label lblProcessedEventLabel;
		private System.Windows.Forms.Label lblProcessedEvent;
		private System.Windows.Forms.RichTextBox rtbBuffs;
		private System.Windows.Forms.Label label1;
	}
}

