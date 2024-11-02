using CafeShopManagementSystem.DAO;
using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class ProfileForm : Form
    {
        private Account account;

        public Account Account
        {
            private get { return account; }
            set
            {
                account = value;
                LoadAccount();
            }
        }

        private int totalTime = 60;
        private int countDownTime = 0;
        private bool timeRunning = false;

        private string code = int.MinValue.ToString();

        public ProfileForm()
        {
            InitializeComponent();
        }

        #region Methods
        private void LoadAccount()
        {
            if (account != null)
            {
                txbUsername.Text = account.Username;
                txbFullname.Text = account.Fullname;
                txbPhoneNumber.Text = account.PhoneNumber;
                txbEmail.Text = account.Email;
                txbRole.Text = RoleDAO.Instance.GetRoleByID(account.RoleID);
            }
        }

        private void TimeOut()
        {
            CodeTimer.Stop();

            timeRunning = false;
            countDownTime = 0;
            code = int.MinValue.ToString();
            btnSendCode.Text = "Gửi mã";
        }
        #endregion

        #region Events
        private void OpenChangePasswordEvt(object sender, EventArgs e)
        {
            pnInfo.Visible = false;
            pnChangePassword.Visible = true;
            btnShowFormChange.Visible = false;
        }

        private void CloseChangePasswordEvt(object sender, EventArgs e)
        {
            txbOldPass.Text = txbNewPass1.Text = txbNewPass2.Text = txbValiCode.Text = "";
            pnInfo.Visible = true;
            pnChangePassword.Visible = false;
            btnShowFormChange.Visible = true;
            TimeOut();
        }

        private void ExitClickEvt(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            if (!timeRunning)
            {
                Random random = new Random();
                int codeNumber = random.Next(0, 100000);
                code = codeNumber.ToString("00000");

                string from = "ntq2372003@gmail.com";
                string pass = "wmmi uwji gyan hepq";

                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(account.Email);
                    mailMessage.From = new MailAddress(from);
                    mailMessage.Subject = "Mã xác nhận để đổi mật khẩu";
                    mailMessage.Body = code;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new NetworkCredential(from, pass);

                    smtpClient.Send(mailMessage);
                    MessageBox.Show("Gửi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                countDownTime = totalTime;
                timeRunning = true;
                CodeTimer.Start();
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string oldPass = txbOldPass.Text.Trim();
            string newPass1 = txbNewPass1.Text.Trim();
            string newPass2 = txbNewPass2.Text.Trim();
            string valiCode = txbValiCode.Text.Trim();

            if (!oldPass.Equals(account.Password))
            {
                MessageBox.Show("Mật Khẩu hiện tại  có sự sai xót!\nYêu cầu kiểm tra lại", "Lỗi", MessageBoxButtons.OK);
                return;
            }

            Regex regexPassword = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.#?!@$%^&*-]).{8,}$");
            Match match = regexPassword.Match(newPass1);
            if (!match.Success)
            {
                string message = "Mật khẩu sai định dạng:\n" +
                                "- Phải có ít nhất 1 chữ hoa\n" +
                                "- Phải có ít nhất 1 chữ thường\n" +
                                "- Phải có ít nhất 1 chữ số\n" +
                                "- Phải có ít nhất 1 kí tự [.#?!@$%^&*-]\n" +
                                "- Phải có ít nhất 8 ký tự";
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK);
                txbNewPass1.Focus();
                return;
            }

            if (!newPass1.Equals(newPass2))
            {
                MessageBox.Show("Mật Khẩu mới không trùng khớp!\nYêu cầu kiểm tra lại", "Lỗi", MessageBoxButtons.OK);
                return;
            }

            if (!valiCode.Equals(code) || !timeRunning)
            {
                MessageBox.Show("Chưa có mã xác thực", "Lỗi", MessageBoxButtons.OK);
                return;
            }

            if (AccountDAO.Instance.UpdatePassword(account.Username, newPass1))
            {
                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK);
                txbOldPass.Text = "";
                txbNewPass1.Text = "";
                txbNewPass2.Text = "";
                txbValiCode.Text = "";
                TimeOut();
            }
        }

        private void CodeTimeEvt(object sender, EventArgs e)
        {
            btnSendCode.Text = countDownTime.ToString();
            countDownTime--;

            if (countDownTime < 0)
                TimeOut();
        }
        #endregion
    }
}
