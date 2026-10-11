using bai4.Models;
using Microsoft.EntityFrameworkCore;

namespace bai4
{
    public class LichKhamView
    {
        public int MaLich { get; set; }
        public string TenBenhNhan { get; set; } = "";
        public string? SDT { get; set; }
        public DateTime NgayKham { get; set; }
        public string GioKham { get; set; } = "";
        public int MaBS { get; set; }
        public string TenBacSi { get; set; } = "";
        public string ChuyenKhoa { get; set; } = "";
        public string TrangThai { get; set; } = "";
    }

    public class BacSiItem
    {
        public int MaBS { get; set; }
        public string HienThi { get; set; } = "";
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                cboTrangThai.Items.AddRange(new object[] { "Chờ khám", "Đã khám", "Đã hủy" });
                cboTrangThai.SelectedIndex = 0;

                dtpTuNgay.Value = DateTime.Today.AddYears(-1);
                dtpDenNgay.Value = DateTime.Today.AddYears(1);

                CauHinhGrid();
                await NapBacSiAsync();
                await LoadDataAsync();
                XoaTrangForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLichKham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLichKham.CurrentRow?.DataBoundItem is not LichKhamView lk) return;

            txtTenBenhNhan.Text = lk.TenBenhNhan;
            txtSDT.Text = lk.SDT;
            dtpNgayKham.Value = lk.NgayKham;

            if (TimeSpan.TryParse(lk.GioKham, out TimeSpan gio))
                dtpGioKham.Value = DateTime.Today.Add(gio);

            cboBacSi.SelectedValue = lk.MaBS;
            cboTrangThai.SelectedIndex = cboTrangThai.FindStringExact(lk.TrangThai);
        }

        private async void btnBacSi_Click(object sender, EventArgs e)
        {
            using (var f = new FormBacSi())
            {
                f.ShowDialog(this);
            }

            try
            {
                await NapBacSiAsync();
                await LoadDataAsync(TuNgay(), DenNgay(), LocBacSi());
                XoaTrangForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(laThem: true)) return;

            try
            {
                using var db = new AnKhangClinicContext();

                var lk = new LichKham
                {
                    TenBenhNhan = txtTenBenhNhan.Text.Trim(),
                    SDT = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim(),
                    NgayKham = dtpNgayKham.Value.Date,
                    GioKham = dtpGioKham.Value.ToString("HH:mm"),
                    MaBS = (int)cboBacSi.SelectedValue!,
                    TrangThai = cboTrangThai.SelectedItem!.ToString()!
                };

                db.LichKhams.Add(lk);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm lịch khám thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(TuNgay(), DenNgay(), LocBacSi());
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
            if (!ValidateInput(laThem: false)) return;

            try
            {
                using var db = new AnKhangClinicContext();
                var lk = await db.LichKhams.FindAsync(id);
                if (lk == null)
                {
                    MessageBox.Show("Lịch khám này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync(TuNgay(), DenNgay(), LocBacSi());
                    XoaTrangForm();
                    return;
                }

                lk.TenBenhNhan = txtTenBenhNhan.Text.Trim();
                lk.SDT = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
                lk.NgayKham = dtpNgayKham.Value.Date;
                lk.GioKham = dtpGioKham.Value.ToString("HH:mm");
                lk.MaBS = (int)cboBacSi.SelectedValue!;
                lk.TrangThai = cboTrangThai.SelectedItem!.ToString()!;

                await db.SaveChangesAsync();

                MessageBox.Show("Cập nhật lịch khám thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(TuNgay(), DenNgay(), LocBacSi());
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
                $"Bạn có chắc muốn xóa lịch khám của \"{txtTenBenhNhan.Text}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var db = new AnKhangClinicContext();
                var lk = await db.LichKhams.FindAsync(id);
                if (lk == null)
                {
                    MessageBox.Show("Lịch khám này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync(TuNgay(), DenNgay(), LocBacSi());
                    XoaTrangForm();
                    return;
                }

                db.LichKhams.Remove(lk);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa lịch khám thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(TuNgay(), DenNgay(), LocBacSi());
                XoaTrangForm();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa lịch khám này vì đang có dữ liệu liên quan!", "Lỗi ràng buộc",
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
            dtpTuNgay.Value = DateTime.Today.AddYears(-1);
            dtpDenNgay.Value = DateTime.Today.AddYears(1);
            cboLocBacSi.SelectedIndex = 0;
            await LoadDataAsync();
            XoaTrangForm();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("\"Từ ngày\" không được lớn hơn \"Đến ngày\"!", "Khoảng ngày không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpTuNgay.Focus();
                return;
            }

            await LoadDataAsync(TuNgay(), DenNgay(), LocBacSi());
            XoaTrangForm();
        }


        private void CauHinhGrid()
        {
            dgvLichKham.AutoGenerateColumns = false;
            dgvLichKham.Columns.Clear();

            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MaLich", HeaderText = "Mã lịch", Width = 70 });
            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TenBenhNhan", HeaderText = "Tên bệnh nhân", Width = 150 });
            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "SDT", HeaderText = "SĐT", Width = 100 });
            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayKham",
                HeaderText = "Ngày khám",
                Width = 100,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });
            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "GioKham", HeaderText = "Giờ khám", Width = 80 });
            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TenBacSi", HeaderText = "Bác sĩ", Width = 150 });
            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "ChuyenKhoa", HeaderText = "Chuyên khoa", Width = 130 });
            dgvLichKham.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TrangThai", HeaderText = "Trạng thái", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private async Task NapBacSiAsync()
        {
            using var db = new AnKhangClinicContext();
            var ds = await db.BacSis
                .AsNoTracking()
                .OrderBy(b => b.HoTen)
                .Select(b => new BacSiItem
                {
                    MaBS = b.MaBS,
                    HienThi = "BS. " + b.HoTen + (b.ChuyenKhoa != null ? " - " + b.ChuyenKhoa : "")
                })
                .ToListAsync();

            int dangLoc = cboLocBacSi.SelectedValue is int v ? v : 0;

            cboBacSi.DataSource = null;
            cboBacSi.DisplayMember = "HienThi";
            cboBacSi.ValueMember = "MaBS";
            cboBacSi.DataSource = ds.ToList();
            cboBacSi.SelectedIndex = -1;

            var dsLoc = new List<BacSiItem> { new BacSiItem { MaBS = 0, HienThi = "Tất cả bác sĩ" } };
            dsLoc.AddRange(ds);
            cboLocBacSi.DataSource = null;
            cboLocBacSi.DisplayMember = "HienThi";
            cboLocBacSi.ValueMember = "MaBS";
            cboLocBacSi.DataSource = dsLoc;
            cboLocBacSi.SelectedValue = dangLoc;
            if (cboLocBacSi.SelectedIndex < 0) cboLocBacSi.SelectedIndex = 0;
        }

        private async Task LoadDataAsync(DateTime? tuNgay = null, DateTime? denNgay = null, int maBS = 0)
        {
            using var db = new AnKhangClinicContext();

            DateTime tu = (tuNgay ?? DateTime.Today.AddYears(-1)).Date;
            DateTime den = (denNgay ?? DateTime.Today.AddYears(1)).Date;

            var ds = await db.LichKhams
                .AsNoTracking()
                .Include(x => x.MaBSNavigation)
                .Where(x => x.NgayKham >= tu && x.NgayKham <= den
                         && (maBS == 0 || x.MaBS == maBS))
                .OrderBy(x => x.NgayKham).ThenBy(x => x.GioKham)
                .ToListAsync();

            dgvLichKham.DataSource = ds.Select(x => new LichKhamView
            {
                MaLich = x.MaLich,
                TenBenhNhan = x.TenBenhNhan,
                SDT = x.SDT,
                NgayKham = x.NgayKham,
                GioKham = x.GioKham,
                MaBS = x.MaBS,
                TenBacSi = "BS. " + x.MaBSNavigation.HoTen,
                ChuyenKhoa = x.MaBSNavigation.ChuyenKhoa ?? "",
                TrangThai = x.TrangThai
            }).ToList();
        }

        private DateTime TuNgay() => dtpTuNgay.Value.Date;
        private DateTime DenNgay() => dtpDenNgay.Value.Date;
        private int LocBacSi() => cboLocBacSi.SelectedValue is int v ? v : 0;

        private void XoaTrangForm()
        {
            dgvLichKham.ClearSelection();
            dgvLichKham.CurrentCell = null;

            txtTenBenhNhan.Clear();
            txtSDT.Clear();
            dtpNgayKham.Value = DateTime.Today;
            dtpGioKham.Value = DateTime.Today.AddHours(8);
            cboBacSi.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = 0;
            txtTenBenhNhan.Focus();
        }

        private bool ValidateInput(bool laThem)
        {
            if (string.IsNullOrWhiteSpace(txtTenBenhNhan.Text))
            {
                MessageBox.Show("Tên bệnh nhân không được để trống!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenBenhNhan.Focus();
                return false;
            }
            if (cboBacSi.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn bác sĩ!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBacSi.Focus();
                return false;
            }
            if (dtpNgayKham.Value.Date < DateTime.Today)
            {
                bool giuNgayCu = !laThem
                    && dgvLichKham.CurrentRow?.DataBoundItem is LichKhamView lk
                    && lk.NgayKham.Date == dtpNgayKham.Value.Date;

                if (!giuNgayCu)
                {
                    MessageBox.Show("Không thể đặt lịch khám vào ngày trong quá khứ!", "Ngày không hợp lệ",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayKham.Focus();
                    return false;
                }
            }
            if (cboTrangThai.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTrangThai.Focus();
                return false;
            }
            return true;
        }

        private bool LayMaDangChon(out int id)
        {
            id = 0;
            if (dgvLichKham.CurrentRow?.DataBoundItem is not LichKhamView lk)
            {
                MessageBox.Show("Vui lòng chọn một lịch khám trong danh sách!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            id = lk.MaLich;
            return true;
        }
    }
}