using bai3.Models;
using Microsoft.EntityFrameworkCore;

namespace bai3
{
    // Dòng dữ liệu hiển thị trên lưới (có cột ảnh và tên loại phòng)
    public class PhongView
    {
        public int MaPhong { get; set; }
        public Image? Anh { get; set; }
        public string SoPhong { get; set; } = "";
        public int TangSo { get; set; }
        public int MaLoai { get; set; }
        public string TenLoai { get; set; } = "";
        public decimal GiaMoiDem { get; set; }
        public string TinhTrang { get; set; } = "";
        public string? HinhAnh { get; set; }
    }

    public partial class Form1 : Form
    {
        // Thư mục Images nằm cạnh file .exe
        private static readonly string ThuMucAnh = Path.Combine(Application.StartupPath, "Images");

        // Tên file ảnh của phòng đang thao tác (chỉ tên file, không phải full path)
        private string? _hinhAnh;

        public Form1()
        {
            InitializeComponent();
        }

        // ===================== EVENT: LOAD FORM =====================
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                cboTinhTrang.Items.AddRange(new object[] { "Trống", "Đang ở", "Đang dọn" });
                cboTinhTrang.SelectedIndex = 0;

                cboLocTinhTrang.Items.AddRange(new object[] { "Tất cả tình trạng", "Trống", "Đang ở", "Đang dọn" });
                cboLocTinhTrang.SelectedIndex = 0;

                CauHinhGrid();
                await NapLoaiPhongAsync();
                await LoadDataAsync();
                XoaTrangForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================== EVENT: CHỌN DÒNG → ĐỔ LÊN CONTROL =====================
        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow?.DataBoundItem is not PhongView pv) return;

            txtSoPhong.Text = pv.SoPhong;
            nudTang.Value = Math.Clamp(pv.TangSo, (int)nudTang.Minimum, (int)nudTang.Maximum);
            cboLoaiPhong.SelectedValue = pv.MaLoai;
            cboTinhTrang.SelectedIndex = cboTinhTrang.FindStringExact(pv.TinhTrang);

            _hinhAnh = pv.HinhAnh;
            HienAnh(DocAnh(_hinhAnh));
        }

        // ===================== EVENT: CHỌN ẢNH (upload) =====================
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Chọn ảnh phòng",
                Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                Directory.CreateDirectory(ThuMucAnh);

                string tenFile = Path.GetFileName(ofd.FileName);
                string dich = Path.Combine(ThuMucAnh, tenFile);

                // Trùng tên với file khác trong thư mục Images -> thêm tiền tố thời gian
                if (File.Exists(dich) &&
                    !string.Equals(Path.GetFullPath(dich), Path.GetFullPath(ofd.FileName), StringComparison.OrdinalIgnoreCase))
                {
                    tenFile = $"{DateTime.Now:yyyyMMddHHmmssfff}_{tenFile}";
                    dich = Path.Combine(ThuMucAnh, tenFile);
                }

                if (!File.Exists(dich))
                    File.Copy(ofd.FileName, dich);

                _hinhAnh = tenFile;               // chỉ lưu TÊN FILE
                HienAnh(DocAnh(_hinhAnh));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể sao chép ảnh: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLoaiPhong_Click(object sender, EventArgs e)
        {
            using (var f = new FormLoaiPhong())
            {
                f.ShowDialog(this);
            }

            try
            {
                await NapLoaiPhongAsync();
                await LoadDataAsync(LocLoai(), LocTinhTrang());
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
            try
            {
                using var db = new SunriseHomestayContext();
                if (!await ValidateAsync(db)) return;

                var p = new Phong
                {
                    SoPhong = txtSoPhong.Text.Trim(),
                    TangSo = (int)nudTang.Value,
                    MaLoai = (int)cboLoaiPhong.SelectedValue!,
                    TinhTrang = cboTinhTrang.SelectedItem!.ToString()!,
                    HinhAnh = _hinhAnh
                };

                db.Phongs.Add(p);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm phòng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(LocLoai(), LocTinhTrang());
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

            try
            {
                using var db = new SunriseHomestayContext();
                if (!await ValidateAsync(db, id)) return;

                var p = await db.Phongs.FindAsync(id);
                if (p == null)
                {
                    MessageBox.Show("Phòng này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync(LocLoai(), LocTinhTrang());
                    XoaTrangForm();
                    return;
                }

                p.SoPhong = txtSoPhong.Text.Trim();
                p.TangSo = (int)nudTang.Value;
                p.MaLoai = (int)cboLoaiPhong.SelectedValue!;
                p.TinhTrang = cboTinhTrang.SelectedItem!.ToString()!;
                p.HinhAnh = _hinhAnh;

                await db.SaveChangesAsync();

                MessageBox.Show("Cập nhật phòng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(LocLoai(), LocTinhTrang());
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
                $"Bạn có chắc muốn xóa phòng \"{txtSoPhong.Text}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var db = new SunriseHomestayContext();
                var p = await db.Phongs.FindAsync(id);
                if (p == null)
                {
                    MessageBox.Show("Phòng này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync(LocLoai(), LocTinhTrang());
                    XoaTrangForm();
                    return;
                }

                db.Phongs.Remove(p);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa phòng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(LocLoai(), LocTinhTrang());
                XoaTrangForm();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa phòng này vì đang có dữ liệu liên quan!", "Lỗi ràng buộc",
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
            cboLocLoai.SelectedIndex = 0;
            cboLocTinhTrang.SelectedIndex = 0;
            await LoadDataAsync();
            XoaTrangForm();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await LoadDataAsync(LocLoai(), LocTinhTrang());
            XoaTrangForm();
        }


        private void CauHinhGrid()
        {
            dgvPhong.AutoGenerateColumns = false;
            dgvPhong.Columns.Clear();
            dgvPhong.RowTemplate.Height = 70;

            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MaPhong", HeaderText = "Mã phòng", Width = 90 });
            dgvPhong.Columns.Add(new DataGridViewImageColumn
            {
                DataPropertyName = "Anh",
                HeaderText = "Ảnh",
                Width = 110,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "SoPhong", HeaderText = "Số phòng", Width = 90 });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TangSo", HeaderText = "Tầng", Width = 70 });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TenLoai", HeaderText = "Loại phòng", Width = 140 });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaMoiDem",
                HeaderText = "Giá/đêm",
                Width = 110,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvPhong.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TinhTrang", HeaderText = "Tình trạng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private async Task NapLoaiPhongAsync()
        {
            using var db = new SunriseHomestayContext();
            var ds = await db.LoaiPhongs.AsNoTracking().OrderBy(l => l.TenLoai).ToListAsync();

            int dangLoc = cboLocLoai.SelectedValue is int v ? v : 0;

            cboLoaiPhong.DataSource = null;
            cboLoaiPhong.DisplayMember = "TenLoai";
            cboLoaiPhong.ValueMember = "MaLoai";
            cboLoaiPhong.DataSource = ds.ToList();
            cboLoaiPhong.SelectedIndex = -1;

            var dsLoc = new List<LoaiPhong> { new LoaiPhong { MaLoai = 0, TenLoai = "Tất cả loại phòng" } };
            dsLoc.AddRange(ds);
            cboLocLoai.DataSource = null;
            cboLocLoai.DisplayMember = "TenLoai";
            cboLocLoai.ValueMember = "MaLoai";
            cboLocLoai.DataSource = dsLoc;
            cboLocLoai.SelectedValue = dangLoc;
            if (cboLocLoai.SelectedIndex < 0) cboLocLoai.SelectedIndex = 0;
        }

        private async Task LoadDataAsync(int maLoai = 0, string tinhTrang = "")
        {
            using var db = new SunriseHomestayContext();

            var ds = await db.Phongs
                .AsNoTracking()
                .Include(p => p.MaLoaiNavigation)
                .Where(p => (maLoai == 0 || p.MaLoai == maLoai)
                         && (tinhTrang == "" || p.TinhTrang == tinhTrang))
                .OrderBy(p => p.MaPhong)
                .ToListAsync();

            var hienThi = ds.Select(p => new PhongView
            {
                MaPhong = p.MaPhong,
                Anh = DocAnh(p.HinhAnh, 100, 66),    
                SoPhong = p.SoPhong,
                TangSo = p.TangSo,
                MaLoai = p.MaLoai,
                TenLoai = p.MaLoaiNavigation.TenLoai,
                GiaMoiDem = p.MaLoaiNavigation.GiaMoiDem,
                TinhTrang = p.TinhTrang,
                HinhAnh = p.HinhAnh
            }).ToList();

            var cu = dgvPhong.DataSource as List<PhongView>;
            dgvPhong.DataSource = hienThi;
            cu?.ForEach(x => x.Anh?.Dispose());     
        }

        private int LocLoai() => cboLocLoai.SelectedValue is int v ? v : 0;

        private string LocTinhTrang() => cboLocTinhTrang.SelectedIndex > 0 ? cboLocTinhTrang.Text : "";

        private static Image? DocAnh(string? tenFile, int rong = 0, int cao = 0)
        {
            if (string.IsNullOrWhiteSpace(tenFile)) return null;
            string path = Path.Combine(ThuMucAnh, tenFile);
            if (!File.Exists(path)) return null;

            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var ms = new MemoryStream();
                fs.CopyTo(ms);
                ms.Position = 0;
                using var goc = Image.FromStream(ms);
                return rong > 0 && cao > 0 ? new Bitmap(goc, rong, cao) : new Bitmap(goc);
            }
            catch
            {
                return null;
            }
        }

        private void HienAnh(Image? anh)
        {
            var cu = picAnh.Image;
            picAnh.Image = anh;
            cu?.Dispose();
        }

        private void XoaTrangForm()
        {
            dgvPhong.ClearSelection();
            dgvPhong.CurrentCell = null;

            txtSoPhong.Clear();
            nudTang.Value = nudTang.Minimum;
            cboLoaiPhong.SelectedIndex = -1;
            cboTinhTrang.SelectedIndex = 0;
            _hinhAnh = null;
            HienAnh(null);
            txtSoPhong.Focus();
        }

        private async Task<bool> ValidateAsync(SunriseHomestayContext db, int? excludeId = null)
        {
            string so = txtSoPhong.Text.Trim();

            if (string.IsNullOrEmpty(so))
            {
                MessageBox.Show("Số phòng không được để trống!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return false;
            }
            if (cboLoaiPhong.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhong.Focus();
                return false;
            }
            if (cboTinhTrang.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn tình trạng phòng!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTinhTrang.Focus();
                return false;
            }

            bool trung = await db.Phongs.AnyAsync(p => p.SoPhong == so && (excludeId == null || p.MaPhong != excludeId));
            if (trung)
            {
                MessageBox.Show($"Số phòng \"{so}\" đã tồn tại!", "Trùng số phòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return false;
            }
            return true;
        }

        private bool LayMaDangChon(out int id)
        {
            id = 0;
            if (dgvPhong.CurrentRow?.DataBoundItem is not PhongView pv)
            {
                MessageBox.Show("Vui lòng chọn một phòng trong danh sách!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            id = pv.MaPhong;
            return true;
        }
    }
}