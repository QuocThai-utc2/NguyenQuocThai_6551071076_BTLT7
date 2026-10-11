namespace bai3
{
    partial class FormLoaiPhong
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
            txtMaLoai = new TextBox();
            lblTen = new Label();
            txtTenLoai = new TextBox();
            lblGia = new Label();
            txtGia = new TextBox();
            lblMoTa = new Label();
            txtMoTa = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            dgvLoaiPhong = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiPhong).BeginInit();
            SuspendLayout();
            // 
            // lblMa
            // 
            lblMa.AutoSize = true;
            lblMa.Location = new Point(20, 22);
            lblMa.Name = "lblMa";
            lblMa.Text = "Mã loại";
            // 
            // txtMaLoai
            // 
            txtMaLoai.Location = new Point(130, 19);
            txtMaLoai.Name = "txtMaLoai";
            txtMaLoai.ReadOnly = true;
            txtMaLoai.Size = new Size(250, 27);
            txtMaLoai.TabStop = false;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Location = new Point(20, 58);
            lblTen.Name = "lblTen";
            lblTen.Text = "Tên loại";
            // 
            // txtTenLoai
            // 
            txtTenLoai.Location = new Point(130, 55);
            txtTenLoai.MaxLength = 100;
            txtTenLoai.Name = "txtTenLoai";
            txtTenLoai.Size = new Size(250, 27);
            txtTenLoai.TabIndex = 0;
            // 
            // lblGia
            // 
            lblGia.AutoSize = true;
            lblGia.Location = new Point(20, 94);
            lblGia.Name = "lblGia";
            lblGia.Text = "Giá mỗi đêm";
            // 
            // txtGia
            // 
            txtGia.Location = new Point(130, 91);
            txtGia.Name = "txtGia";
            txtGia.Size = new Size(250, 27);
            txtGia.TabIndex = 1;
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(20, 130);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Text = "Mô tả";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(130, 127);
            txtMoTa.MaxLength = 255;
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.ScrollBars = ScrollBars.Vertical;
            txtMoTa.Size = new Size(250, 70);
            txtMoTa.TabIndex = 2;
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
            // dgvLoaiPhong
            // 
            dgvLoaiPhong.AllowUserToAddRows = false;
            dgvLoaiPhong.AllowUserToDeleteRows = false;
            dgvLoaiPhong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLoaiPhong.BackgroundColor = SystemColors.Window;
            dgvLoaiPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoaiPhong.Location = new Point(20, 215);
            dgvLoaiPhong.MultiSelect = false;
            dgvLoaiPhong.Name = "dgvLoaiPhong";
            dgvLoaiPhong.ReadOnly = true;
            dgvLoaiPhong.RowHeadersWidth = 30;
            dgvLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiPhong.Size = new Size(510, 235);
            dgvLoaiPhong.TabIndex = 7;
            // 
            // FormLoaiPhong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(554, 471);
            Controls.Add(dgvLoaiPhong);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtMoTa);
            Controls.Add(lblMoTa);
            Controls.Add(txtGia);
            Controls.Add(lblGia);
            Controls.Add(txtTenLoai);
            Controls.Add(lblTen);
            Controls.Add(txtMaLoai);
            Controls.Add(lblMa);
            MinimumSize = new Size(570, 510);
            Name = "FormLoaiPhong";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản Lý Loại Phòng";
            ((System.ComponentModel.ISupportInitialize)dgvLoaiPhong).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMa;
        private TextBox txtMaLoai;
        private Label lblTen;
        private TextBox txtTenLoai;
        private Label lblGia;
        private TextBox txtGia;
        private Label lblMoTa;
        private TextBox txtMoTa;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvLoaiPhong;
    }
}