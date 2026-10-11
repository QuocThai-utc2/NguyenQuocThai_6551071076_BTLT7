using bai3.Models;
using Microsoft.EntityFrameworkCore;

namespace bai3
{
    public partial class FormLoaiPhong : Form
    {
        public FormLoaiPhong()
        {
            InitializeComponent();
        }

        private async void FormLoaiPhong_Load(object sender, EventArgs e)
        {
            try
            {
                CauHinhGrid();
                await LoadDataAsync();
                XoaTrangForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.CurrentRow?.DataBoundItem is not LoaiPhong lp) return;

            txtMaLoai.Text = lp.MaLoai.ToString();
            txtTenLoai.Text = lp.TenLoai;
            txtGia.Text = lp.GiaMoiDem.ToString("0.##");
            txtMoTa.Text = lp.MoTa;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal gia)) return;

            try
            {
                using var db = new SunriseHomestayContext();

                var lp = new LoaiPhong
                {
                    TenLoai = txtTenLoai.Text.Trim(),
                    GiaMoiDem = gia,
                    MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
                };
                db.LoaiPhongs.Add(lp);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm loại phòng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync();
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
            if (!int.TryParse(txtMaLoai.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một loại phòng để sửa!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput(out decimal gia)) return;

            try
            {
                using var db = new SunriseHomestayContext();
                var lp = await db.LoaiPhongs.FindAsync(id);
                if (lp == null)
                {
                    MessageBox.Show("Loại phòng này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync();
                    XoaTrangForm();
                    return;
                }

                lp.TenLoai = txtTenLoai.Text.Trim();
                lp.GiaMoiDem = gia;
                lp.MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim();
                await db.SaveChangesAsync();

                MessageBox.Show("Cập nhật loại phòng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync();
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
            if (!int.TryParse(txtMaLoai.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một loại phòng để xóa!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var xacNhan = MessageBox.Show(
                $"Bạn có chắc muốn xóa loại phòng \"{txtTenLoai.Text}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var db = new SunriseHomestayContext();
                var lp = await db.LoaiPhongs.FindAsync(id);
                if (lp == null)
                {
                    MessageBox.Show("Loại phòng này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync();
                    XoaTrangForm();
                    return;
                }

                db.LoaiPhongs.Remove(lp);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa loại phòng thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync();
                XoaTrangForm();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa vì loại phòng này đang có phòng sử dụng!\n" +
                                "Hãy xóa hoặc chuyển các phòng thuộc loại này trước.",
                    "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
            XoaTrangForm();
        }


        private void CauHinhGrid()
        {
            dgvLoaiPhong.AutoGenerateColumns = false;
            dgvLoaiPhong.Columns.Clear();

            dgvLoaiPhong.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MaLoai", HeaderText = "Mã loại", Width = 80 });
            dgvLoaiPhong.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TenLoai", HeaderText = "Tên loại", Width = 140 });
            dgvLoaiPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaMoiDem",
                HeaderText = "Giá/đêm",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvLoaiPhong.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MoTa", HeaderText = "Mô tả", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private async Task LoadDataAsync()
        {
            using var db = new SunriseHomestayContext();
            dgvLoaiPhong.DataSource = await db.LoaiPhongs.AsNoTracking().OrderBy(l => l.MaLoai).ToListAsync();
        }

        private void XoaTrangForm()
        {
            dgvLoaiPhong.ClearSelection();
            dgvLoaiPhong.CurrentCell = null;

            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtGia.Clear();
            txtMoTa.Clear();
            txtTenLoai.Focus();
        }

        private bool ValidateInput(out decimal gia)
        {
            gia = 0;

            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Tên loại phòng không được để trống!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return false;
            }
            if (!decimal.TryParse(txtGia.Text.Trim(), out gia) || gia < 0)
            {
                MessageBox.Show("Giá mỗi đêm phải là một số không âm!", "Giá không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return false;
            }
            return true;
        }
    }
}