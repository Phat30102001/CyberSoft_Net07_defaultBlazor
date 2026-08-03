using System.ComponentModel.DataAnnotations;

namespace buoi18.Models
{
    public class StudentDto
    {
        [Required(ErrorMessage = "Vui lòng nhập mã sinh viên")]
        public string MaSV { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên sinh viên")]

        public string HoTen { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SoDT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập CMND")]
        public string CMND { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0,10, ErrorMessage ="Điểm phải từ 1->10")]
        public double DiemToan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0,10, ErrorMessage ="Điểm phải từ 1->10")]
        public double DiemLy { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0,10, ErrorMessage ="Điểm phải từ 1->10")]
        public double DiemHoa { get; set; }
    }
}