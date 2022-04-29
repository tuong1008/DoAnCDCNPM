using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SignalrSqlDependency1.Models
{
    public class LenhDat
    {
        public int id { get; set; }

        [Column(TypeName = "nchar(7)")]
        public string maCP { get; set; }

        public DateTime ngayDat { get; set; }

        [Column(TypeName = "nchar(1)")]
        public string loaiGD { get; set; }

        [Column(TypeName = "nchar(10)")]
        public string loaiLenh { get; set; }

        public int soLuong { get; set; }

        [Column(TypeName = "float")]
        public float giaDat { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string trangThaiLenh { get; set; }
    }
}