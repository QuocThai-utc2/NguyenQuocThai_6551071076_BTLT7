namespace bai3
{
    partial class Form1
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
            lblSoPhong = new Label();
            txtSoPhong = new TextBox();
            lblTang = new Label();
            nudTang = new NumericUpDown();
            lblLoai = new Label();
            cboLoaiPhong = new ComboBox();
            lblTinhTrang = new Label();
            cboTinhTrang = new ComboBox();
            picAnh = new PictureBox();
            btnChonAnh = new Button();
            btnLoaiPhong = new Button();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            cboLocLoai = new ComboBox();
            cboLocTinhTrang = new ComboBox();
            btnTimKiem = new Button();
            dgvPhong = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)nudTang).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAnh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).BeginInit();
            SuspendLayout();
            // 
            // lblSoPhong
            // 
            lblSoPhong.AutoSize = true;
            lblSoPhong.Location = new Point(20, 20);
            lblSoPhong.Name = "lblSoPhong";
            lblSoPhong.Text = "Số phòng";
            // 
            // txtSoPhong
            // 
            txtSoPhong.Location = new Point(20, 45);
            txtSoPhong.MaxLength = 10;
            txtSoPhong.Name = "txtSoPhong";
            txtSoPhong.Size = new Size(150, 27);
            txtSoPhong.TabIndex = 0;
            // 
            // lblTang
            // 
            lblTang.AutoSize = true;
            lblTang.Location = new Point(190, 20);
            lblTang.Name = "lblTang";
            lblTang.Text = "Tầng số";
            // 
            // nudTang
            // 
            nudTang.Location = new Point(190, 45);
            nudTang.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudTang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTang.Name = "nudTang";
            nudTang.Size = new Size(80, 27);
            nudTang.TabIndex = 1;
            nudTang.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblLoai
            // 
            lblLoai.AutoSize = true;
            lblLoai.Location = new Point(290, 20);
            lblLoai.Name = "lblLoai";
            lblLoai.Text = "Loại phòng";
            // 
            // cboLoaiPhong
            // 
            cboLoaiPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiPhong.Location = new Point(290, 45);
            cboLoaiPhong.Name = "cboLoaiPhong";
            cboLoaiPhong.Size = new Size(150, 28);
            cboLoaiPhong.TabIndex = 2;
            // 
            // lblTinhTrang
            // 
            lblTinhTrang.AutoSize = true;
            lblTinhTrang.Location = new Point(460, 20);
            lblTinhTrang.Name = "lblTinhTrang";
            lblTinhTrang.Text = "Tình trạng";
            // 
            // cboTinhTrang
            // 
            cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTinhTrang.Location = new Point(460, 45);
            cboTinhTrang.Name = "cboTinhTrang";
            cboTinhTrang.Size = new Size(130, 28);
            cboTinhTrang.TabIndex = 3;
            // 
            // picAnh
            // 
            picAnh.BorderStyle = BorderStyle.FixedSingle;
            picAnh.Location = new Point(620, 15);
            picAnh.Name = "picAnh";
            picAnh.Size = new Size(120, 90);
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picAnh.TabStop = false;
            // 
            // btnChonAnh
            // 
            btnChonAnh.Location = new Point(620, 110);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(120, 32);
            btnChonAnh.TabIndex = 4;
            btnChonAnh.Text = "Chọn ảnh...";
            btnChonAnh.UseVisualStyleBackColor = true;
            // 
            // btnLoaiPhong
            // 
            btnLoaiPhong.Location = new Point(20, 95);
            btnLoaiPhong.Name = "btnLoaiPhong";
            btnLoaiPhong.Size = new Size(170, 34);
            btnLoaiPhong.TabIndex = 5;
            btnLoaiPhong.Text = "Quản lý loại phòng";
            btnLoaiPhong.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(770, 15);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(110, 34);
            btnThem.TabIndex = 6;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(770, 55);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(110, 34);
            btnSua.TabIndex = 7;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(770, 95);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(110, 34);
            btnXoa.TabIndex = 8;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(770, 135);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 34);
            btnLamMoi.TabIndex = 9;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // cboLocLoai
            // 
            cboLocLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocLoai.Location = new Point(20, 180);
            cboLocLoai.Name = "cboLocLoai";
            cboLocLoai.Size = new Size(200, 28);
            cboLocLoai.TabIndex = 10;
            // 
            // cboLocTinhTrang
            // 
            cboLocTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocTinhTrang.Location = new Point(235, 180);
            cboLocTinhTrang.Name = "cboLocTinhTrang";
            cboLocTinhTrang.Size = new Size(200, 28);
            cboLocTinhTrang.TabIndex = 11;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(450, 177);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(110, 34);
            btnTimKiem.TabIndex = 12;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // dgvPhong
            // 
            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.AllowUserToDeleteRows = false;
            dgvPhong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPhong.BackgroundColor = SystemColors.Window;
            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhong.Location = new Point(20, 225);
            dgvPhong.MultiSelect = false;
            dgvPhong.Name = "dgvPhong";
            dgvPhong.ReadOnly = true;
            dgvPhong.RowHeadersWidth = 30;
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.Size = new Size(860, 395);
            dgvPhong.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 641);
            Controls.Add(dgvPhong);
            Controls.Add(btnTimKiem);
            Controls.Add(cboLocTinhTrang);
            Controls.Add(cboLocLoai);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(btnLoaiPhong);
            Controls.Add(btnChonAnh);
            Controls.Add(picAnh);
            Controls.Add(cboTinhTrang);
            Controls.Add(lblTinhTrang);
            Controls.Add(cboLoaiPhong);
            Controls.Add(lblLoai);
            Controls.Add(nudTang);
            Controls.Add(lblTang);
            Controls.Add(txtSoPhong);
            Controls.Add(lblSoPhong);
            MinimumSize = new Size(920, 680);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Phòng - Sunrise Homestay";
            Load += Form1_Load;
            dgvPhong.SelectionChanged += dgvPhong_SelectionChanged;
            btnChonAnh.Click += btnChonAnh_Click;
            btnLoaiPhong.Click += btnLoaiPhong_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            ((System.ComponentModel.ISupportInitialize)nudTang).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAnh).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPhong).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSoPhong;
        private TextBox txtSoPhong;
        private Label lblTang;
        private NumericUpDown nudTang;  
        private Label lblLoai;
        private ComboBox cboLoaiPhong;
        private Label lblTinhTrang;
        private ComboBox cboTinhTrang;
        private PictureBox picAnh;
        private Button btnChonAnh;
        private Button btnLoaiPhong;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private ComboBox cboLocLoai;
        private ComboBox cboLocTinhTrang;
        private Button btnTimKiem;
        private DataGridView dgvPhong;
    }
}