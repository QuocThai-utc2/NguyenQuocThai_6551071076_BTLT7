using bai1.Models;
using Microsoft.EntityFrameworkCore;

namespace bai1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            CauHinhGrid();
            await LoadDataAsync();
        }

        private void CauHinhGrid()
        {
            dgvTheLoai.AutoGenerateColumns = false;
            dgvTheLoai.Columns.Clear();

            dgvTheLoai.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MaTL", HeaderText = "Mã TL", Width = 70 });
            dgvTheLoai.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "TenTheLoai", HeaderText = "Tên thể loại", Width = 180 });
            dgvTheLoai.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MoTa", HeaderText = "Mô tả", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvTheLoai.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "SoLuongSach", HeaderText = "Số lượng sách", Width = 110 });
            dgvTheLoai.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayTao",
                HeaderText = "Ngày tạo",
                Width = 150,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm:ss" }
            });
        }

        private async Task LoadDataAsync(string keyword = "")
        {
            using var db = new TriThucBooksContext();

            var query = db.TheLoaiSaches.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(t => t.TenTheLoai.Contains(keyword));
            }

            dgvTheLoai.DataSource = await query.OrderBy(t => t.MaTL).ToListAsync();
        }

        private void dgvTheLoai_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTheLoai.CurrentRow?.DataBoundItem is not TheLoaiSach tl) return;

            txtMaTL.Text = tl.MaTL.ToString();
            txtTenTheLoai.Text = tl.TenTheLoai;
            txtMoTa.Text = tl.MoTa;
            lblNgayTao.Text = "Ngày tạo: " + tl.NgayTao.ToString("dd/MM/yyyy HH:mm:ss");
        }


        private async Task<bool> ValidateAsync(TriThucBooksContext db, int? excludeId = null)
        {
            string ten = txtTenTheLoai.Text.Trim();

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Tên thể loại không được để trống!", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTheLoai.Focus();
                return false;
            }

            bool trung = await db.TheLoaiSaches
                .AnyAsync(t => t.TenTheLoai == ten && (excludeId == null || t.MaTL != excludeId));

            if (trung)
            {
                MessageBox.Show($"Thể loại \"{ten}\" đã tồn tại!", "Trùng tên",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTheLoai.Focus();
                return false;
            }
            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new TriThucBooksContext();
                if (!await ValidateAsync(db)) return;

                var tl = new TheLoaiSach
                {
                    TenTheLoai = txtTenTheLoai.Text.Trim(),
                    MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim()
                };

                db.TheLoaiSaches.Add(tl);
                await db.SaveChangesAsync();

                MessageBox.Show("Thêm thể loại thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LamMoiAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaTL.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một thể loại trong danh sách để sửa!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new TriThucBooksContext();
                if (!await ValidateAsync(db, id)) return;

                var tl = await db.TheLoaiSaches.FindAsync(id);
                if (tl == null)
                {
                    MessageBox.Show("Thể loại này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LamMoiAsync();
                    return;
                }

                tl.TenTheLoai = txtTenTheLoai.Text.Trim();
                tl.MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim();

                await db.SaveChangesAsync();

                MessageBox.Show("Cập nhật thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadDataAsync(txtTimKiem.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaTL.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một thể loại trong danh sách để xóa!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa thể loại \"{txtTenTheLoai.Text}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var db = new TriThucBooksContext();
                var tl = await db.TheLoaiSaches.FindAsync(id);
                if (tl == null)
                {
                    MessageBox.Show("Thể loại này không còn tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LamMoiAsync();
                    return;
                }

                db.TheLoaiSaches.Remove(tl);
                await db.SaveChangesAsync();

                MessageBox.Show("Xóa thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LamMoiAsync();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(
                    "Không thể xóa vì thể loại này đang được các sách khác tham chiếu!\n" +
                    "Hãy xóa hoặc chuyển các sách thuộc thể loại này trước.",
                    "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await LoadDataAsync(txtTimKiem.Text);
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            await LamMoiAsync();
        }

        private async Task LamMoiAsync()
        {
            await LoadDataAsync(txtTimKiem.Text);

            dgvTheLoai.ClearSelection();
            dgvTheLoai.CurrentCell = null;

            txtMaTL.Clear();
            txtTenTheLoai.Clear();
            txtMoTa.Clear();
            lblNgayTao.Text = "Ngày tạo: ";
            txtTenTheLoai.Focus();
        }
    }
}