/*
 * Created by SharpDevelop.
 * User: CodeExplorer
 * Date: 23.12.2019
 * Time: 14:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Modulo2Simplificator
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 13);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(30, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(48, -1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(312, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Equations File name: (if empty load equations from textbox)";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(48, 15);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(326, 20);
			this.textBox1.TabIndex = 3;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(48, 80);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(326, 106);
			this.textBox2.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(48, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(330, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Enter here modulo 2 equations separated by \':\' or newline";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(48, 192);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 6;
			this.button2.Text = "Simplificate";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(209, 192);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(193, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "Status";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 41);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(381, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "By default all equations are consideret to be equal with 0 else enter equation=1";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(48, 242);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(312, 106);
			this.textBox3.TabIndex = 9;
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(129, 221);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(74, 15);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "Reinsert *";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(128, 192);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 11;
			this.button3.Text = "Product";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(405, 360);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Name = "MainForm";
			this.Text = "Modulo 2 Simplificator";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
	}
}
