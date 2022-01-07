using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Account
    {
        public int Id { get; set; }

        [DisplayName("Tên tài khoản")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} từ 6-20 kí tự")]
        public string Username { get; set; }

        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} từ 6-20 kí tự")]
        public string Password { get; set; }

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "{0} không hợp lệ")]
        //[RegularExpression(@"[a-z0-9._%+-]+@gmail.com", ErrorMessage = "Vui lòng nhập một email hợp lệ kết thúc bằng @gmail.com")]
        public string Email { get; set; }

        [DisplayName("SĐT")]
        [RegularExpression("0\\d{9}", ErrorMessage = "SĐT không hợp lệ")]
        public string Phone { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Họ tên")]
        public string FullName { get; set; }

        [DisplayName("Admin")] 
        public bool IsAdmin { get; set; } 

        [DisplayName("Avatar")]
        public string Avatar { get; set; }

        [DisplayName("Active")]
        public bool Status { get; set; } 

        
        public List<Invoice> Invoices { get; set; }

        
        public List<Cart> Carts { get; set; }
    }
}
