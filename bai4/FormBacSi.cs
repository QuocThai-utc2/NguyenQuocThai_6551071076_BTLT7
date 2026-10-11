using bai4.Models;
using Microsoft.EntityFrameworkCore;

namespace bai4
{
    public partial class FormBacSi : Form
    {
        public FormBacSi()
        {
            InitializeComponent();
        }

        private async void FormBacSi_Load(object sender, EventArgs e)
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

        private void dgvBacSi_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBacSi.CurrentRow?.DataBoundItem is not BacSi bs) return;

            txtMaBS.Text = bs.MaBS.ToString();
            txtHoTen.Text = bs.HoTen;
            txtChuyenKhoa.Text = bs.ChuyenKhoa;
            txtSDT.Text = bs.SDT;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using var db = new AnKhangClinicContext();
                db.BacSis.Add(new BacSi
                {
                    HoTen = txtHoTen.Text.Trim(),
                    ChuyenKhoa = string.IsNullOrWhiteSpace(txtChuyenKhoa.Text) ? null : txtChuyenKhoa.Text.Trim(),
                    SDT = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim()
                });
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm bác sĩ thành công!", "Thông báo",
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
            if (!int.TryParse(txtMaBS.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một bác sĩ để sửa!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            try
            {
                using var db = new AnKhangClinicContext();
                var bs = await db.BacSis.FindAsync(id);
                if (bs == null)
                {
                    MessageBox.Show("Bác sĩ này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync();
                    XoaTrangForm();
                    return;
                }

                bs.HoTen = txtHoTen.Text.Trim();
                bs.ChuyenKhoa = string.IsNullOrWhiteSpace(txtChuyenKhoa.Text) ? null : txtChuyenKhoa.Text.Trim();
                bs.SDT = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
                await db.SaveChangesAsync();

                MessageBox.Show("Cập nhật bác sĩ thành công!", "Thông báo",
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
            if (!int.TryParse(txtMaBS.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một bác sĩ để xóa!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var xacNhan = MessageBox.Show(
                $"Bạn có chắc muốn xóa bác sĩ \"{txtHoTen.Text}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                using var db = new AnKhangClinicContext();
                var bs = await db.BacSis.FindAsync(id);
                if (bs == null)
                {
                    MessageBox.Show("Bác sĩ này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadDataAsync();
                    XoaTrangForm();
                    return;
                }

                db.BacSis.Remove(bs);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa bác sĩ thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync();
                XoaTrangForm();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa vì bác sĩ này đang có lịch khám!\n" +
                                "Hãy xóa hoặc chuyển các lịch khám của bác sĩ này trước.",
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
            dgvBacSi.AutoGenerateColumns = false;
            dgvBacSi.Columns.Clear();

            dgvBacSi.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MaBS", HeaderText = "Mã BS", Width = 70 });
            dgvBacSi.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "HoTen", HeaderText = "Họ tên", Width = 150 });
            dgvBacSi.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "ChuyenKhoa", HeaderText = "Chuyên khoa", Width = 140 });
            dgvBacSi.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "SDT", HeaderText = "SĐT", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private async Task LoadDataAsync()
        {
            using var db = new AnKhangClinicContext();
            dgvBacSi.DataSource = await db.BacSis.AsNoTracking().OrderBy(b => b.MaBS).ToListAsync();
        }

        private void XoaTrangForm()
        {
            dgvBacSi.ClearSelection();
            dgvBacSi.CurrentCell = null;

            txtMaBS.Clear();
            txtHoTen.Clear();
            txtChuyenKhoa.Clear();
            txtSDT.Clear();
            txtHoTen.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên bác sĩ không được để trống!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }
            return true;
        }
    }
}