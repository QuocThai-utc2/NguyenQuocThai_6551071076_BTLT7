namespace bai4
{
    partial class FormBacSi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblMa = new Label();
            txtMaBS = new TextBox();
            lblTen = new Label();
            txtHoTen = new TextBox();
            lblCK = new Label();
            txtChuyenKhoa = new TextBox();
            lblSDT = new Label();
            txtSDT = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            dgvBacSi = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvBacSi).BeginInit();
            SuspendLayout();
            // 
            // lblMa
            // 
            lblMa.AutoSize = true;
            lblMa.Location = new Point(20, 22);
            lblMa.Name = "lblMa";
            lblMa.Text = "Mã bác sĩ";
            // 
            // txtMaBS
            // 
            txtMaBS.Location = new Point(130, 19);
            txtMaBS.Name = "txtMaBS";
            txtMaBS.ReadOnly = true;
            txtMaBS.Size = new Size(250, 27);
            txtMaBS.TabStop = false;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Location = new Point(20, 58);
            lblTen.Name = "lblTen";
            lblTen.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(130, 55);
            txtHoTen.MaxLength = 100;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(250, 27);
            txtHoTen.TabIndex = 0;
            // 
            // lblCK
            // 
            lblCK.AutoSize = true;
            lblCK.Location = new Point(20, 94);
            lblCK.Name = "lblCK";
            lblCK.Text = "Chuyên khoa";
            // 
            // txtChuyenKhoa
            // 
            txtChuyenKhoa.Location = new Point(130, 91);
            txtChuyenKhoa.MaxLength = 100;
            txtChuyenKhoa.Name = "txtChuyenKhoa";
            txtChuyenKhoa.Size = new Size(250, 27);
            txtChuyenKhoa.TabIndex = 1;
            // 
            // lblSDT
            // 
            lblSDT.AutoSize = true;
            lblSDT.Location = new Point(20, 130);
            lblSDT.Name = "lblSDT";
            lblSDT.Text = "Số điện thoại";
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(130, 127);
            txtSDT.MaxLength = 15;
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(250, 27);
            txtSDT.TabIndex = 2;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(420, 15);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(110, 34);
            btnThem.TabIndex = 3;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(420, 55);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(110, 34);
            btnSua.TabIndex = 4;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(420, 95);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(110, 34);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(420, 135);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 34);
            btnLamMoi.TabIndex = 6;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // dgvBacSi
            // 
            dgvBacSi.AllowUserToAddRows = false;
            dgvBacSi.AllowUserToDeleteRows = false;
            dgvBacSi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBacSi.BackgroundColor = SystemColors.Window;
            dgvBacSi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBacSi.Location = new Point(20, 185);
            dgvBacSi.MultiSelect = false;
            dgvBacSi.Name = "dgvBacSi";
            dgvBacSi.ReadOnly = true;
            dgvBacSi.RowHeadersWidth = 30;
            dgvBacSi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBacSi.Size = new Size(510, 235);
            dgvBacSi.TabIndex = 7;
            // 
            // FormBacSi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(554, 441);
            Controls.Add(dgvBacSi);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtSDT);
            Controls.Add(lblSDT);
            Controls.Add(txtChuyenKhoa);
            Controls.Add(lblCK);
            Controls.Add(txtHoTen);
            Controls.Add(lblTen);
            Controls.Add(txtMaBS);
            Controls.Add(lblMa);
            MinimumSize = new Size(570, 480);
            Name = "FormBacSi";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản Lý Bác Sĩ";
            Load += FormBacSi_Load;
            dgvBacSi.SelectionChanged += dgvBacSi_SelectionChanged;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            ((System.ComponentModel.ISupportInitialize)dgvBacSi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMa;
        private TextBox txtMaBS;
        private Label lblTen;
        private TextBox txtHoTen;
        private Label lblCK;
        private TextBox txtChuyenKhoa;
        private Label lblSDT;
        private TextBox txtSDT;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvBacSi;
    }
}