using MobileDevice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileDevice.DTO
{
    public class CartDTO
    {
        private MobilePhoneDB db = new MobilePhoneDB();
        [Key]
        public int iIDsanpham { set; get; }
        public string sTensanpham { set; get; }
        public string sHinhanhsanpham { set; get; }
        public string diachi { get; set; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double tTongtien { get; set; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public CartDTO(int IDsanpham)
        {
            iIDsanpham = IDsanpham;
            Product sp = db.Products.Single(n => n.ID_Product == iIDsanpham);
            sTensanpham = sp.Name;
            sHinhanhsanpham = sp.Image;
            dDongia = Double.Parse(sp.Price.ToString());
            iSoluong = 1;
        }
    }
}