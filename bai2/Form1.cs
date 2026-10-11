using bai2.Models;
using Microsoft.EntityFrameworkCore;

namespace bai2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            cboHang.Items.AddRange(new object[] { "Basic", "VIP", "Premium" });
            cboHang.SelectedIndex = 0;

            cboLocHang.Items.AddRange(new object[] { "Tất cả", "Basic", "VIP", "Premium" });
            cboLocHang.SelectedIndex = 0;

            dtpNgaySinh.Value = DateTime.Today.AddYears(-18);

            CauHinhGrid();
            await LoadDataAsync();
            XoaTrangForm();
        }

        private void dgvHoiVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoiVien.CurrentRow?.DataBoundItem is not HoiVien hv) return;

            txtHoTen.Text = hv.HoTen;
            txtSDT.Text = hv.SDT;
            txtEmail.Text = hv.Email;

            rdoNam.Checked = hv.GioiTinh;
            rdoNu.Checked = !hv.GioiTinh;

            dtpNgaySinh.Value = hv.NgaySinh;

            cboHang.SelectedIndex = cboHang.FindStringExact(hv.HangThanhVien);

            chkTrangThai.Checked = hv.TrangThai;
        }

        private void dgvHoiVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value is not bool giaTri) return;

            string cot = dgvHoiVien.Columns[e.ColumnIndex].DataPropertyName;

            if (cot == "GioiTinh")
            {
                e.Value = giaTri ? "Nam" : "Nữ";
                e.FormattingApplied = true;
            }
            else if (cot == "TrangThai")
            {
                e.Value = giaTri ? "Đang hoạt động" : "Tạm ngưng";
                e.FormattingApplied = true;
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using var db = new FitZoneContext();

                var hv = new HoiVien
                {
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = rdoNam.Checked,
                    NgaySinh = dtpNgaySinh.Value.Date,
                    SDT = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    HangThanhVien = cboHang.SelectedItem!.ToString()!,
                    TrangThai = chkTrangThai.Checked
                };

                db.HoiViens.Add(hv);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm hội viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(txtTimKiem.Text, HangDangLoc());
                XoaTrangForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (!LayMaDangChon(out int id)) return;
            if (!ValidateInput()) return;

            try
            {
                using var db = new FitZoneContext();
                var hv = await db.HoiViens.FindAsync(id);
                if (hv == null)
                {
                    MessageBox.Show("Hội viên này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync();
                    XoaTrangForm();
                    return;
                }

                hv.HoTen = txtHoTen.Text.Trim();
                hv.GioiTinh = rdoNam.Checked;
                hv.NgaySinh = dtpNgaySinh.Value.Date;
                hv.SDT = txtSDT.Text.Trim();
                hv.Email = txtEmail.Text.Trim();
                hv.HangThanhVien = cboHang.SelectedItem!.ToString()!;
                hv.TrangThai = chkTrangThai.Checked;

                await db.SaveChangesAsync();

                MessageBox.Show("Cập nhật hội viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(txtTimKiem.Text, HangDangLoc());
                XoaTrangForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (!LayMaDangChon(out int id)) return;

            var xacNhan = MessageBox.Show(
                $"Bạn có chắc muốn xóa hội viên \"{txtHoTen.Text}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var db = new FitZoneContext();
                var hv = await db.HoiViens.FindAsync(id);
                if (hv == null)
                {
                    MessageBox.Show("Hội viên này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync();
                    XoaTrangForm();
                    return;
                }

                db.HoiViens.Remove(hv);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa hội viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(txtTimKiem.Text, HangDangLoc());
                XoaTrangForm();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa hội viên này vì đang có dữ liệu liên quan!", "Lỗi ràng buộc",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLocHang.SelectedIndex = 0;
            await LoadDataAsync();
            XoaTrangForm();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await LoadDataAsync(txtTimKiem.Text, HangDangLoc());
            XoaTrangForm();
        }

        private void CauHinhGrid()
        {
            dgvHoiVien.AutoGenerateColumns = false;
            dgvHoiVien.Columns.Clear();

            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MaHV", HeaderText = "Mã HV", Width = 70 });
            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "HoTen", HeaderText = "Họ tên", Width = 160 });
            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "GioiTinh", HeaderText = "Giới tính", Width = 80 });
            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgaySinh",
                HeaderText = "Ngày sinh",
                Width = 100,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });
            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "SDT", HeaderText = "SĐT", Width = 110 });
            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "Email", HeaderText = "Email", Width = 150 });
            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "HangThanhVien", HeaderText = "Hạng thành viên", Width = 120 });
            dgvHoiVien.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TrangThai", HeaderText = "Trạng thái", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

\        private async Task LoadDataAsync(string ten = "", string hang = "")
        {
            using var db = new FitZoneContext();

            ten = ten.Trim();
            bool coTen = !string.IsNullOrEmpty(ten);
            bool coHang = !string.IsNullOrEmpty(hang);

            var ds = await db.HoiViens
                .AsNoTracking()
                .Where(h => (!coTen || h.HoTen.Contains(ten)) && (!coHang || h.HangThanhVien == hang))
                .OrderBy(h => h.MaHV)
                .ToListAsync();

            dgvHoiVien.DataSource = ds;
        }

\        private string HangDangLoc()
        {
            return cboLocHang.SelectedIndex > 0 ? cboLocHang.Text : "";
        }

\        private void XoaTrangForm()
        {
            dgvHoiVien.ClearSelection();
            dgvHoiVien.CurrentCell = null;

            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            rdoNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Today.AddYears(-18);
            cboHang.SelectedIndex = 0;
            chkTrangThai.Checked = true;
            txtHoTen.Focus();
        }

\        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            string sdt = txtSDT.Text.Trim();
            if (!sdt.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa chữ số!", "SĐT không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            if (sdt.Length < 9 || sdt.Length > 11)
            {
                MessageBox.Show("Số điện thoại phải có từ 9 đến 11 chữ số!", "SĐT không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains('@'))
            {
                MessageBox.Show("Email phải chứa ký tự '@'!", "Email không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            DateTime ns = dtpNgaySinh.Value.Date;
            int tuoi = DateTime.Today.Year - ns.Year;
            if (ns > DateTime.Today.AddYears(-tuoi)) tuoi--;
            if (tuoi < 15)
            {
                MessageBox.Show($"Hội viên phải từ 15 tuổi trở lên (hiện {tuoi} tuổi)!", "Tuổi không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            if (cboHang.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn hạng thành viên!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHang.Focus();
                return false;
            }
            return true;
        }

\        private bool LayMaDangChon(out int id)
        {
            id = 0;
            if (dgvHoiVien.CurrentRow?.DataBoundItem is not HoiVien hv)
            {
                MessageBox.Show("Vui lòng chọn một hội viên trong danh sách!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            id = hv.MaHV;
            return true;
        }
    }
}