namespace bai4
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
            lblTen = new Label();
            txtTenBenhNhan = new TextBox();
            lblSDT = new Label();
            txtSDT = new TextBox();
            lblNgay = new Label();
            dtpNgayKham = new DateTimePicker();
            lblGio = new Label();
            dtpGioKham = new DateTimePicker();
            lblBacSi = new Label();
            cboBacSi = new ComboBox();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            btnBacSi = new Button();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblTuNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            lblDenNgay = new Label();
            dtpDenNgay = new DateTimePicker();
            cboLocBacSi = new ComboBox();
            btnTimKiem = new Button();
            dgvLichKham = new DataGridView();
            Load += Form1_Load;
            dgvLichKham.SelectionChanged += dgvLichKham_SelectionChanged;
            btnBacSi.Click += btnBacSi_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            ((System.ComponentModel.ISupportInitialize)dgvLichKham).BeginInit();
            SuspendLayout();
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Location = new Point(20, 22);
            lblTen.Name = "lblTen";
            lblTen.Text = "Tên bệnh nhân";
            // 
            // txtTenBenhNhan
            // 
            txtTenBenhNhan.Location = new Point(140, 19);
            txtTenBenhNhan.MaxLength = 100;
            txtTenBenhNhan.Name = "txtTenBenhNhan";
            txtTenBenhNhan.Size = new Size(230, 27);
            txtTenBenhNhan.TabIndex = 0;
            // 
            // lblSDT
            // 
            lblSDT.AutoSize = true;
            lblSDT.Location = new Point(20, 62);
            lblSDT.Name = "lblSDT";
            lblSDT.Text = "Số điện thoại";
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(140, 59);
            txtSDT.MaxLength = 15;
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(230, 27);
            txtSDT.TabIndex = 1;
            // 
            // lblNgay
            // 
            lblNgay.AutoSize = true;
            lblNgay.Location = new Point(20, 102);
            lblNgay.Name = "lblNgay";
            lblNgay.Text = "Ngày khám";
            // 
            // dtpNgayKham
            // 
            dtpNgayKham.Format = DateTimePickerFormat.Short;
            dtpNgayKham.Location = new Point(140, 99);
            dtpNgayKham.Name = "dtpNgayKham";
            dtpNgayKham.Size = new Size(230, 27);
            dtpNgayKham.TabIndex = 2;
            // 
            // lblGio
            // 
            lblGio.AutoSize = true;
            lblGio.Location = new Point(400, 22);
            lblGio.Name = "lblGio";
            lblGio.Text = "Giờ khám";
            // 
            // dtpGioKham
            // 
            dtpGioKham.Format = DateTimePickerFormat.Time;
            dtpGioKham.Location = new Point(400, 45);
            dtpGioKham.Name = "dtpGioKham";
            dtpGioKham.ShowUpDown = true;
            dtpGioKham.Size = new Size(110, 27);
            dtpGioKham.TabIndex = 3;
            // 
            // lblBacSi
            // 
            lblBacSi.AutoSize = true;
            lblBacSi.Location = new Point(540, 22);
            lblBacSi.Name = "lblBacSi";
            lblBacSi.Text = "Bác sĩ";
            // 
            // cboBacSi
            // 
            cboBacSi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBacSi.Location = new Point(540, 45);
            cboBacSi.Name = "cboBacSi";
            cboBacSi.Size = new Size(280, 28);
            cboBacSi.TabIndex = 4;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(400, 85);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Text = "Trạng thái";
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Location = new Point(400, 108);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(150, 28);
            cboTrangThai.TabIndex = 5;
            // 
            // btnBacSi
            // 
            btnBacSi.Location = new Point(20, 145);
            btnBacSi.Name = "btnBacSi";
            btnBacSi.Size = new Size(170, 34);
            btnBacSi.TabIndex = 6;
            btnBacSi.Text = "Quản lý bác sĩ";
            btnBacSi.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(850, 15);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(100, 34);
            btnThem.TabIndex = 7;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(850, 55);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 34);
            btnSua.TabIndex = 8;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(850, 95);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 34);
            btnXoa.TabIndex = 9;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(850, 135);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(100, 34);
            btnLamMoi.TabIndex = 10;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // lblTuNgay
            // 
            lblTuNgay.AutoSize = true;
            lblTuNgay.Location = new Point(20, 205);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Text = "Từ ngày";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpTuNgay.Location = new Point(90, 201);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(130, 27);
            dtpTuNgay.TabIndex = 11;
            // 
            // lblDenNgay
            // 
            lblDenNgay.AutoSize = true;
            lblDenNgay.Location = new Point(235, 205);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Text = "Đến ngày";
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Location = new Point(315, 201);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(130, 27);
            dtpDenNgay.TabIndex = 12;
            // 
            // cboLocBacSi
            // 
            cboLocBacSi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocBacSi.Location = new Point(465, 201);
            cboLocBacSi.Name = "cboLocBacSi";
            cboLocBacSi.Size = new Size(280, 28);
            cboLocBacSi.TabIndex = 13;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(760, 198);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(110, 34);
            btnTimKiem.TabIndex = 14;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // dgvLichKham
            // 
            dgvLichKham.AllowUserToAddRows = false;
            dgvLichKham.AllowUserToDeleteRows = false;
            dgvLichKham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLichKham.BackgroundColor = SystemColors.Window;
            dgvLichKham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichKham.Location = new Point(20, 250);
            dgvLichKham.MultiSelect = false;
            dgvLichKham.Name = "dgvLichKham";
            dgvLichKham.ReadOnly = true;
            dgvLichKham.RowHeadersWidth = 30;
            dgvLichKham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLichKham.Size = new Size(930, 290);
            dgvLichKham.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 561);
            Controls.Add(dgvLichKham);
            Controls.Add(btnTimKiem);
            Controls.Add(cboLocBacSi);
            Controls.Add(dtpDenNgay);
            Controls.Add(lblDenNgay);
            Controls.Add(dtpTuNgay);
            Controls.Add(lblTuNgay);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(btnBacSi);
            Controls.Add(cboTrangThai);
            Controls.Add(lblTrangThai);
            Controls.Add(cboBacSi);
            Controls.Add(lblBacSi);
            Controls.Add(dtpGioKham);
            Controls.Add(lblGio);
            Controls.Add(dtpNgayKham);
            Controls.Add(lblNgay);
            Controls.Add(txtSDT);
            Controls.Add(lblSDT);
            Controls.Add(txtTenBenhNhan);
            Controls.Add(lblTen);
            MinimumSize = new Size(990, 600);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản Lý Lịch Khám Bệnh - An Khang Clinic";
            ((System.ComponentModel.ISupportInitialize)dgvLichKham).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTen;
        private TextBox txtTenBenhNhan;
        private Label lblSDT;
        private TextBox txtSDT;
        private Label lblNgay;
        private DateTimePicker dtpNgayKham;
        private Label lblGio;
        private DateTimePicker dtpGioKham;
        private Label lblBacSi;
        private ComboBox cboBacSi;
        private Label lblTrangThai;
        private ComboBox cboTrangThai;
        private Button btnBacSi;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private ComboBox cboLocBacSi;
        private Button btnTimKiem;
        private DataGridView dgvLichKham;
    }
}